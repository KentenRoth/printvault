namespace PrintVault.Backend.Models;

public class MfData
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string CreationDate { get; set; } = string.Empty;
    public string ProfileTitle { get; set; } = string.Empty;
    public string ProfileDescription { get; set; } = string.Empty;
    public int NumberOfPlates { get; set; }
    public double TotalFilamentUsed { get; set; }
    public int TotalPrintTimeSeconds { get; set; }
    public List<PlateData> Plates { get; set; } = new();

}