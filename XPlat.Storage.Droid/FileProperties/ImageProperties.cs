namespace XPlat.Storage.FileProperties
{
    using System.Collections.Generic;

    using Android.Media;

    using XPlat.Helpers;

    internal class ImageProperties : IImageProperties
    {
        private readonly ExifInterface exifInterface;

        public ImageProperties(string filePath)
        {
            this.exifInterface = new ExifInterface(filePath);
        }

        public string Title { get; }

        public int Rating { get; }

        public string DateTaken => this.exifInterface.GetAttribute(ExifInterface.TagDatetime);

        public string CameraModel => this.exifInterface.GetAttribute(ExifInterface.TagModel);

        public string CameraManufacturer => this.exifInterface.GetAttribute(ExifInterface.TagMake);

        public double? Latitude => this.GetLatitude();

        public double? Longitude => this.GetLongitude();

        public PhotoOrientation Orientation => GetOrientation(
            this.exifInterface.GetAttributeInt(ExifInterface.TagOrientation, (int)Android.Media.Orientation.Undefined));

        public IReadOnlyList<string> PeopleNames { get; }

        public int Height => ParseHelper.SafeParseInt(this.exifInterface.GetAttribute(ExifInterface.TagImageLength));

        public IList<string> Keywords { get; }

        public int Width => ParseHelper.SafeParseInt(this.exifInterface.GetAttribute(ExifInterface.TagImageWidth));

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
            string attrLat = this.exifInterface.GetAttribute(ExifInterface.TagGpsLatitude);
            string attrLatRef = this.exifInterface.GetAttribute(ExifInterface.TagGpsLatitudeRef);

            if (!string.IsNullOrWhiteSpace(attrLat) && !string.IsNullOrWhiteSpace(attrLatRef))
            {
                return attrLatRef.Equals("N") ? ConvertToDegrees(attrLat) : 0 - ConvertToDegrees(attrLat);
            }

            return null;
        }

        private double? GetLongitude()
        {
            string attrLong = this.exifInterface.GetAttribute(ExifInterface.TagGpsLongitude);
            string attrLongRef = this.exifInterface.GetAttribute(ExifInterface.TagGpsLongitudeRef);

            if (!string.IsNullOrWhiteSpace(attrLong) && !string.IsNullOrWhiteSpace(attrLongRef))
            {
                return attrLongRef.Equals("E") ? ConvertToDegrees(attrLong) : 0 - ConvertToDegrees(attrLong);
            }

            return null;
        }

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
    }
}