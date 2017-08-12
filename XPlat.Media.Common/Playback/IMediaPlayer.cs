namespace XPlat.Media.Playback
{
    using System;

    using XPlat.Foundation;

    public interface IMediaPlayer : IMediaPlayerSource
    {
        void Play();

        void Pause();

        void SetUriSource(Uri value);

        bool AutoPlay { get; set; }

        bool IsLoopingEnabled { get; set; }

        bool IsMuted { get; set; }

        double Volume { get; set; }

        IMediaPlaybackSession PlaybackSession { get; }

        event TypedEventHandler<IMediaPlayer, object> BufferingEnded;

        event TypedEventHandler<IMediaPlayer, object> BufferingStarted;

        event TypedEventHandler<IMediaPlayer, object> CurrentStateChanged;

        event TypedEventHandler<IMediaPlayer, object> MediaEnded;

        event TypedEventHandler<IMediaPlayer, MediaPlayerFailedEventArgs> MediaFailed;

        event TypedEventHandler<IMediaPlayer, object> MediaOpened;

        event TypedEventHandler<IMediaPlayer, object> SeekCompleted;

        event TypedEventHandler<IMediaPlayer, object> VolumeChanged;
    }
}