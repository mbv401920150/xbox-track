using XboxTrack.Models;

namespace XboxTrack.Services;

public interface IXboxPurchaseService
{
    Task<List<XboxPurchaseHistory>> ReadHistoryPerUsers(MemoryStream memoryStream);
    Task SaveToFile();
}