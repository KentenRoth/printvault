using System.Net;
using System.Text.RegularExpressions;

namespace PrintVault.Backend.Helpers;

public static class HtmlDescriptionHelper
{
    public static string Normalize(string? description)
    {
        if (string.IsNullOrWhiteSpace(description)) return string.Empty;
        
        if (Regex.IsMatch(description, @"</?[a-zA-Z]")) return description;
        if (Regex.IsMatch(description, @"&lt;/?[a-zA-Z]")) return WebUtility.HtmlDecode(description); 
        
        return description;
    }
}