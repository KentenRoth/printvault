namespace PrintVault.Backend.DTOs.Model.Response;

public class PlateResponseDto
{
    public int Id { get; set; }
    public int PlateNumber { get; set; }
    public double FilamentUsedG { get; set; }
    public int PrintTimeSeconds { get; set; }
    public string ThumbnailSmallPath { get; set; } = string.Empty;
    public string ThumbnailMediumPath { get; set; } = string.Empty;
    public string ThumbnailLargePath { get; set; } = string.Empty;
}
