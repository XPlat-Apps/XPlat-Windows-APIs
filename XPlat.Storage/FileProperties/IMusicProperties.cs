namespace XPlat.Storage.FileProperties
{
    using System;
    using System.Collections.Generic;

    /// <summary>Provides access to the music-related properties of an item (like a file or folder).</summary>
    public interface IMusicProperties
    {
        /// <summary>Gets the rating associated with a music file.</summary>
        int Rating { get; }

        /// <summary>Gets the name of the album artist of the song.</summary>
        string AlbumArtist { get; }

        /// <summary>Gets the artists that contributed to the song.</summary>
        string Artist { get; }

        /// <summary>Gets the name of the album that contains the song.</summary>
        string Album { get; }

        /// <summary>Gets the publisher of the song.</summary>
        string Publisher { get; }

        /// <summary>Gets the year that the song was released.</summary>
        int Year { get; }

        /// <summary>Gets the track number of the song on the song's album.</summary>
        int TrackNumber { get; }

        /// <summary>Gets the title of the song</summary>
        string Title { get; }

        /// <summary>Gets the subtitle of the song.</summary>
        string Subtitle { get; }

        /// <summary>Gets the producers of the song.</summary>
        IList<string> Producers { get; }

        /// <summary>Gets the composers of the song.</summary>
        IList<string> Composers { get; }

        /// <summary>Gets the conductors of the song.</summary>
        IList<string> Conductors { get; }

        /// <summary>Gets the duration of the song in milliseconds.</summary>
        TimeSpan Duration { get; }

        /// <summary>Gets the songwriters.</summary>
        IList<string> Writers { get; }

        /// <summary>Gets the names of music genres that the song belongs to.</summary>
        IList<string> Genre { get; }

        /// <summary>Gets the bit rate of the song file.</summary>
        int Bitrate { get; }
    }
}