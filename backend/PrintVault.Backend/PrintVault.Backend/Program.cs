using PrintVault.Backend.Configuration;
using PrintVault.Backend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<StorageOptions>(builder.Configuration.GetSection("Storage"));

builder.Services.AddHostedService<FileWatcherService>();

var app = builder.Build();

app.Run();