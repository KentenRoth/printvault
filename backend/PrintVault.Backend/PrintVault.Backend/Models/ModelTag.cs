namespace PrintVault.Backend.Models;

public class ModelTag
{
    public int PrintModelId { get; set; }
    public PrintModel PrintModel { get; set; } = null!;

    public int TagId { get; set; }
    public Tag Tag { get; set; } = null!;
}