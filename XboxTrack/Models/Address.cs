namespace XboxTrack.Models;

public record Address
{
    public string? Id { get; set; }
    public string? Addressee { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? State { get; set; }
    public string? Street1 { get; set; }
    public string? Street2 { get; set; }
    public string? Street3 { get; set; }
    public string? RegionName { get; set; }
}