namespace XPlat.Storage.FileProperties
{
    using System;
    using System.Collections.Generic;

    public interface IVideoProperties
    {
        int Year { get; }

        string Title { get; }

        string Subtitle { get; }

        string Publisher { get; }

        int Rating { get; }

        double? Latitude { get; }

        VideoOrientation Orientation { get; }

        TimeSpan Duration { get; }

        int Bitrate { get; }

        IList<string> Producers { get; }

        IList<string> Directors { get; }

        int Height { get; }

        int Width { get; }

        double? Longitude { get; }

        IList<string> Writers { get; }

        IList<string> Keywords { get; }
    }
}