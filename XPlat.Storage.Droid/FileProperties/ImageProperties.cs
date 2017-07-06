namespace XPlat.Storage.FileProperties
{
    using System;
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
            var attrLat = this.exifInterface.GetAttribute(ExifInterface.TagGpsLatitude);
            var attrLatRef = this.exifInterface.GetAttribute(ExifInterface.TagGpsLatitudeRef);

            if (!string.IsNullOrWhiteSpace(attrLat) && !string.IsNullOrWhiteSpace(attrLatRef))
            {
                return attrLatRef.Equals("N") ? ConvertToDegrees(attrLat) : 0 - ConvertToDegrees(attrLat);
            }

            return null;
        }

        private double? GetLongitude()
        {
            var attrLong = this.exifInterface.GetAttribute(ExifInterface.TagGpsLongitude);
            var attrLongRef = this.exifInterface.GetAttribute(ExifInterface.TagGpsLongitudeRef);

            if (!string.IsNullOrWhiteSpace(attrLong) && !string.IsNullOrWhiteSpace(attrLongRef))
            {
                return attrLongRef.Equals("E") ? ConvertToDegrees(attrLong) : 0 - ConvertToDegrees(attrLong);
            }

            return null;
        }

        private static double ConvertToDegrees(string stringDms)
        {
            var dms = stringDms.Split(',');

            var stringD = dms[0].Split('/');
            var d0 = double.Parse(stringD[0]);
            var d1 = double.Parse(stringD[1]);
            var floatD = d0 / d1;

            var stringM = dms[1].Split('/');
            var m0 = double.Parse(stringM[0]);
            var m1 = double.Parse(stringM[1]);
            var floatM = m0 / m1;

            var stringS = dms[2].Split('/');
            var s0 = double.Parse(stringS[0]);
            var s1 = double.Parse(stringS[1]);
            var floatS = s0 / s1;

            var result = floatD + (floatM / 60) + (floatS / 3600);
            return result;
        }
    }
}