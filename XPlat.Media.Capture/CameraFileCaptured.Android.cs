#if __ANDROID__
namespace XPlat.Media.Capture
{
    using XPlat.Storage;

    internal class CameraFileCaptured
    {
        public CameraFileCaptured(int requestId, IStorageFile file, bool cancel)
        {
            if (file == null)
            {
                cancel = true;
            }

            this.RequestId = requestId;
            this.File = file;
            this.Cancel = cancel;
        }

        public bool Cancel { get; }

        public IStorageFile File { get; }

        public int RequestId { get; }
    }
}
#endif