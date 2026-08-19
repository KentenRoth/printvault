using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using PrintVault.Backend.Configuration;
using PrintVault.Backend.Services;

namespace PrintVault.Tests.Services;

public class FileWatcherServiceTests : IDisposable
{
    private readonly string _incomingPath;
    private readonly string _storagePath;
    private readonly FileWatcherService _service;

    public FileWatcherServiceTests()
    {
        _incomingPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        _storagePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        
        Directory.CreateDirectory(_incomingPath);
        Directory.CreateDirectory(_storagePath);

        var options = Options.Create(new StorageOptions
        {
            IncomingPath = _incomingPath,
            StorageRoot = _storagePath,
        });
        
        var scopeFactory = new ServiceCollection().BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();
        _service = new FileWatcherService(NullLogger<FileWatcherService>.Instance, options, new MfParserService(), scopeFactory);
    }

    [Fact]
    public void MoveFile_WhenFileExists_MovesFileToStorage()
    {
        var fileName = "test.3mf";
        var sourcePath = Path.Combine(_incomingPath, fileName);
        File.WriteAllText(sourcePath, "fake 3mf content");

        _service.MoveFile(sourcePath);
        
        Assert.False(File.Exists(sourcePath));
        Assert.True(File.Exists(Path.Combine(_storagePath, fileName)));
    }

    [Fact]
    public void MoveFile_WhenDuplicateExisting_MovesFileWithTimestamp()
    {
        var fileName = "test.3mf";
        var sourcePath = Path.Combine(_incomingPath, fileName);
        File.WriteAllText(sourcePath, "fake 3mf content");
        
        File.WriteAllText(Path.Combine(_storagePath, fileName), "existing file");
        
        _service.MoveFile(sourcePath);
        
        Assert.False(File.Exists(sourcePath));
        var storageFiles = Directory.GetFiles(_storagePath);
        Assert.Equal(2, storageFiles.Length);
    }


    public void Dispose()
    {
        Directory.Delete(_incomingPath, true);
        Directory.Delete(_storagePath, true);
    }
}