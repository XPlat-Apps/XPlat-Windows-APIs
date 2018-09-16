namespace XPlat.Media.Capture
{
    /// <summary>Provides settings for capturing photos.</summary>
    public interface ICameraCaptureUIPhotoCaptureSettings
    {
        /// <summary>Determines the maximum resolution the user will be able to select.</summary>
        CameraCaptureUIMaxPhotoResolution MaxResolution { get; set; }

        /// <summary>Determines whether photo cropping will be enabled in the user interface for capture a photo.</summary>
        bool AllowCropping { get; set; }
    }
}