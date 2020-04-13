namespace XPlat.Media.Capture
{
    /// <summary>Determines the highest resolution the user can select for capturing video.</summary>
    public enum CameraCaptureUIMaxVideoResolution
    {
        /// <summary>The user can select any resolution.</summary>
        HighestAvailable,

        /// <summary>The user can select resolutions up to low definition resolutions.</summary>
        LowDefinition,

        /// <summary>The user can select resolutions up to standard definition resolutions.</summary>
        StandardDefinition,

        /// <summary>The user can select resolutions up to high definition resolutions.</summary>
        HighDefinition,
    }
}