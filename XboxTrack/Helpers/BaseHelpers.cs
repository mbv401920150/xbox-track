using System.Globalization;
using XboxTrack.Models;

namespace XboxTrack.Helpers;

public static class BaseHelpers
{
    private static string CultureBaseOnCountry(string currCode, List<CountryData>? CountryData) =>
        CountryData?.FirstOrDefault(x => x.CurrencyCode == currCode)?.Culture ?? "en-US";
    
    public static double ParseAmount(string currCode, string amount, List<CountryData>? CountryData)
    {
        if (amount == "")
        {
            return 0;
        }

        var culture = CultureBaseOnCountry(currCode, CountryData);

        if (double.TryParse(amount, NumberStyles.Any, CultureInfo.CreateSpecificCulture(culture), out var number) || 
            double.TryParse(amount, NumberStyles.Any, CultureInfo.InvariantCulture, out number) || 
            double.TryParse(amount, NumberStyles.Any, CultureInfo.CreateSpecificCulture("es-AR"), out number))
        {
            return Math.Round(number, 2);
        }

        throw new FormatException("Invalid number format.");
    }
}