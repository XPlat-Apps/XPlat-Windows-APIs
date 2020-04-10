#if WINDOWS_UWP
namespace XPlat.Storage.FileProperties
{
    using System;
    using System.Collections.Generic;

    /// <summary>Provides access to the music-related properties of an item (like a file or folder).</summary>
    public class MusicProperties : IMusicProperties
    {
        public MusicProperties(Windows.Storage.FileProperties.MusicProperties props)
        {
            this.Rating = (int)props.Rating;
            this.AlbumArtist = props.AlbumArtist;
            this.Artist = props.Artist;
            this.Album = props.Album;
            this.Publisher = props.Publisher;
            this.Year = (int)props.Year;
            this.TrackNumber = (int)props.TrackNumber;
            this.Title = props.Title;
            this.Subtitle = props.Subtitle;
            this.Producers = props.Producers;
            this.Composers = props.Composers;
            this.Conductors = props.Conductors;
            this.Duration = props.Duration;
            this.Writers = props.Writers;
            this.Genre = props.Genre;
            this.Bitrate = (int)props.Bitrate;
        }

        /// <summary>Gets the rating associated with a music file.</summary>
        public int Rating { get; }

        /// <summary>Gets the name of the album artist of the song.</summary>
        public string AlbumArtist { get; }

        /// <summary>Gets the artists that contributed to the song.</summary>
        public string Artist { get; }

        /// <summary>Gets the name of the album that contains the song.</summary>
        public string Album { get; }

        /// <summary>Gets the publisher of the song.</summary>
        public string Publisher { get; }

        /// <summary>Gets the year that the song was released.</summary>
        public int Year { get; }

        /// <summary>Gets the track number of the song on the song's album.</summary>
        public int TrackNumber { get; }

        /// <summary>Gets the title of the song</summary>
        public string Title { get; }

        /// <summary>Gets the subtitle of the song.</summary>
        public string Subtitle { get; }

        /// <summary>Gets the producers of the song.</summary>
        public IList<string> Producers { get; }

        /// <summary>Gets the composers of the song.</summary>
        public IList<string> Composers { get; }

        /// <summary>Gets the conductors of the song.</summary>
        public IList<string> Conductors { get; }

        /// <summary>Gets the duration of the song in milliseconds.</summary>
        public TimeSpan Duration { get; }

        /// <summary>Gets the songwriters.</summary>
        public IList<string> Writers { get; }

        /// <summary>Gets the names of music genres that the song belongs to.</summary>
        public IList<string> Genre { get; }

        /// <summary>Gets the bit rate of the song file.</summary>
        public int Bitrate { get; }

        public static implicit operator MusicProperties(Windows.Storage.FileProperties.MusicProperties props)
        {
            return new MusicProperties(props);
        }
    }
}
#endif