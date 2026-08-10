namespace PrintVault.Backend.Models;

public class Plate
{
    public int Id { get; set; }
    public int PlateNumber { get; set; }
    public double FilamentUsedG { get; set; }
    public int PrintTimeSeconds { get; set; }
    public string ThumbnailSmallPath { get; set; } = string.Empty;
    public string ThumbnailMediumPath { get; set; } = string.Empty;
    public string ThumbnailLargePath { get; set; } = string.Empty;

    public int PrintModelId { get; set; }
    public PrintModel PrintModel { get; set; } = null!;
}