#if __ANDROID__
namespace XPlat.Storage.FileProperties
{
    using System.Collections.Generic;

    using Android.Media;

    /// <summary>Provides access to the image-related properties of an item (like a file or folder).</summary>
    internal class ImageProperties : IImageProperties
    {
        public ImageProperties(string filePath)
        {
            this.ExifInterface = new ExifInterface(filePath);
        }

        public ExifInterface ExifInterface { get; }

        /// <summary>Gets the title of the image.</summary>
        public string Title { get; }

        /// <summary>Gets the rating associated with an image file.</summary>
        public int Rating { get; }

        /// <summary>Gets the date when the image was taken.</summary>
        public string DateTaken => this.ExifInterface.GetAttribute(ExifInterface.TagDatetime);

        /// <summary>Gets the model of the camera that took the photo.</summary>
        public string CameraModel => this.ExifInterface.GetAttribute(ExifInterface.TagModel);

        /// <summary>Gets the manufacturer of the camera that took the photo.</summary>
        public string CameraManufacturer => this.ExifInterface.GetAttribute(ExifInterface.TagMake);

        /// <summary>Gets the latitude coordinate where the photo was taken.</summary>
        public double? Latitude => this.GetLatitude();

        /// <summary>Gets the longitude coordinate where the photo was taken.</summary>
        public double? Longitude => this.GetLongitude();

        /// <summary>Gets the Exchangeable Image File (EXIF) orientation flag of the photo.</summary>
        public PhotoOrientation Orientation => GetOrientation(
            this.ExifInterface.GetAttributeInt(ExifInterface.TagOrientation, (int)Android.Media.Orientation.Undefined));

        /// <summary>Gets the names of people who are tagged in the photo.</summary>
        public IReadOnlyList<string> PeopleNames { get; }

        /// <summary>Gets the height of the image.</summary>
        public int Height => int.TryParse(this.ExifInterface.GetAttribute(ExifInterface.TagImageLength), out int parsedValue) ? parsedValue : 0;

        /// <summary>Gets the collection of keywords associated with the image.</summary>
        public IList<string> Keywords { get; }

        /// <summary>Gets the width of the image.</summary>
        public int Width => int.TryParse(this.ExifInterface.GetAttribute(ExifInterface.TagImageWidth), out int parsedValue) ? parsedValue : 0;

        private static double ConvertToDegrees(string stringDms)
        {
            string[] dms = stringDms.Split(',');

            string[] stringD = dms[0].Split('/');
            double d0 = double.Parse(stringD[0]);
            double d1 = double.Parse(stringD[1]);
            double floatD = d0 / d1;

            string[] stringM = dms[1].Split('/');
            double m0 = double.Parse(stringM[0]);
            double m1 = double.Parse(stringM[1]);
            double floatM = m0 / m1;

            string[] stringS = dms[2].Split('/');
            double s0 = double.Parse(stringS[0]);
            double s1 = double.Parse(stringS[1]);
            double floatS = s0 / s1;

            double result = floatD + (floatM / 60) + (floatS / 3600);
            return result;
        }

        private static PhotoOrientation GetOrientation(int orientationIdx)
        {
            switch (orientationIdx)
            {
                case (int)Android.Media.Orientation.Normal:
                    return PhotoOrientation.Normal;
                case (int)Android.Media.Orientation.FlipHorizontal:
                    return PhotoOrientation.FlipHorizontal;
                case (int)Android.Media.Orientation.Rotate180:
                    return PhotoOrientation.Rotate180;
                case (int)Android.Media.Orientation.FlipVertical:
                    return PhotoOrientation.FlipVertical;
                case (int)Android.Media.Orientation.Transpose:
                    return PhotoOrientation.Transpose;
                case (int)Android.Media.Orientation.Rotate90:
                    return PhotoOrientation.Rotate90;
                case (int)Android.Media.Orientation.Transverse:
                    return PhotoOrientation.Transverse;
                case (int)Android.Media.Orientation.Rotate270:
                    return PhotoOrientation.Rotate270;
            }

            return PhotoOrientation.Unspecified;
        }

        private double? GetLatitude()
        {
            string attrLat = this.ExifInterface.GetAttribute(ExifInterface.TagGpsLatitude);
            string attrLatRef = this.ExifInterface.GetAttribute(ExifInterface.TagGpsLatitudeRef);

            if (!string.IsNullOrWhiteSpace(attrLat) && !string.IsNullOrWhiteSpace(attrLatRef))
            {
                return attrLatRef.Equals("N") ? ConvertToDegrees(attrLat) : 0 - ConvertToDegrees(attrLat);
            }

            return null;
        }

        private double? GetLongitude()
        {
            string attrLong = this.ExifInterface.GetAttribute(ExifInterface.TagGpsLongitude);
            string attrLongRef = this.ExifInterface.GetAttribute(ExifInterface.TagGpsLongitudeRef);

            if (!string.IsNullOrWhiteSpace(attrLong) && !string.IsNullOrWhiteSpace(attrLongRef))
            {
                return attrLongRef.Equals("E") ? ConvertToDegrees(attrLong) : 0 - ConvertToDegrees(attrLong);
            }

            return null;
        }
    }
}
#endif