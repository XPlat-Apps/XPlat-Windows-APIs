namespace XPlat.UnitTests.Droid.Helpers
{
    using System.Collections.Generic;

    using XPlat.Storage;
    using XPlat.Storage.FileProperties;

    public static class StorageHelper
    {
        public static IStorageFile CreateStorageFile(IStorageFolder folder, string name)
        {
            var storageFileTask = folder.CreateFileAsync(name);
            storageFileTask.Wait();
            return storageFileTask.Result;
        }

        public static IStorageFolder CreateStorageFolder(IStorageFolder folder, string name)
        {
            var storageFileTask = folder.CreateFolderAsync(name);
            storageFileTask.Wait();
            return storageFileTask.Result;
        }

        public static IStorageFile CreateStorageFile(IStorageFolder folder, string name, CreationCollisionOption collisionOption)
        {
            var storageFileTask = folder.CreateFileAsync(name, collisionOption);
            storageFileTask.Wait();
            return storageFileTask.Result;
        }

        public static IStorageFolder CreateStorageFolder(IStorageFolder folder, string name, CreationCollisionOption collisionOption)
        {
            var storageFileTask = folder.CreateFolderAsync(name, collisionOption);
            storageFileTask.Wait();
            return storageFileTask.Result;
        }

        public static IReadOnlyList<IStorageItem> GetFolderItems(IStorageFolder folder, int idx, int count)
        {
            var itemsTask = folder.GetItemsAsync(idx, count);
            itemsTask.Wait();
            return itemsTask.Result;
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

        public static void DeleteStorageItem(IStorageItem item)
        {
            var deleteTask = item.DeleteAsync();
            deleteTask.Wait();
        }
    }
}