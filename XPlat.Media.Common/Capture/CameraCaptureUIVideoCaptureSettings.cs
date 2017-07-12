namespace XPlat.Media.Capture
{
    /// <summary>
    /// Provides settings for capturing videos.
    /// </summary>
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

        /// <inheritdoc />
        public CameraCaptureUIMaxVideoResolution MaxResolution { get; set; }

        /// <inheritdoc />
        public bool AllowTrimming { get; set; }

        /// <inheritdoc />
        public float MaxDurationInSeconds {  get; set; }
    }
}
