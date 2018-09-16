#if __ANDROID__
namespace XPlat.Storage.FileProperties
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using Android.Media;

    /// <summary>Provides access to the video-related properties of an item (like a file or folder).</summary>
    public class VideoProperties : IVideoProperties
    {
        private const string LatLongRegexPattern = @"[+-]\d{1,3}.\d{0,10}";

        internal VideoProperties(string filePath)
        {
            this.MediaMetadataRetriever = new MediaMetadataRetriever();
            this.MediaMetadataRetriever.SetDataSource(filePath);
        }

        public MediaMetadataRetriever MediaMetadataRetriever { get; }

        /// <summary>Gets the year that the video was shot or released.</summary>
        public int Year => int.TryParse(this.MediaMetadataRetriever.ExtractMetadata(MetadataKey.Year), out int parsedValue)
            ? parsedValue
            : 0;

        /// <summary>Gets the title of the video.</summary>
        public string Title => this.MediaMetadataRetriever.ExtractMetadata(MetadataKey.Title);

        /// <summary>Gets the subtitle of the video.</summary>
        public string Subtitle { get; }

        /// <summary>Gets the publisher of the video.</summary>
        public string Publisher { get; }

        /// <summary>Gets the rating associated with a video file.</summary>
        public int Rating { get; }

        /// <summary>Gets the latitude coordinate where the video was shot.</summary>
        public double? Latitude => this.GetLatitude();

        /// <summary>Gets a VideoOrientation value that indicates how the video should be rotated to display it correctly.</summary>
        public VideoOrientation Orientation => this.GetVideoOrientation();

        /// <summary>Gets the duration of the video.</summary>
        public TimeSpan Duration => TimeSpan.FromMilliseconds(
            double.TryParse(this.MediaMetadataRetriever.ExtractMetadata(MetadataKey.Duration), out double parsedValue)
                ? parsedValue
                : 0);

        /// <summary>Gets the sum audio and video bitrate of the video.</summary>
        public int Bitrate => int.TryParse(this.MediaMetadataRetriever.ExtractMetadata(MetadataKey.Bitrate), out int parsedValue)
            ? parsedValue
            : 0;

        /// <summary>Gets the producers of the video.</summary>
        public IList<string> Producers { get; }

        /// <summary>Gets the directors of the video.</summary>
        public IList<string> Directors { get; }

        /// <summary>Gets the height of the video.</summary>
        public int Height => int.TryParse(this.MediaMetadataRetriever.ExtractMetadata(MetadataKey.VideoHeight), out int parsedValue)
            ? parsedValue
            : 0;

        /// <summary>Gets the width of the video.</summary>
        public int Width => int.TryParse(this.MediaMetadataRetriever.ExtractMetadata(MetadataKey.VideoWidth), out int parsedValue)
            ? parsedValue
            : 0;

        /// <summary>Gets the longitude coordinate where the video was shot.</summary>
        public double? Longitude => this.GetLongitude();

        /// <summary>Gets the script writers for the video.</summary>
        public IList<string> Writers => new List<string>
                                            {
                                                this.MediaMetadataRetriever.ExtractMetadata(
                                                    MetadataKey.Writer)
                                            };

        /// <summary>Gets the collection of keywords associated with the video.</summary>
        public IList<string> Keywords { get; }

        private double? GetLatitude()
        {
            Regex regex = new Regex(LatLongRegexPattern);

            string latLong = this.MediaMetadataRetriever.ExtractMetadata(MetadataKey.Location);
            MatchCollection matches = regex.Matches(latLong);
            if (matches.Count >= 2)
            {
                return double.TryParse(matches[0].ToString(), out double parsedValue)
                    ? parsedValue
                    : 0;
            }

            return null;
        }

        private double? GetLongitude()
        {
            Regex regex = new Regex(LatLongRegexPattern);

            string latLong = this.MediaMetadataRetriever.ExtractMetadata(MetadataKey.Location);
            MatchCollection matches = regex.Matches(latLong);
            if (matches.Count >= 2)
            {
                return double.TryParse(matches[1].ToString(), out double parsedValue)
                    ? parsedValue
                    : 0;
            }

            return null;
        }

        private VideoOrientation GetVideoOrientation()
        {
            int orientation = int.TryParse(this.MediaMetadataRetriever.ExtractMetadata(MetadataKey.VideoRotation), out int parsedValue)
                ? parsedValue
                : 0;

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
#endif