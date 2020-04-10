namespace XPlat.Media.Capture
{
    /// <summary>Provides settings for capturing photos.</summary>
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

        /// <summary>Determines the maximum resolution the user will be able to select.</summary>
        public CameraCaptureUIMaxPhotoResolution MaxResolution { get; set; }

        /// <summary>Determines whether photo cropping will be enabled in the user interface for capture a photo.</summary>
        public bool AllowCropping { get; set; }
    }
}