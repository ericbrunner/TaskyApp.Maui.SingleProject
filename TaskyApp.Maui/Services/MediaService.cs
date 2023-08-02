using TaskyApp.Contracts.Services;

namespace TaskyApp.Maui.SingleProject.Services;

public class MediaService : IMediaService
{
    public async Task<IEnumerable<IImageMediaInfo>> PickImages(string? title = null)
    {
        var imageMediaInfos = new List<IImageMediaInfo>();

        var pickerOptions = new PickOptions
        {
            PickerTitle = title ?? "Pick Image Please",
            FileTypes = FilePickerFileType.Images
        };

        var fileResults = await FilePicker.PickMultipleAsync(pickerOptions);

        foreach (var fileResult in fileResults)
        {
            imageMediaInfos.Add(new ImageMediaInfo(fileResult));
        }
        
        return imageMediaInfos;
    }
}