namespace XPlat.Media.Capture
{
    /// <summary>
    /// Determines the highest resolution the user can select for capturing photos.
    /// </summary>
    public enum CameraCaptureUIMaxPhotoResolution
    {
        /// <summary>
        /// Default, will return the image at the default settings of the camera capture user interface.
        /// </summary>
        HighestAvailable,

        /// <summary>
        /// Will return the image up to a resolution of 320x240, or a similar 16:9 resolution.
        /// </summary>
        VerySmallQvga,

        /// <summary>
        /// Will return the image up to a resolution of 640x480, or a similar 16:9 resolution.
        /// </summary>
        SmallVga,

        /// <summary>
        /// Will return the image up to a resolution of 1024x768, or a similar 16:9 resolution.
        /// </summary>
        MediumXga,

        /// <summary>
        /// Will return the image up to a resolution of 1920x1080, or a similar 16:9 resolution.
        /// </summary>
        Large3M,

        /// <summary>
        /// Will return the image up to a resolution of 2560x1920, or a similar 16:9 resolution.
        /// </summary>
        VeryLarge5M
    }
}