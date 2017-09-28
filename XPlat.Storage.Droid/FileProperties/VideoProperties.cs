namespace XPlat.Storage.FileProperties
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using Android.Media;

    using XPlat.Helpers;

    public class VideoProperties : IVideoProperties
    {
        private readonly MediaMetadataRetriever mediaMetadataRetriever;

        internal VideoProperties(string filePath)
        {
            this.mediaMetadataRetriever = new MediaMetadataRetriever();
            this.mediaMetadataRetriever.SetDataSource(filePath);
        }

        public int Year => ParseHelper.SafeParseInt(this.mediaMetadataRetriever.ExtractMetadata(MetadataKey.Year));

        public string Title => this.mediaMetadataRetriever.ExtractMetadata(MetadataKey.Title);

        public string Subtitle { get; }

        public string Publisher { get; }

        public int Rating { get; }

        public double? Latitude => this.GetLatitude();

        public VideoOrientation Orientation => this.GetVideoOrientation();

        public TimeSpan Duration => TimeSpan.FromMilliseconds(
            ParseHelper.SafeParseDouble(this.mediaMetadataRetriever.ExtractMetadata(MetadataKey.Duration)));

        public int Bitrate => ParseHelper.SafeParseInt(
            this.mediaMetadataRetriever.ExtractMetadata(MetadataKey.Bitrate));

        public IList<string> Producers { get; }

        public IList<string> Directors { get; }

        public int Height => ParseHelper.SafeParseInt(
            this.mediaMetadataRetriever.ExtractMetadata(MetadataKey.VideoHeight));

        public int Width => ParseHelper.SafeParseInt(
            this.mediaMetadataRetriever.ExtractMetadata(MetadataKey.VideoWidth));

        public double? Longitude => this.GetLongitude();

        public IList<string> Writers => new List<string>
                                            {
                                                this.mediaMetadataRetriever.ExtractMetadata(
                                                    MetadataKey.Writer)
                                            };

        public IList<string> Keywords { get; }

        private double? GetLatitude()
        {
            const string RegexPattern = @"[+-]\d{1,3}.\d{0,10}";
            Regex regex = new Regex(RegexPattern);

            string latLong = this.mediaMetadataRetriever.ExtractMetadata(MetadataKey.Location);
            MatchCollection matches = regex.Matches(latLong);
            if (matches.Count >= 2)
            {
                return ParseHelper.SafeParseDouble(matches[0]);
            }

            return null;
        }

        private double? GetLongitude()
        {
            const string RegexPattern = @"[+-]\d{1,3}.\d{0,10}";
            Regex regex = new Regex(RegexPattern);

            string latLong = this.mediaMetadataRetriever.ExtractMetadata(MetadataKey.Location);
            MatchCollection matches = regex.Matches(latLong);
            if (matches.Count >= 2)
            {
                return ParseHelper.SafeParseDouble(matches[1]);
            }

            return null;
        }

        private VideoOrientation GetVideoOrientation()
        {
            int orientation = ParseHelper.SafeParseInt(
                this.mediaMetadataRetriever.ExtractMetadata(MetadataKey.VideoRotation));

            switch (orientation)
            {
                case 90: return VideoOrientation.Rotate90;
                case 180: return VideoOrientation.Rotate180;
                case 270: return VideoOrientation.Rotate270;
            }

            return VideoOrientation.Normal;
        }
    }
}