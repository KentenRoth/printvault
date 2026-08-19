using Microsoft.Extensions.Options;
using PrintVault.Backend.Configuration;

namespace PrintVault.Backend.Services;

public class FileWatcherService : BackgroundService
{
    private readonly ILogger<FileWatcherService> _logger;
    private readonly StorageOptions _options;
    private readonly MfParserService _parser;
    private readonly IServiceScopeFactory _scopeFactory;
    private FileSystemWatcher? _watcher;

    public FileWatcherService(
        ILogger<FileWatcherService> logger,
        IOptions<StorageOptions> options,
        MfParserService parser,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _options = options.Value;
        _parser = parser;
        _scopeFactory = scopeFactory;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _watcher = new FileSystemWatcher(_options.IncomingPath, "*.3mf")
        {
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite,
            EnableRaisingEvents = true
        };

        _watcher.Created += OnFileCreated;
        _logger.LogInformation("Watching for .3mf files at {path}", _options.IncomingPath);
        stoppingToken.Register(() =>
        {
            _logger.LogInformation("FileWatcherService is stopping.");
            _watcher.EnableRaisingEvents = false;
        });

        return Task.CompletedTask;
    }

    private async void OnFileCreated(object sender, FileSystemEventArgs e)
    {
        _logger.LogInformation("Detected: {File}", e.Name);

        var ready = await WaitForFileReadyAsync(e.FullPath);
        if (!ready)
        {
            _logger.LogError("File not ready: {File}", e.Name);
            return;
        }

        var destination = MoveFile(e.FullPath);

        try
        {
            var data = _parser.Parse(destination);

            await using var scope = _scopeFactory.CreateAsyncScope();
            var ingestion = scope.ServiceProvider.GetRequiredService<ModelIngestionService>();
            await ingestion.IngestAsync(data);

            _logger.LogInformation("Ingested: {File}", e.Name);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to ingest {File}", e.Name);
        }
    }

    public string MoveFile(string sourcePath)
    {
        var destination = BuildDestinationPath(sourcePath);
        File.Move(sourcePath, destination);
        _logger.LogInformation("Moving {Source} to {Destination}", Path.GetFileName(sourcePath), Path.GetFileName(destination));
        return destination;
    }

    private async Task<bool> WaitForFileReadyAsync(string filePath)
    {
        const int maxRetries = 3;
        const int baseDelay = 500;

        for (var attempt = 1; attempt <= maxRetries; attempt++)
        {
            try
            {
                await using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
                return true;
            }
            catch (IOException)
            {
                var delay = baseDelay * (int)Math.Pow(2, attempt - 1);
                _logger.LogWarning("File not ready (attempt {Attempt}/{Max}), retrying in {Delay}ms: {File}", attempt, maxRetries, delay, Path.GetFileName(filePath));
                await Task.Delay(delay);
            }
        }
        return false;
    }

    private string BuildDestinationPath(string sourcePath)
    {
        var fileName = Path.GetFileNameWithoutExtension(sourcePath);
        var extension = Path.GetExtension(sourcePath);
        var candidate = Path.Combine(_options.StorageRoot, $"{fileName}{extension}");

        if (!File.Exists(candidate)) return candidate;

        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        return Path.Combine(_options.StorageRoot, $"{fileName}{timestamp}{extension}");
    }

    public override void Dispose()
    {
        _watcher?.Dispose();
        base.Dispose();
    }
}