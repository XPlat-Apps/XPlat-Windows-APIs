namespace XPlat.Media.Playback
{
    using System;

    using XPlat.Foundation;
    using XPlat.Storage;

    public class MediaPlayer : IMediaPlayer
    {
        public void SetFileSource(IStorageFile file)
        {
            throw new NotImplementedException();
        }

        public void Play()
        {
            throw new NotImplementedException();
        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void SetUriSource(Uri value)
        {
            throw new NotImplementedException();
        }

        public bool AutoPlay { get; set; }

        public bool IsLoopingEnabled { get; set; }

        public bool IsMuted { get; set; }

        public double Volume { get; set; }

        public IMediaPlaybackSession PlaybackSession { get; }

        public event TypedEventHandler<IMediaPlayer, object> BufferingEnded;

        public event TypedEventHandler<IMediaPlayer, object> BufferingStarted;

        public event TypedEventHandler<IMediaPlayer, object> CurrentStateChanged;

        public event TypedEventHandler<IMediaPlayer, object> MediaEnded;

        public event TypedEventHandler<IMediaPlayer, MediaPlayerFailedEventArgs> MediaFailed;

        public event TypedEventHandler<IMediaPlayer, object> MediaOpened;

        public event TypedEventHandler<IMediaPlayer, object> SeekCompleted;

        public event TypedEventHandler<IMediaPlayer, object> VolumeChanged;
    }
}