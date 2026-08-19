using PrintVault.Backend.Data;
using PrintVault.Backend.Models;

namespace PrintVault.Backend.Services;

public class ModelIngestionService(PrintVaultContext db)
{
    public async Task IngestAsync(MfData data)
    {
        var model = new PrintModel
        {
            Title = data.Title,
            Description = data.Description,
            CreationDate = data.CreationDate,
            ProfileTitle = data.ProfileTitle,
            ProfileDescription = data.ProfileDescription,
            TotalFilamentUsedG = data.TotalFilamentUsed,
            TotalPrintTimeSeconds = data.TotalPrintTimeSeconds,
            NumberOfPlates = data.NumberOfPlates,
            Plates = data.Plates.Select(p => new Plate
            {
                PlateNumber = p.PlateNumber,
                FilamentUsedG = p.FilamentUsed,
                PrintTimeSeconds = p.PrintTimeSeconds
            }).ToList()
        };

        db.PrintModels.Add(model);
        await db.SaveChangesAsync();
    }
}