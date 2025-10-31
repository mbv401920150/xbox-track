namespace XboxTrack.Models;

public record XboxPurchaseHistory
{
    public string? User { get; set; }
    public string? ProductId { get; set; }
    public string? Description { get; set; }
    public string? Type { get; set; }
    public double Price { get; set; }
    public string? Currency { get; set; }
    public double UsdPrice { get; set; }
    public DateTime Date { get; set; }
    public string? Status { get; set; }
    public IEnumerable<string>? ItemsInPack { get; set; }
    public string? Link { get; set; }
    public string? LinkImage { get; set; }
    public bool Completed { get; set; }
    public bool OnGoing { get; set; }
    public string? Notes { get; set; }
}