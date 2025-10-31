namespace XboxTrack.Models;

public record CurrencyInfo
{
    public string? CurrencyCode { get; set; }
    public string? CurrencyCulture { get; set; }
    public string? Market { get; set; }
}