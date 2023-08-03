
using Microsoft.Maui.Graphics.Platform;

namespace TaskyApp.Contracts.Services;

public class ImageMediaInfo : IImageMediaInfo
{
    private readonly FileResult _fileResult;

    public ImageMediaInfo(FileResult fileResult)
    {
        _fileResult = fileResult;
        ContentType = _fileResult.ContentType;
        FileName = _fileResult.FileName;
        FullPath = _fileResult.FullPath;
    }

    public Task<Stream> GetStream() => _fileResult.OpenReadAsync();

    public string ContentType { get; }
    public string FileName { get; }
    public string FullPath { get; }
    public async Task<Stream> GetDownsizedStream(float maxWidthOrHeight)
    {
        var stream = await GetStream();

        var platformImage = PlatformImage.FromStream(stream);
        var downSizedImage = platformImage.Downsize(maxWidthOrHeight);

        var downsizedMemoryStream = new MemoryStream();
        await downSizedImage.SaveAsync(downsizedMemoryStream, quality:0.9F);
        downsizedMemoryStream.Position = 0; // reset to start of stream

        return downsizedMemoryStream;
    }
}