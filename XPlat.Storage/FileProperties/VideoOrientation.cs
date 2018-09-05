namespace XPlat.Storage.FileProperties
{
    /// <summary>Indicates how to rotate the video to display it correctly.</summary>
    public enum VideoOrientation
    {
        /// <summary>No rotation needed. The video can be displayed using its current orientation.</summary>
        Normal = 0,

        /// <summary>Rotate the video 90 degrees.</summary>
        Rotate90 = 90,

        /// <summary>Rotate the video counter-clockwise 180 degrees.</summary>
        Rotate180 = 180,

        /// <summary>Rotate the video counter-clockwise 270 degrees.</summary>
        Rotate270 = 270,
    }
}