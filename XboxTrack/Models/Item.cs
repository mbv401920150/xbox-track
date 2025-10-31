namespace XboxTrack.Models;

public record Item
{
    public bool IsAttnReqd { get; set; }
    public string? ItemTypeName { get; set; }
    public bool ShouldShowManageSubsLink { get; set; }
    public string? LineItemId { get; set; }
    public string? LocalStatus { get; set; }
    public string? RedemptionLinkText { get; set; }
    public RelatedItems? RelatedItems { get; set; }
    public string? ProductId { get; set; }
    public bool IsRefundEligible { get; set; }
    public bool ShowRefundInformationLink { get; set; }
    public bool ShowRefundInformationLinkGeneric { get; set; }
    public int QuantityEligibleForReturn { get; set; }
    public int BundleSlotType { get; set; }
    public bool IsDeepDiscounted { get; set; }
    public bool IsPurchased { get; set; }
    public bool ShowGiftCodeLink { get; set; }
    public string? DebugData { get; set; }
    public bool IsCanceled { get; set; }
    public bool IsCharged { get; set; }
    public bool IsXPriceOrder { get; set; }
    public bool IsPreorder { get; set; }
    public string? LocalTitle { get; set; }
    public string? ItemState { get; set; }
    public string? LogoLink { get; set; }
    public string? LogoColor { get; set; }
    public string? ProductOrParentLink { get; set; }
    public string? PartnerId { get; set; }
    public string? PartnerName { get; set; }
    public bool IsSubscription { get; set; }
    public int Quantity { get; set; }
    public int CancellableQuantity { get; set; }
    public bool IsPaymentUpdatable { get; set; }
    public bool IsPaymentUpdatableWithoutPiCheck { get; set; }
    public string? TotalListPrice { get; set; }
    public string? TotalAmountWithoutTax { get; set; }
    public bool ShouldShowStrikeOffListPrice { get; set; }
    public bool IsFeeInclusive { get; set; }
    public bool IsReturnEligible { get; set; }
    public bool IsReturned { get; set; }
    public bool HasBackupPi { get; set; }
    public bool IsBOPISItem { get; set; }
    public bool IsGift { get; set; }
    public bool IsAskToBuy { get; set; }
    public bool IsAssociatedWithCurrentUser { get; set; }
    public string? Description { get; set; }
    public string? UpdatePiLink { get; set; }
}

