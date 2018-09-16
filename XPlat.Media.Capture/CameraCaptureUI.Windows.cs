#if WINDOWS_UWP
namespace XPlat.Media.Capture
{
    using System;
    using System.Threading.Tasks;
    using XPlat.Media.Capture.Extensions;
    using XPlat.Storage;

    /// <summary>Provides a full window UI for capturing video and photos from a camera.</summary>
    public class CameraCaptureUI : ICameraCaptureUI
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CameraCaptureUI"/> class.
        /// </summary>
        public CameraCaptureUI()
        {
            this.PhotoSettings = new CameraCaptureUIPhotoCaptureSettings();
            this.VideoSettings = new CameraCaptureUIVideoCaptureSettings();
        }

        /// <summary>Provides settings for capturing photos.</summary>
        public CameraCaptureUIPhotoCaptureSettings PhotoSettings { get; }

        /// <summary>Provides settings for capturing videos. The settings include maximum resolution, maximum duration, and whether or not to allow trimming.</summary>
        public CameraCaptureUIVideoCaptureSettings VideoSettings { get; }

        /// <summary>Launches the CameraCaptureUI user interface.</summary>
        /// <returns>When this operation completes, an IStorageFile object is returned.</returns>
        /// <param name="mode">Specifies whether the user interface that will be shown allows the user to capture a photo, capture a video, or capture both photos and videos.</param>
        public async Task<IStorageFile> CaptureFileAsync(CameraCaptureUIMode mode)
        {
            Windows.Media.Capture.CameraCaptureUI dialog = new Windows.Media.Capture.CameraCaptureUI();
            dialog.PhotoSettings.AllowCropping = this.PhotoSettings.AllowCropping;
            dialog.PhotoSettings.MaxResolution = this.PhotoSettings.MaxResolution.ToWindowsCameraCaptureUIMaxPhotoResolution();
            dialog.VideoSettings.MaxResolution = this.VideoSettings.MaxResolution.ToWindowsCameraCaptureUIMaxVideoResolution();
            dialog.VideoSettings.AllowTrimming = this.VideoSettings.AllowTrimming;
            dialog.VideoSettings.MaxDurationInSeconds = this.VideoSettings.MaxDurationInSeconds;

            Windows.Storage.StorageFile file = null;

            switch (mode)
            {
                case CameraCaptureUIMode.PhotoOrVideo:
                    file = await dialog.CaptureFileAsync(Windows.Media.Capture.CameraCaptureUIMode.PhotoOrVideo);
                    break;
                case CameraCaptureUIMode.Photo:
                    file = await dialog.CaptureFileAsync(Windows.Media.Capture.CameraCaptureUIMode.Photo);
                    break;
                case CameraCaptureUIMode.Video:
                    file = await dialog.CaptureFileAsync(Windows.Media.Capture.CameraCaptureUIMode.Video);
                    break;
            }

            return file == null ? null : await StorageFile.GetFileFromPathAsync(file.Path);
        }
    }
}
#endif