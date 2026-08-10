namespace PrintVault.Backend.Models;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    
    public ICollection<ModelTag> ModelTags { get; set; } = new List<ModelTag>();
}