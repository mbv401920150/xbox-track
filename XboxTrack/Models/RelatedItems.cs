namespace XboxTrack.Models;

public record RelatedItems
{
    public string? Title { get; set; }
    public List<RelatedItemsDetail> Items { get; set; } = [];
}
