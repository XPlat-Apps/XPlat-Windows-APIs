namespace XPlat.Storage
{
    using Android.OS;

    public static class KnownFolders
    {
        public static IStorageFolder CameraRoll
        {
            get
            {
                var cameraRoll = StorageFolder.GetFolderFromPathAsync(
                    Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDcim).AbsolutePath);
                cameraRoll.Wait();
                return cameraRoll.Result;
            }
        }
    }
}