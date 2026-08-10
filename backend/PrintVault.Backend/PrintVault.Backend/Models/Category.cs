namespace PrintVault.Backend.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public ICollection<PrintModel> PrintModels { get; set; } = new List<PrintModel>();
}