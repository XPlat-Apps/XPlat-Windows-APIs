namespace XPlat.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using XPlat.Storage.FileProperties;

    public static partial class Extensions
    {
        public static async Task<Dictionary<string, object>> GetAllMediaPropertiesAsync(
            this IStorageItemContentProperties storageProps)
        {
            var props = new Dictionary<string, object>();

            try
            {
                var imageProps = await storageProps.GetImagePropertiesAsync();

                if (imageProps != null)
                {
                    if (!string.IsNullOrWhiteSpace(imageProps.Title) && !props.ContainsKey("System.Title"))
                    {
                        props.Add("System.Title", imageProps.Title);
                    }

                    if (!props.ContainsKey("System.Rating"))
                    {
                        props.Add("System.Rating", imageProps.Rating);
                    }

                    if (!string.IsNullOrWhiteSpace(imageProps.DateTaken) && !props.ContainsKey("System.Photo.DateTaken"))
                    {
                        props.Add("System.Photo.DateTaken", imageProps.DateTaken);
                    }

                    if (!string.IsNullOrWhiteSpace(imageProps.CameraModel)
                        && !props.ContainsKey("System.Photo.CameraManufacturer"))
                    {
                        props.Add("System.Photo.CameraModel", imageProps.CameraModel);
                    }

                    if (!string.IsNullOrWhiteSpace(imageProps.CameraManufacturer)
                        && !props.ContainsKey("System.Photo.CameraManufacturer"))
                    {
                        props.Add("System.Photo.CameraManufacturer", imageProps.CameraManufacturer);
                    }

                    if (imageProps.Latitude != null && !props.ContainsKey("System.GPS.Latitude"))
                    {
                        props.Add("System.GPS.Latitude", imageProps.Latitude);
                    }

                    if (imageProps.Longitude != null && !props.ContainsKey("System.GPS.Longitude"))
                    {
                        props.Add("System.GPS.Longitude", imageProps.Latitude);
                    }

                    if (imageProps.Orientation != PhotoOrientation.Unspecified
                        && !props.ContainsKey("System.Photo.Orientation"))
                    {
                        props.Add("System.Photo.Orientation", imageProps.Orientation);
                    }

                    if (imageProps.PeopleNames != null && imageProps.PeopleNames.Any()
                        && !props.ContainsKey("System.Photo.PeopleNames"))
                    {
                        props.Add("System.Photo.PeopleNames", imageProps.PeopleNames);
                    }

                    if (imageProps.Height != 0 && !props.ContainsKey("System.Image.VerticalSize"))
                    {
                        props.Add("System.Image.VerticalSize", imageProps.Height);
                    }

                    if (imageProps.Keywords != null && imageProps.Keywords.Any()
                        && !props.ContainsKey("System.Keywords"))
                    {
                        props.Add("System.Keywords", imageProps.Keywords);
                    }

                    if (imageProps.Width != 0 && !props.ContainsKey("System.Image.HorizontalSize"))
                    {
                        props.Add("System.Image.HorizontalSize", imageProps.Width);
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                var videoProps = await storageProps.GetVideoPropertiesAsync();
                if (videoProps != null)
                {
                    if (videoProps.Year != 0 && !props.ContainsKey("System.Media.Year"))
                    {
                        props.Add("System.Media.Year", videoProps.Year);
                    }

                    if (!string.IsNullOrWhiteSpace(videoProps.Title) && !props.ContainsKey("System.Title"))
                    {
                        props.Add("System.Title", videoProps.Title);
                    }

                    if (!string.IsNullOrWhiteSpace(videoProps.Subtitle) && !props.ContainsKey("System.Media.SubTitle"))
                    {
                        props.Add("System.Media.SubTitle", videoProps.Subtitle);
                    }

                    if (!string.IsNullOrWhiteSpace(videoProps.Publisher)
                        && !props.ContainsKey("System.Media.Publisher"))
                    {
                        props.Add("System.Media.Publisher", videoProps.Publisher);
                    }

                    if (!props.ContainsKey("System.Rating"))
                    {
                        props.Add("System.Rating", videoProps.Rating);
                    }

                    if (videoProps.Latitude != null && !props.ContainsKey("System.GPS.Latitude"))
                    {
                        props.Add("System.GPS.Latitude", videoProps.Latitude);
                    }

                    if (!props.ContainsKey("System.Video.Orientation"))
                    {
                        props.Add("System.Video.Orientation", videoProps.Orientation);
                    }

                    if (!props.ContainsKey("System.Media.Duration"))
                    {
                        props.Add("System.Media.Duration", videoProps.Duration);
                    }

                    if (!props.ContainsKey("System.Video.TotalBitrate"))
                    {
                        props.Add("System.Video.TotalBitrate", videoProps.Bitrate);
                    }

                    if (videoProps.Producers != null && videoProps.Producers.Any()
                        && !props.ContainsKey("System.Media.Producer"))
                    {
                        props.Add("System.Media.Producer", videoProps.Producers);
                    }

                    if (videoProps.Directors != null && videoProps.Directors.Any()
                        && !props.ContainsKey("System.Video.Director"))
                    {
                        props.Add("System.Video.Director", videoProps.Directors);
                    }

                    if (videoProps.Height != 0 && !props.ContainsKey("System.Video.FrameHeight"))
                    {
                        props.Add("System.Video.FrameHeight", videoProps.Height);
                    }

                    if (videoProps.Keywords != null && videoProps.Keywords.Any()
                        && !props.ContainsKey("System.Keywords"))
                    {
                        props.Add("System.Keywords", videoProps.Keywords);
                    }

                    if (videoProps.Width != 0 && !props.ContainsKey("System.Video.FrameWidth"))
                    {
                        props.Add("System.Video.FrameWidth", videoProps.Width);
                    }

                    if (videoProps.Longitude != null && !props.ContainsKey("System.GPS.Longitude"))
                    {
                        props.Add("System.GPS.Longitude", videoProps.Latitude);
                    }

                    if (videoProps.Writers != null && videoProps.Writers.Any()
                        && !props.ContainsKey("System.Media.Writer"))
                    {
                        props.Add("System.Media.Writer", videoProps.Writers);
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                var musicProps = await storageProps.GetMusicPropertiesAsync();
                if (musicProps != null)
                {
                    if (!props.ContainsKey("System.Rating"))
                    {
                        props.Add("System.Rating", musicProps.Rating);
                    }

                    if (!string.IsNullOrWhiteSpace(musicProps.AlbumArtist)
                        && !props.ContainsKey("System.Music.AlbumArtist"))
                    {
                        props.Add("System.Music.AlbumArtist", musicProps.AlbumArtist);
                    }

                    if (!string.IsNullOrWhiteSpace(musicProps.Artist)
                        && !props.ContainsKey("System.Music.Artist"))
                    {
                        props.Add("System.Music.Artist", musicProps.Artist);
                    }

                    if (!string.IsNullOrWhiteSpace(musicProps.Album)
                        && !props.ContainsKey("System.Music.AlbumTitle"))
                    {
                        props.Add("System.Music.AlbumTitle", musicProps.Album);
                    }

                    if (!string.IsNullOrWhiteSpace(musicProps.Publisher)
                        && !props.ContainsKey("System.Media.Publisher"))
                    {
                        props.Add("System.Media.Publisher", musicProps.Publisher);
                    }

                    if (musicProps.Year != 0 && !props.ContainsKey("System.Media.Year"))
                    {
                        props.Add("System.Media.Year", musicProps.Year);
                    }

                    if (musicProps.TrackNumber != 0 && !props.ContainsKey("System.Music.TrackNumber"))
                    {
                        props.Add("System.Music.TrackNumber", musicProps.TrackNumber);
                    }

                    if (!string.IsNullOrWhiteSpace(musicProps.Title) && !props.ContainsKey("System.Title"))
                    {
                        props.Add("System.Title", musicProps.Title);
                    }

                    if (!string.IsNullOrWhiteSpace(musicProps.Subtitle) && !props.ContainsKey("System.Media.SubTitle"))
                    {
                        props.Add("System.Media.SubTitle", musicProps.Subtitle);
                    }

                    if (musicProps.Producers != null && musicProps.Producers.Any()
                        && !props.ContainsKey("System.Media.Producer"))
                    {
                        props.Add("System.Media.Producer", musicProps.Producers);
                    }

                    if (musicProps.Composers != null && musicProps.Composers.Any()
                        && !props.ContainsKey("System.Music.Composer"))
                    {
                        props.Add("System.Music.Composer", musicProps.Composers);
                    }

                    if (musicProps.Conductors != null && musicProps.Conductors.Any()
                        && !props.ContainsKey("System.Music.Conductor"))
                    {
                        props.Add("System.Music.Conductor", musicProps.Conductors);
                    }

                    if (!props.ContainsKey("System.Media.Duration"))
                    {
                        props.Add("System.Media.Duration", musicProps.Duration);
                    }

                    if (musicProps.Writers != null && musicProps.Writers.Any()
                        && !props.ContainsKey("System.Media.Writer"))
                    {
                        props.Add("System.Media.Writer", musicProps.Writers);
                    }

                    if (musicProps.Genre != null && musicProps.Genre.Any()
                        && !props.ContainsKey("System.Music.Genre"))
                    {
                        props.Add("System.Music.Genre", musicProps.Genre);
                    }

                    if (!props.ContainsKey("System.Audio.EncodingBitrate"))
                    {
                        props.Add("System.Audio.EncodingBitrate", musicProps.Bitrate);
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return props;
        }
    }
}