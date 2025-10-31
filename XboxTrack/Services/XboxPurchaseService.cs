using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using XboxTrack.Helpers;
using XboxTrack.Models;

namespace XboxTrack.Services;

public class XboxPurchaseService : IXboxPurchaseService
{
    private List<CountryData>? CountryData { get; set; }
    private List<XboxPurchaseHistory> GeneralHistoryInfo { get; set; } = new();

    private void ReadHistoryItem(RootOrderObject historyItem, string userName)
    {
        historyItem.Orders?.ForEach(order => ReadOrder(order, userName));
    }

    private double ExchangeRate(string currCode) =>
        CountryData?.FirstOrDefault(x => x.CurrencyCode == currCode)?.ExchangeRate ?? 1;

    private void ReadOrder(Order order, string userName)
    {
        var currCode = order.CurrencyInfo?.CurrencyCode ?? "N/A";
        DateTime.TryParse(order.LocalSubmittedDate, out var date);

        if (order.Items is null)
        {
            return;
        }

        foreach (var orderItem in order.Items)
        {
            var price = Regex.Replace(orderItem.TotalListPrice ?? "", "[^0-9.,]", "");

            var amount = BaseHelpers.ParseAmount(currCode, price, CountryData);

            var link = orderItem.ProductOrParentLink;
            var gameDescription = orderItem
                .Description?
                .RemoveSpecialChar()
                .RemoveAccents();

            if (string.IsNullOrEmpty(gameDescription))
            {
                gameDescription = orderItem.LocalTitle?.RemoveSpecialChar().RemoveAccents();
            }

            var productId = orderItem.ProductId;

            var type = orderItem.ItemTypeName;

            var usdPrice = amount / ExchangeRate(currCode);

            var itemsInPack = orderItem.RelatedItems is not null
                ? orderItem
                    .RelatedItems
                    .Items?
                    .OrderBy(x => x.Title)
                    .Select(x => x.Title?.RemoveSpecialChar().RemoveAccents() ?? "")
                : Array.Empty<string>();

            var status = orderItem.ItemState;
            var logoLink = orderItem.LogoLink;

            var duplicateItems = GeneralHistoryInfo.Where(x =>
                x.ProductId == productId &&
                x.User == userName).ToList();

            if (duplicateItems.Any())
            {
                var moveToNextItem = false;

                foreach (var duplicateItem in duplicateItems)
                {
                    if (duplicateItem.Date > date)
                    {
                        moveToNextItem = true;
                        continue;
                    }

                    GeneralHistoryInfo.Remove(duplicateItem);
                }

                if (moveToNextItem)
                {
                    continue;
                }
            }

            GeneralHistoryInfo.Add(new XboxPurchaseHistory
            {
                User = userName,
                ProductId = productId,
                Description = gameDescription,
                Type = type,
                Price = amount,
                Currency = currCode,
                UsdPrice = usdPrice,
                Date = date,
                Status = status,
                ItemsInPack = itemsInPack,
                Link = link,
                LinkImage = logoLink
            });
        }
    }

    private async Task<string> GetContentAsStringAsync(ZipArchiveEntry entry)
    {
        await using var entryStream = entry.Open();
        using var reader = new StreamReader(entryStream);
        return await reader.ReadToEndAsync();
    }

    public async Task<List<XboxPurchaseHistory>> ReadHistoryPerUsers(MemoryStream memoryStream)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        try
        {
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Read))
            {
                var countryData =
                    archive.Entries.FirstOrDefault(x => x.Name.Contains("countryData.json", StringComparison.OrdinalIgnoreCase));

                if (countryData is not null)
                {
                    var jsonContent = await GetContentAsStringAsync(countryData);

                    CountryData = HelperIo.ConvertJsonContent<List<CountryData>>(jsonContent);
                }

                var purchaseFiles = archive
                    .Entries
                    .Where(x => x.FullName.Contains("purchase", StringComparison.OrdinalIgnoreCase) &&
                                x.Name.Contains(".json", StringComparison.OrdinalIgnoreCase))
                    .OrderBy(x => x.FullName);

                foreach (var purchaseFile in purchaseFiles)
                {
                    var fileInfo = new FileInfo(purchaseFile.FullName);

                    List<RootOrderObject> allItems = new();

                    try
                    {
                        var jsonString = await GetContentAsStringAsync(purchaseFile);
                        var rootOrderObject = JsonSerializer.Deserialize<RootOrderObject>(jsonString, options);

                        if (rootOrderObject is not null)
                        {
                            allItems.Add(rootOrderObject);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Write($"Error in: {purchaseFile.FullName} | {e}");
                        throw;
                    }

                    var historyItems = allItems;

                    historyItems.ForEach(items => ReadHistoryItem(items, fileInfo.Directory?.Name ?? "_DEFAULT"));
                }
            }

            GeneralHistoryInfo = GeneralHistoryInfo
                .OrderByDescending(x => x.Date)
                .ThenBy(x => x.Description)
                .ToList();

            return GeneralHistoryInfo;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task SaveToFile()
    {
        var results = string.Join("\r\n", GeneralHistoryInfo);

        await using var sw = new StreamWriter("./Xbox Games.csv", false, new UTF8Encoding(true));
        await sw.WriteAsync(results);
        sw.Close();
    }
}