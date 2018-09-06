namespace XPlat.Storage.Extensions
{
    using System.Threading.Tasks;

    public static class StorageFileExtensions
    {
#if __ANDROID__
        public static IStorageFile GetFileFromPath(string path)
        {
            Task<IStorageFile> storageFileTask = StorageFile.GetFileFromPathAsync(path);
            storageFileTask.Wait();
            return storageFileTask.Result;
        }
#endif
    }
}