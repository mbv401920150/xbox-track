namespace XboxTrack.Models;

public record RootOrderObject
{
    public string? ContinuationToken { get; set; }
    public int FilterChangeCount { get; set; }
    public List<Order>? Orders { get; set; }
}