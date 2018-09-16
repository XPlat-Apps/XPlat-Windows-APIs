namespace XPlat.Storage.FileProperties
{
    /// <summary>Indicates the Exchangeable Image File (EXIF) orientation flag of the photo. This flag describes how to rotate the photo to display it correctly.</summary>
    public enum PhotoOrientation
    {
        /// <summary>An orientation flag is not set.</summary>
        Unspecified,

        /// <summary>No rotation needed. The photo can be displayed using its current orientation.</summary>
        Normal,

        /// <summary>Flip the photo horizontally.</summary>
        FlipHorizontal,

        /// <summary>Rotate the photo 180 degrees.</summary>
        Rotate180,

        /// <summary>Flip the photo vertically.</summary>
        FlipVertical,

        /// <summary>Rotate the photo counter-clockwise 90 degrees and then flip it horizontally.</summary>
        Transpose,

        /// <summary>Rotate the photo counter-clockwise 270 degrees.</summary>
        Rotate270,

        /// <summary>Rotate the photo counter-clockwise 270 degrees and then flip it horizontally.</summary>
        Transverse,

        /// <summary>Rotate the photo counter-clockwise 90 degrees.</summary>
        Rotate90,
    }
}