namespace XboxTrack.Services;

public interface IClipboardService
{
    Task CopyToClipboard(string text);
}