namespace XPlat.Media.Capture
{
    using System;
    using System.Threading.Tasks;

    using Windows.Media.Capture;

    using XPlat.Storage;

    public class CameraCaptureUI : ICameraCaptureUI
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CameraCaptureUI"/> class.
        /// </summary>
        public CameraCaptureUI()
        {
            this.PhotoSettings = new CameraCaptureUIPhotoCaptureSettings();
        }

        /// <inheritdoc />
        public async Task<IStorageFile> CaptureFileAsync(CameraCaptureUIMode mode)
        {
            var dialog = new Windows.Media.Capture.CameraCaptureUI();
            dialog.PhotoSettings.AllowCropping = this.PhotoSettings.AllowCropping;
            dialog.PhotoSettings.MaxResolution = this.PhotoSettings.MaxResolution.ToCameraCaptureUIMaxPhotoResolution();
            dialog.VideoSettings.MaxResolution = CameraCaptureUIMaxVideoResolution.StandardDefinition;
            dialog.VideoSettings.AllowTrimming = false;

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

        /// <inheritdoc />
        public CameraCaptureUIPhotoCaptureSettings PhotoSettings { get; }
    }
}