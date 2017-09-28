namespace XPlat.Storage.Helpers
{
    using System.Threading.Tasks;

    public static class DroidStorageHelper
    {
        public static IStorageFile GetFileFromPath(string path)
        {
            Task<IStorageFile> storageFileTask = StorageFile.GetFileFromPathAsync(path);
            storageFileTask.Wait();
            return storageFileTask.Result;
        }
    }
}