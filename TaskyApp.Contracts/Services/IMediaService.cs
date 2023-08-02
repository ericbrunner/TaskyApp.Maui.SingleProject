namespace TaskyApp.Contracts.Services;

public interface IMediaService
{
    Task<IEnumerable<IImageMediaInfo>> PickImages(string title = null);
}

public interface IImageMediaInfo
{
    Task<Stream> GetStream();
    string ContentType { get; }
    string FileName { get; }
    string FullPath { get; }
    Task<Stream> GetDownsizedStream(float maxWidthOrHeight);
}