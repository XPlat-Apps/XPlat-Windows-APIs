namespace XPlat.Samples.Android.ViewModels
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using global::Android.Content;

    using MADE.App.Views.Navigation.ViewModels;

    using XPlat.Storage;
    using XPlat.Storage.FileProperties;
    using XPlat.Storage.Pickers;

    public class FileCaptureFragmentViewModel : PageViewModel
    {
        private readonly List<string> fileTypes;

        public FileCaptureFragmentViewModel()
        {
            this.fileTypes = new List<string>()
                                 {
                                     ".mp4",
                                     ".wav",
                                     ".wma",
                                     ".mid",
                                     ".mp3",
                                     ".3gp",
                                     ".avi",
                                     ".flv",
                                     ".mov",
                                     ".mpg",
                                     ".wmv",
                                     ".jpg",
                                     ".png",
                                     ".gif",
                                     ".bmp",
                                     ".tif",
                                     ".tiff",
                                     ".txt",
                                     ".doc",
                                     ".docx",
                                     ".pdf",
                                     ".7z",
                                     ".rar",
                                     ".zip"
                                 };
        }

        public async Task<object> CaptureFileAsync()
        {
            FileOpenPicker picker = new FileOpenPicker();

            foreach (var fileType in this.fileTypes)
            {
                picker.FileTypeFilter.Add(fileType);
            }

            IStorageFile file = await picker.PickSingleFileAsync();
            IBasicProperties properties = await file.GetBasicPropertiesAsync();
            return file;
        }
    }
}