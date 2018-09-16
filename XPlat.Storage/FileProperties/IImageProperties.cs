namespace XPlat.Storage.FileProperties
{
    using System.Collections.Generic;

    /// <summary>Provides access to the image-related properties of an item (like a file or folder).</summary>
    public interface IImageProperties
    {
        /// <summary>Gets the title of the image.</summary>
        string Title { get; }

        /// <summary>Gets the rating associated with an image file.</summary>
        int Rating { get; }

        /// <summary>Gets the date when the image was taken.</summary>
        string DateTaken { get; }

        /// <summary>Gets the model of the camera that took the photo.</summary>
        string CameraModel { get; }

        /// <summary>Gets the manufacturer of the camera that took the photo.</summary>
        string CameraManufacturer { get; }

        /// <summary>Gets the latitude coordinate where the photo was taken.</summary>
        double? Latitude { get; }

        /// <summary>Gets the longitude coordinate where the photo was taken.</summary>
        double? Longitude { get; }

        /// <summary>Gets the Exchangeable Image File (EXIF) orientation flag of the photo.</summary>
        PhotoOrientation Orientation { get; }

        /// <summary>Gets the names of people who are tagged in the photo.</summary>
        IReadOnlyList<string> PeopleNames { get; }

        /// <summary>Gets the height of the image.</summary>
        int Height { get; }

        /// <summary>Gets the collection of keywords associated with the image.</summary>
        IList<string> Keywords { get; }

        /// <summary>Gets the width of the image.</summary>
        int Width { get; }
    }
}