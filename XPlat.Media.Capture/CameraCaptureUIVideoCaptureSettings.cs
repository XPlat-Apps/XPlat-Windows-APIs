namespace XPlat.Media.Capture
{
    /// <summary>Provides settings for capturing videos. The settings include maximum resolution, maximum duration, and whether or not to allow trimming.</summary>
    public class CameraCaptureUIVideoCaptureSettings : ICameraCaptureUIVideoCaptureSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CameraCaptureUIVideoCaptureSettings"/> class.
        /// </summary>
        public CameraCaptureUIVideoCaptureSettings()
        {
            this.MaxResolution = CameraCaptureUIMaxVideoResolution.HighestAvailable;
            this.AllowTrimming = false;
        }

        /// <summary>Determines the maximum resolution that the user can select.</summary>
        public CameraCaptureUIMaxVideoResolution MaxResolution { get; set; }

        /// <summary>Determines the maximum duration of a video.</summary>
        public float MaxDurationInSeconds { get; set; }

        /// <summary>Determines whether or not the video trimming user interface will be enabled.</summary>
        public bool AllowTrimming { get; set; }
    }
}