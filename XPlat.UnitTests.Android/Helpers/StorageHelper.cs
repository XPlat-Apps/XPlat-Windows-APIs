using System.Collections.Generic;
using System.Threading.Tasks;
using XPlat.Storage;
using XPlat.Storage.FileProperties;

namespace XPlat.UnitTests.Android.Helpers
{
    public static class StorageHelper
    {
        public static IStorageFile CreateStorageFile(IStorageFolder folder, string name)
        {
            Task<IStorageFile> storageFileTask = folder.CreateFileAsync(name);
            return storageFileTask.GetAwaiter().GetResult();
        }

        public static IStorageFolder CreateStorageFolder(IStorageFolder folder, string name)
        {
            Task<IStorageFolder> storageFileTask = folder.CreateFolderAsync(name);
            return storageFileTask.GetAwaiter().GetResult();
        }

        public static IStorageFile CreateStorageFile(
            IStorageFolder folder,
            string name,
            CreationCollisionOption collisionOption)
        {
            Task<IStorageFile> storageFileTask = folder.CreateFileAsync(name, collisionOption);
            return storageFileTask.GetAwaiter().GetResult();
        }

        public static IStorageFolder CreateStorageFolder(
            IStorageFolder folder,
            string name,
            CreationCollisionOption collisionOption)
        {
            Task<IStorageFolder> storageFileTask = folder.CreateFolderAsync(name, collisionOption);
            return storageFileTask.GetAwaiter().GetResult();
        }

        public static IReadOnlyList<IStorageItem> GetFolderItems(IStorageFolder folder, int idx, int count)
        {
            Task<IReadOnlyList<IStorageItem>> itemsTask = folder.GetItemsAsync(idx, count);
            return itemsTask.GetAwaiter().GetResult();
        }

        public static void WriteTextToFile(IStorageFile file, string text)
        {
            Task writeTask = file.WriteTextAsync(text);
            writeTask.GetAwaiter().GetResult();
        }

        public static IBasicProperties GetBasicProperties(IStorageItem item)
        {
            Task<IBasicProperties> propsTask = item.GetBasicPropertiesAsync();
            return propsTask.GetAwaiter().GetResult();
        }

        public static IDictionary<string, object> RetrieveProperties(IStorageItemProperties item, IEnumerable<string> propertiesToRetrieve)
        {
            Task<IDictionary<string, object>> propsTask = item.Properties.RetrievePropertiesAsync(propertiesToRetrieve);
            return propsTask.GetAwaiter().GetResult();
        }

        public static void DeleteStorageItem(IStorageItem item)
        {
            Task deleteTask = item.DeleteAsync();
            deleteTask.GetAwaiter().GetResult();
        }

        public static IStorageItem TryGetItem(IStorageFolder folder, string name)
        {
            Task<IStorageItem> getItemTask = folder.TryGetItemAsync(name);
            return getItemTask.GetAwaiter().GetResult();
        }
    }
}