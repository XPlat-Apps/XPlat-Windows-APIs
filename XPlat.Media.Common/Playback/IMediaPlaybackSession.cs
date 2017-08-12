namespace XPlat.Media.Playback
{
    using System;
    using System.Runtime.InteropServices;

    using XPlat.Foundation;

    public interface IMediaPlaybackSession
    {
        double BufferingProgress { get; }

        bool CanPause { get; }

        bool CanSeek { get; }

        double DownloadProgress { get; }

        bool IsProtected { get; }

        IMediaPlayer MediaPlayer { get; }

        TimeSpan NaturalDuration { get; }

        uint NaturalVideoHeight { get; }

        uint NaturalVideoWidth { get; }

        double PlaybackRate { get; set; }

        MediaPlaybackState PlaybackState { get; }

        TimeSpan Position { get; [param: In] set; }

        event TypedEventHandler<IMediaPlaybackSession, object> BufferingEnded;

        event TypedEventHandler<IMediaPlaybackSession, object> BufferingProgressChanged;

        event TypedEventHandler<IMediaPlaybackSession, object> BufferingStarted;

        event TypedEventHandler<IMediaPlaybackSession, object> DownloadProgressChanged;

        event TypedEventHandler<IMediaPlaybackSession, object> NaturalDurationChanged;

        event TypedEventHandler<IMediaPlaybackSession, object> NaturalVideoSizeChanged;

        event TypedEventHandler<IMediaPlaybackSession, object> PlaybackRateChanged;

        event TypedEventHandler<IMediaPlaybackSession, object> PlaybackStateChanged;

        event TypedEventHandler<IMediaPlaybackSession, object> PositionChanged;

        event TypedEventHandler<IMediaPlaybackSession, object> SeekCompleted;
    }
}