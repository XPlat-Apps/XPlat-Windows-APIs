namespace XPlat.UnitTests.UWP.Helpers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using XPlat.Storage;
    using XPlat.Storage.FileProperties;

    public static class StorageHelper
    {
        public static IStorageFile CreateStorageFile(IStorageFolder folder, string name)
        {
            Task<IStorageFile> storageFileTask = folder.CreateFileAsync(name);
            storageFileTask.Wait();
            return storageFileTask.Result;
        }

        public static IStorageFolder CreateStorageFolder(IStorageFolder folder, string name)
        {
            Task<IStorageFolder> storageFileTask = folder.CreateFolderAsync(name);
            storageFileTask.Wait();
            return storageFileTask.Result;
        }

        public static IStorageFile CreateStorageFile(
            IStorageFolder folder,
            string name,
            CreationCollisionOption collisionOption)
        {
            Task<IStorageFile> storageFileTask = folder.CreateFileAsync(name, collisionOption);
            storageFileTask.Wait();
            return storageFileTask.Result;
        }

        public static IStorageFolder CreateStorageFolder(
            IStorageFolder folder,
            string name,
            CreationCollisionOption collisionOption)
        {
            Task<IStorageFolder> storageFileTask = folder.CreateFolderAsync(name, collisionOption);
            storageFileTask.Wait();
            return storageFileTask.Result;
        }

        public static IReadOnlyList<IStorageItem> GetFolderItems(IStorageFolder folder, int idx, int count)
        {
            Task<IReadOnlyList<IStorageItem>> itemsTask = folder.GetItemsAsync(idx, count);
            itemsTask.Wait();
            return itemsTask.Result;
        }

        public static void WriteTextToFile(IStorageFile file, string text)
        {
            Task writeTask = file.WriteTextAsync(text);
            writeTask.Wait();
        }

        public static IBasicProperties GetBasicProperties(IStorageItem item)
        {
            Task<IBasicProperties> propsTask = item.GetBasicPropertiesAsync();
            propsTask.Wait();
            return propsTask.Result;
        }

        public static IDictionary<string, object> RetrieveProperties(IStorageItemProperties item, IEnumerable<string> propertiesToRetrieve)
        {
            Task<IDictionary<string, object>> propsTask = item.Properties.RetrievePropertiesAsync(propertiesToRetrieve);
            propsTask.Wait();
            return propsTask.Result;
        }

        public static void DeleteStorageItem(IStorageItem item)
        {
            Task deleteTask = item.DeleteAsync();
            deleteTask.Wait();
        }

        public static IStorageItem TryGetItem(IStorageFolder folder, string name)
        {
            Task<IStorageItem> getItemTask = folder.TryGetItemAsync(name);
            getItemTask.Wait();
            return getItemTask.Result;
        }
    }
}