namespace XPlat.Storage.Helpers
{
    public static class DroidStorageHelper
    {
        public static IStorageFile GetFileFromPath(string path)
        {
            var storageFileTask = StorageFile.GetFileFromPathAsync(path);
            storageFileTask.Wait();
            return storageFileTask.Result;
        }
    }
}