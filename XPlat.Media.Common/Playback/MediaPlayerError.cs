namespace XPlat.Media.Playback
{
    /// <summary>
    /// Indicates possible media player errors.
    /// </summary>
    public enum MediaPlayerError
    {
        Unknown,

        Aborted,

        NetworkError,

        DecodingError,

        SourceNotSupported,
    }
}