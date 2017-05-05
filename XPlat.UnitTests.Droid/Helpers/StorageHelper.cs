namespace XPlat.UnitTests.Droid.Helpers
{
    using XPlat.Storage;
    using XPlat.Storage.FileProperties;

    public static class StorageHelper
    {
        public static IStorageFile CreateStorageFile(IStorageFolder folder, string name)
        {
            var storageFileTask = folder.CreateFileAsync(name, CreationCollisionOption.GenerateUniqueName);
            storageFileTask.Wait();
            return storageFileTask.Result;
        }

        public static IStorageFolder CreateStorageFolder(IStorageFolder folder, string name)
        {
            var storageFileTask = folder.CreateFolderAsync(name, CreationCollisionOption.GenerateUniqueName);
            storageFileTask.Wait();
            return storageFileTask.Result;
        }

        public static void WriteTextToFile(IStorageFile file, string text)
        {
            var writeTask = file.WriteTextAsync(text);
            writeTask.Wait();
        }

        public static IBasicProperties GetBasicProperties(IStorageItem item)
        {
            var propsTask = item.GetBasicPropertiesAsync();
            propsTask.Wait();
            return propsTask.Result;
        }
    }
}