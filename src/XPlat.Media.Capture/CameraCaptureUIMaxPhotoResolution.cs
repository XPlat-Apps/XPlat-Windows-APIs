namespace XPlat.Media.Capture
{
    /// <summary>Determines the highest resolution the user can select for capturing photos.</summary>
    public enum CameraCaptureUIMaxPhotoResolution
    {
        /// <summary>The user can select any resolution.</summary>
        HighestAvailable,

        /// <summary>The user can select resolutions up to 320 X 240, or a similar 16:9 resolution.</summary>
        VerySmallQvga,

        /// <summary>The user can select resolutions up to 320 X 240, or a similar 16:9 resolution.</summary>
        SmallVga,

        /// <summary>The user can select resolutions up to 1024 X 768, or a similar 16:9 resolution.</summary>
        MediumXga,

        /// <summary>The user can select resolutions up to 1920 X 1080, or a similar 4:3 resolution.</summary>
        Large3M,

        /// <summary>The user can select resolutions up to 5MP.</summary>
        VeryLarge5M,
    }
}