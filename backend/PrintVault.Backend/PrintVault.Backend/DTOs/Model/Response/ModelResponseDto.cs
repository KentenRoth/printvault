namespace PrintVault.Backend.DTOs.Model.Response;

public class ModelResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CreationDate { get; set; } = string.Empty;
    public string ProfileTitle { get; set; } = string.Empty;
    public string ProfileDescription { get; set; } = string.Empty;
    public double TotalFilamentUsedG { get; set; }
    public int TotalPrintTimeSeconds { get; set; }
    public int NumberOfPlates { get; set; }
    public bool IsFavorite { get; set; }
    public DateTime ImportedAt { get; set; }

    public List<PlateResponseDto> Plates { get; set; } = new();
}