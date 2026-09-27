namespace PrintVault.Backend.DTOs.Model.Request;

public class UpdateModelDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsFavorite { get; set; }
    public int? CategoryId { get; set; }
    public bool? RemoveCategory { get; set; }
    public List<int>? TagIds { get; set; }
}