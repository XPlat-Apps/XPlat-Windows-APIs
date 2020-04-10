namespace XPlat.Media.Capture
{
    /// <summary>Provides settings for capturing videos. The settings include maximum resolution, maximum duration, and whether or not to allow trimming.</summary>
    public interface ICameraCaptureUIVideoCaptureSettings
    {
        /// <summary>Determines the maximum resolution that the user can select.</summary>
        CameraCaptureUIMaxVideoResolution MaxResolution { get; set; }

        /// <summary>Determines the maximum duration of a video.</summary>
        float MaxDurationInSeconds { get; set; }

        /// <summary>Determines whether or not the video trimming user interface will be enabled.</summary>
        bool AllowTrimming { get; set; }
    }
}