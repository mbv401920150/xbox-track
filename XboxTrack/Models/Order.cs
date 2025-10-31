namespace XboxTrack.Models;

public record Order
{
    public string? CidHex { get; set; }
    public string? LocalSubmittedDate { get; set; }
    public bool HasAuthFailedItem { get; set; }
    public bool IsSingleDigitalPreorder { get; set; }
    public bool HasPreorder { get; set; }
    public bool HasMultipleItems { get; set; }
    public bool HasPhysicalItem { get; set; }
    public bool HasDigitalItem { get; set; }
    public string? OrderId { get; set; }
    public int ControlsOrderType { get; set; }
    public string? VanityOrderId { get; set; }
    public string? LocalVanityOrderId { get; set; }
    public string? SupportLinkId { get; set; }
    public string? LocalTotal { get; set; }
    public double LocalTotalInDecimal { get; set; }
    public string? LocalPaymentInstrumentNames { get; set; }
    public string? Market { get; set; }
    public CurrencyInfo? CurrencyInfo { get; set; }
    public bool IsEUMarket { get; set; }
    public bool IsShippingAddress { get; set; }
    public Address? Address { get; set; }
    public List<Item>? Items { get; set; }
}