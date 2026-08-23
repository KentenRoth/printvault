using Microsoft.EntityFrameworkCore;
using PrintVault.Backend.Configuration;
using PrintVault.Backend.Data;
using PrintVault.Backend.Interfaces;
using PrintVault.Backend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<StorageOptions>(
    builder.Configuration.GetSection("Storage"));

builder.Services.AddDbContext<PrintVaultContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PrintVault")));

builder.Services.AddSingleton<MfParserService>();
builder.Services.AddScoped<ModelIngestionService>();
builder.Services.AddHostedService<FileWatcherService>();
builder.Services.AddControllers();
builder.Services.AddScoped<IModelService, ModelService>();

var app = builder.Build();

app.MapControllers();

app.Run();