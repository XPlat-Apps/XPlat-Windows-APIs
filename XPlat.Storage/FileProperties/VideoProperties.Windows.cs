#if WINDOWS_UWP
namespace XPlat.Storage.FileProperties
{
    using System;
    using System.Collections.Generic;

    /// <summary>Provides access to the video-related properties of an item (like a file or folder).</summary>
    public class VideoProperties : IVideoProperties
    {
        public VideoProperties(Windows.Storage.FileProperties.VideoProperties props)
        {
            this.Year = (int) props.Year;
            this.Title = props.Title;
            this.Subtitle = props.Subtitle;
            this.Publisher = props.Publisher;
            this.Rating = (int) props.Rating;
            this.Latitude = props.Latitude;
            this.Orientation = (VideoOrientation) props.Orientation;
            this.Duration = props.Duration;
            this.Bitrate = (int) props.Bitrate;
            this.Producers = props.Producers;
            this.Directors = props.Directors;
            this.Height = (int) props.Height;
            this.Width = (int) props.Width;
            this.Longitude = props.Longitude;
            this.Writers = props.Writers;
            this.Keywords = props.Keywords;
        }

        /// <summary>Gets the year that the video was shot or released.</summary>
        public int Year { get; }

        /// <summary>Gets the title of the video.</summary>
        public string Title { get; }

        /// <summary>Gets the subtitle of the video.</summary>
        public string Subtitle { get; }

        /// <summary>Gets the publisher of the video.</summary>
        public string Publisher { get; }

        /// <summary>Gets the rating associated with a video file.</summary>
        public int Rating { get; }

        /// <summary>Gets the latitude coordinate where the video was shot.</summary>
        public double? Latitude { get; }

        /// <summary>Gets a VideoOrientation value that indicates how the video should be rotated to display it correctly.</summary>
        public VideoOrientation Orientation { get; }

        /// <summary>Gets the duration of the video.</summary>
        public TimeSpan Duration { get; }

        /// <summary>Gets the sum audio and video bitrate of the video.</summary>
        public int Bitrate { get; }

        /// <summary>Gets the producers of the video.</summary>
        public IList<string> Producers { get; }

        /// <summary>Gets the directors of the video.</summary>
        public IList<string> Directors { get; }

        /// <summary>Gets the height of the video.</summary>
        public int Height { get; }

        /// <summary>Gets the width of the video.</summary>
        public int Width { get; }

        /// <summary>Gets the longitude coordinate where the video was shot.</summary>
        public double? Longitude { get; }

        /// <summary>Gets the script writers for the video.</summary>
        public IList<string> Writers { get; }

        /// <summary>Gets the collection of keywords associated with the video.</summary>
        public IList<string> Keywords { get; }

        public static implicit operator VideoProperties(Windows.Storage.FileProperties.VideoProperties props)
        {
            return new VideoProperties(props);
        }
    }
}
#endif