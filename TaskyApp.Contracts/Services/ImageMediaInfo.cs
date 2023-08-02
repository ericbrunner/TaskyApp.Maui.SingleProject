
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
}