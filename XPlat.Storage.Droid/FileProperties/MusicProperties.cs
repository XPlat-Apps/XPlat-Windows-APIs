namespace XPlat.Storage.FileProperties
{
    using System;
    using System.Collections.Generic;

    using Android.Media;

    using XPlat.Helpers;

    public class MusicProperties : IMusicProperties
    {
        private readonly MediaMetadataRetriever mediaMetadataRetriever;

        internal MusicProperties(string filePath)
        {
            this.mediaMetadataRetriever = new MediaMetadataRetriever();
            this.mediaMetadataRetriever.SetDataSource(filePath);
        }

        public int Rating { get; }

        public string AlbumArtist => this.mediaMetadataRetriever.ExtractMetadata(MetadataKey.Albumartist);

        public string Artist => this.mediaMetadataRetriever.ExtractMetadata(MetadataKey.Artist);

        public string Album => this.mediaMetadataRetriever.ExtractMetadata(MetadataKey.Album);

        public string Publisher { get; }

        public int Year => ParseHelper.SafeParseInt(this.mediaMetadataRetriever.ExtractMetadata(MetadataKey.Year));

        public int TrackNumber => ParseHelper.SafeParseInt(
            this.mediaMetadataRetriever.ExtractMetadata(MetadataKey.CdTrackNumber));

        public string Title => this.mediaMetadataRetriever.ExtractMetadata(MetadataKey.Title);

        public string Subtitle { get; }

        public IList<string> Producers { get; }

        public IList<string> Composers => new List<string>
                                              {
                                                  this.mediaMetadataRetriever.ExtractMetadata(
                                                      MetadataKey.Composer)
                                              };

        public IList<string> Conductors { get; }

        public TimeSpan Duration => TimeSpan.FromMilliseconds(
            ParseHelper.SafeParseDouble(this.mediaMetadataRetriever.ExtractMetadata(MetadataKey.Duration)));

        public IList<string> Writers => new List<string>
                                            {
                                                this.mediaMetadataRetriever.ExtractMetadata(
                                                    MetadataKey.Writer)
                                            };

        public IList<string> Genre => new List<string>
                                          {
                                              this.mediaMetadataRetriever.ExtractMetadata(
                                                  MetadataKey.Genre)
                                          };

        public int Bitrate => ParseHelper.SafeParseInt(
            this.mediaMetadataRetriever.ExtractMetadata(MetadataKey.Bitrate));
    }
}