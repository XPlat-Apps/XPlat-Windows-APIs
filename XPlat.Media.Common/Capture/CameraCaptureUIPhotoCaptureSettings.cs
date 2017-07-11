namespace XPlat.Media.Capture
{
    /// <summary>
    /// Provides settings for capturing photos.
    /// </summary>
    public class CameraCaptureUIPhotoCaptureSettings : ICameraCaptureUIPhotoCaptureSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CameraCaptureUIPhotoCaptureSettings"/> class.
        /// </summary>
        public CameraCaptureUIPhotoCaptureSettings()
        {
            this.MaxResolution = CameraCaptureUIMaxPhotoResolution.HighestAvailable;
            this.AllowCropping = false;
        }

        /// <inheritdoc />
        public CameraCaptureUIMaxPhotoResolution MaxResolution { get; set; }

        /// <inheritdoc />
        public bool AllowCropping { get; set; }
    }
}