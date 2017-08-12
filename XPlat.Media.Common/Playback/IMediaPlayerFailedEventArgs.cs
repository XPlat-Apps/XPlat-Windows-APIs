namespace XPlat.Media.Playback
{
    using System;

    public interface IMediaPlayerFailedEventArgs
    {
        MediaPlayerError Error { get; }

        string ErrorMessage { get; }

        Exception ExtendedErrorCode { get; }
    }
}