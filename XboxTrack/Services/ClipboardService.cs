using Microsoft.JSInterop;

namespace XboxTrack.Services;

public class ClipboardService(IJSRuntime jsInterop) : IClipboardService
{
    public async Task CopyToClipboard(string text)
    {
        await jsInterop.InvokeVoidAsync("navigator.clipboard.writeText", text);
    }
}