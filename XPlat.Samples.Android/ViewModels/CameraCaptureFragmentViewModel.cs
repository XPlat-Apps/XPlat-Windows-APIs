using System.Threading.Tasks;
using CommonServiceLocator;
using MADE.App.Views.Dialogs;
using MADE.App.Views.Navigation.ViewModels;
using XPlat.Exceptions;
using XPlat.Media.Capture;
using XPlat.Storage;

namespace XPlat.Samples.Android.ViewModels
{
    public class CameraCaptureFragmentViewModel : PageViewModel
    {
        private IStorageFile image;
        private IStorageFile video;

        public ICameraCaptureUI CameraCaptureUI { get; set; }

        public async Task CaptureImageAsync()
        {
            if (CameraCaptureUI != null)
            {
                try
                {
                    this.image = await CameraCaptureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

                    System.Diagnostics.Debug.WriteLine(image != null
                        ? $"Image captured at '{this.image.Path}'."
                        : "Image capture cancelled by the user.");
                }
                catch (AppPermissionInvalidException apie)
                {
                    System.Diagnostics.Debug.WriteLine(apie.ToString());
                }
            }
        }

        public async Task CaptureVideoAsync()
        {
            if (CameraCaptureUI != null)
            {
                try
                {
                    this.video = await CameraCaptureUI.CaptureFileAsync(CameraCaptureUIMode.Video);

                    System.Diagnostics.Debug.WriteLine(video != null
                        ? $"Video captured at '{this.video.Path}'."
                        : "Video capture cancelled by the user.");
                }
                catch (AppPermissionInvalidException apie)
                {
                    System.Diagnostics.Debug.WriteLine(apie.ToString());
                }
            }
        }
    }
}