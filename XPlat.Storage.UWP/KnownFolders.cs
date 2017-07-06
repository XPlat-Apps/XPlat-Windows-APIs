namespace XPlat.Storage
{
    public static class KnownFolders
    {
        public static IStorageFolder CameraRoll => new StorageFolder(Windows.Storage.KnownFolders.CameraRoll);
    }
}