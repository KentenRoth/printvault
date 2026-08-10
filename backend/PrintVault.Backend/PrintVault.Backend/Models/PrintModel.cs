namespace PrintVault.Backend.Models;

public class PrintModel
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
    public DateTime ImportedAt { get; set; } = DateTime.UtcNow;

    public int? CategoryId { get; set; }
    public Category? Category { get; set; }

    public ICollection<Plate> Plates { get; set; } = new List<Plate>();
    public ICollection<ModelImage> Images { get; set; } = new List<ModelImage>();
    public ICollection<ModelTag> ModelTags { get; set; } = new List<ModelTag>();
}