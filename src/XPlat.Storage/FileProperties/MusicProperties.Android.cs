#if __ANDROID__
namespace XPlat.Storage.FileProperties
{
    using System;
    using System.Collections.Generic;

    using Android.Media;

    /// <summary>Provides access to the music-related properties of an item (like a file or folder).</summary>
    public class MusicProperties : IMusicProperties
    {
        internal MusicProperties(string filePath)
        {
            this.MediaMetadataRetriever = new MediaMetadataRetriever();
            this.MediaMetadataRetriever.SetDataSource(filePath);
        }

        public MediaMetadataRetriever MediaMetadataRetriever { get; }

        /// <summary>Gets the rating associated with a music file.</summary>
        public int Rating { get; }

        /// <summary>Gets the name of the album artist of the song.</summary>
        public string AlbumArtist => this.MediaMetadataRetriever.ExtractMetadata(MetadataKey.Albumartist);

        /// <summary>Gets the artists that contributed to the song.</summary>
        public string Artist => this.MediaMetadataRetriever.ExtractMetadata(MetadataKey.Artist);

        /// <summary>Gets the name of the album that contains the song.</summary>
        public string Album => this.MediaMetadataRetriever.ExtractMetadata(MetadataKey.Album);

        /// <summary>Gets the publisher of the song.</summary>
        public string Publisher { get; }

        /// <summary>Gets the year that the song was released.</summary>
        public int Year =>
            int.TryParse(this.MediaMetadataRetriever.ExtractMetadata(MetadataKey.Year), out int parsedValue)
                ? parsedValue
                : 0;

        /// <summary>Gets the track number of the song on the song's album.</summary>
        public int TrackNumber =>
            int.TryParse(this.MediaMetadataRetriever.ExtractMetadata(MetadataKey.CdTrackNumber), out int parsedValue)
                ? parsedValue
                : 0;

        /// <summary>Gets the title of the song</summary>
        public string Title => this.MediaMetadataRetriever.ExtractMetadata(MetadataKey.Title);

        /// <summary>Gets the subtitle of the song.</summary>
        public string Subtitle { get; }

        /// <summary>Gets the producers of the song.</summary>
        public IList<string> Producers { get; }

        /// <summary>Gets the composers of the song.</summary>
        public IList<string> Composers => new List<string>
        {
            this.MediaMetadataRetriever.ExtractMetadata(
                MetadataKey.Composer)
        };

        /// <summary>Gets the conductors of the song.</summary>
        public IList<string> Conductors { get; }

        /// <summary>Gets the duration of the song in milliseconds.</summary>
        public TimeSpan Duration => TimeSpan.FromMilliseconds(
            double.TryParse(this.MediaMetadataRetriever.ExtractMetadata(MetadataKey.Duration), out double parsedValue)
                ? parsedValue
                : 0);

        /// <summary>Gets the songwriters.</summary>
        public IList<string> Writers => new List<string>
        {
            this.MediaMetadataRetriever.ExtractMetadata(
                MetadataKey.Writer)
        };

        /// <summary>Gets the names of music genres that the song belongs to.</summary>
        public IList<string> Genre => new List<string>
        {
            this.MediaMetadataRetriever.ExtractMetadata(
                MetadataKey.Genre)
        };

        /// <summary>Gets the bit rate of the song file.</summary>
        public int Bitrate =>
            int.TryParse(this.MediaMetadataRetriever.ExtractMetadata(MetadataKey.Bitrate), out int parsedValue)
                ? parsedValue
                : 0;
    }
}
#endif