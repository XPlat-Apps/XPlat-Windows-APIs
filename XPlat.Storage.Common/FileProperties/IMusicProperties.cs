namespace XPlat.Storage.FileProperties
{
    using System;
    using System.Collections.Generic;

    public interface IMusicProperties
    {
        int Rating { get; }

        string AlbumArtist { get; }

        string Artist { get; }

        string Album { get; }

        string Publisher { get; }

        int Year { get; }

        int TrackNumber { get; }

        string Title { get; }

        string Subtitle { get; }

        IList<string> Producers { get; }

        IList<string> Composers { get; }

        IList<string> Conductors { get; }

        TimeSpan Duration { get; }

        IList<string> Writers { get; }

        IList<string> Genre { get; }

        int Bitrate { get; }
    }
}