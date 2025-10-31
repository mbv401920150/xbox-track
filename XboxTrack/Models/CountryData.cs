namespace XboxTrack.Models;

public record CountryData
{
    public string? CurrencyCode { get; set; }
    public double ExchangeRate { get; set; }
    public string? Culture { get; set; }
}