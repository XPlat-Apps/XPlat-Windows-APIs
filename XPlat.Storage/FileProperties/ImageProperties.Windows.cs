#if WINDOWS_UWP
namespace XPlat.Storage.FileProperties
{
    using System.Collections.Generic;

    /// <summary>Provides access to the image-related properties of an item (like a file or folder).</summary>
    internal class ImageProperties : IImageProperties
    {
        public ImageProperties(Windows.Storage.FileProperties.ImageProperties props)
        {
            this.Title = props.Title;
            this.Rating = (int) props.Rating;
            this.DateTaken = props.DateTaken.ToString();
            this.CameraModel = props.CameraModel;
            this.CameraManufacturer = props.CameraManufacturer;
            this.Latitude = props.Latitude;
            this.Longitude = props.Longitude;
            this.Orientation = (PhotoOrientation) props.Orientation;
            this.PeopleNames = props.PeopleNames;
            this.Height = (int) props.Height;
            this.Keywords = props.Keywords;
            this.Width = (int) props.Width;
        }

        /// <summary>Gets the title of the image.</summary>
        public string Title { get; }

        /// <summary>Gets the rating associated with an image file.</summary>
        public int Rating { get; }

        /// <summary>Gets the date when the image was taken.</summary>
        public string DateTaken { get; }

        /// <summary>Gets the model of the camera that took the photo.</summary>
        public string CameraModel { get; }

        /// <summary>Gets the manufacturer of the camera that took the photo.</summary>
        public string CameraManufacturer { get; }

        /// <summary>Gets the latitude coordinate where the photo was taken.</summary>
        public double? Latitude { get; }

        /// <summary>Gets the longitude coordinate where the photo was taken.</summary>
        public double? Longitude { get; }

        /// <summary>Gets the Exchangeable Image File (EXIF) orientation flag of the photo.</summary>
        public PhotoOrientation Orientation { get; }

        /// <summary>Gets the names of people who are tagged in the photo.</summary>
        public IReadOnlyList<string> PeopleNames { get; }

        /// <summary>Gets the height of the image.</summary>
        public int Height { get; }

        /// <summary>Gets the collection of keywords associated with the image.</summary>
        public IList<string> Keywords { get; }

        /// <summary>Gets the width of the image.</summary>
        public int Width { get; }

        public static implicit operator ImageProperties(Windows.Storage.FileProperties.ImageProperties props)
        {
            return new ImageProperties(props);
        }
    }
}
#endif