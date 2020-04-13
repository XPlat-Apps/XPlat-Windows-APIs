#if __ANDROID__
namespace XPlat.Media.Capture
{
    using XPlat.Storage;

    internal class CameraFileCaptured
    {
        public CameraFileCaptured(int requestId, IStorageFile file, bool cancel, bool permissionDenied)
        {
            if (file == null)
            {
                cancel = true;
            }

            this.RequestId = requestId;
            this.File = file;
            this.Cancel = cancel;
            this.PermissionDenied = permissionDenied;
        }

        public bool Cancel { get; }

        public bool PermissionDenied { get; }

        public IStorageFile File { get; }

        public int RequestId { get; }
    }
}
#endif