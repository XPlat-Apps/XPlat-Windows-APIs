namespace XPlat.Media.Playback
{
    using XPlat.Storage;

    public interface IMediaPlayerSource
    {
        void SetFileSource(IStorageFile file);
    }
}