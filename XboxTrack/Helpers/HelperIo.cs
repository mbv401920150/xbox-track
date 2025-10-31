using System.Text.Json;

namespace XboxTrack.Helpers;

public static class HelperIo
{
    public static T? ConvertJsonContent<T>(string jsonContent)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<T>(jsonContent, options);
    }
}