namespace XPlat.Storage.FileProperties
{
    using System;
    using System.Collections.Generic;

    /// <summary>Provides access to the video-related properties of an item (like a file or folder).</summary>
    public interface IVideoProperties
    {
        /// <summary>Gets the year that the video was shot or released.</summary>
        int Year { get; }

        /// <summary>Gets the title of the video.</summary>
        string Title { get; }

        /// <summary>Gets the subtitle of the video.</summary>
        string Subtitle { get; }

        /// <summary>Gets the publisher of the video.</summary>
        string Publisher { get; }

        /// <summary>Gets the rating associated with a video file.</summary>
        int Rating { get; }

        /// <summary>Gets the latitude coordinate where the video was shot.</summary>
        double? Latitude { get; }

        /// <summary>Gets a VideoOrientation value that indicates how the video should be rotated to display it correctly.</summary>
        VideoOrientation Orientation { get; }

        /// <summary>Gets the duration of the video.</summary>
        TimeSpan Duration { get; }

        /// <summary>Gets the sum audio and video bitrate of the video.</summary>
        int Bitrate { get; }

        /// <summary>Gets the producers of the video.</summary>
        IList<string> Producers { get; }

        /// <summary>Gets the directors of the video.</summary>
        IList<string> Directors { get; }

        /// <summary>Gets the height of the video.</summary>
        int Height { get; }

        /// <summary>Gets the width of the video.</summary>
        int Width { get; }

        /// <summary>Gets the longitude coordinate where the video was shot.</summary>
        double? Longitude { get; }

        /// <summary>Gets the script writers for the video.</summary>
        IList<string> Writers { get; }

        /// <summary>Gets the collection of keywords associated with the video.</summary>
        IList<string> Keywords { get; }
    }
}