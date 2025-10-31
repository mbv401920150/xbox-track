using XboxTrack.Models;
using XboxTrack.Services;

namespace XboxTrack.ViewModels;

public class PageIndexViewModel(IXboxPurchaseService xboxPurchaseService)
{
    private void NotifyStateChanged() => OnChange?.Invoke();
    public event Action? OnChange;
    
    public bool IsLoading { get; set; }
    public List<XboxPurchaseHistory> PurchaseHistory { get; set; } = [];
    
    public async Task GetPurchaseItems(MemoryStream memoryStream)
    {
        try
        {
            IsLoading = true;
            NotifyStateChanged();
            PurchaseHistory = await xboxPurchaseService.ReadHistoryPerUsers(memoryStream);
        }
        finally
        {
            IsLoading = false;
            NotifyStateChanged();
        }
    }
}