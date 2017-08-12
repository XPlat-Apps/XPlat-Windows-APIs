namespace XPlat.Media.Playback
{
    using System;

    public class MediaPlayerFailedEventArgs : IMediaPlayerFailedEventArgs
    {
        public MediaPlayerFailedEventArgs(MediaPlayerError error)
            : this(error, null, null)
        {
        }

        public MediaPlayerFailedEventArgs(MediaPlayerError error, string errorMessage)
            : this(error, errorMessage, null)
        {
        }

        public MediaPlayerFailedEventArgs(MediaPlayerError error, string errorMessage, Exception innerException)
        {
            this.Error = error;
            this.ErrorMessage = errorMessage;
            this.ExtendedErrorCode = innerException;
        }

        public MediaPlayerError Error { get; }

        public string ErrorMessage { get; }

        public Exception ExtendedErrorCode { get; }
    }
}