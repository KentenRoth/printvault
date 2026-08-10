namespace PrintVault.Backend.Models;

public class ModelImage
{
    public int Id { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public string ImageType { get; set; } = string.Empty;

    public int PrintModelId { get; set; }
    public PrintModel PrintModel { get; set; } = null!;
}