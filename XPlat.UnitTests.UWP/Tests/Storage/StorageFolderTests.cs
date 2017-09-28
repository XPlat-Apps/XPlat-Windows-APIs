namespace XPlat.UnitTests.UWP.Tests.Storage
{
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using XPlat.Storage;
    using XPlat.Storage.FileProperties;
    using XPlat.UnitTests.UWP.Helpers;

    [TestClass]
    public class StorageFolderTests
    {
        [TestMethod]
        public void StorageFolder_ReturnsBasicProperties()
        {
            const string Text = "Hello, World!";

            IStorageFolder folder = StorageHelper.CreateStorageFolder(ApplicationData.Current.LocalFolder, "FolderTest", CreationCollisionOption.GenerateUniqueName);

            IBasicProperties props = StorageHelper.GetBasicProperties(folder);

            Assert.AreEqual(0, props.Size);

            IStorageFile file1 = StorageHelper.CreateStorageFile(folder, "TestFile1.txt");

            StorageHelper.WriteTextToFile(file1, Text);

            IBasicProperties updatedProps = StorageHelper.GetBasicProperties(folder);

            Assert.AreEqual(Text.Length, (int)updatedProps.Size);
        }

        [TestMethod]
        public void StorageFolder_GetItemsAsync_ReturnsSubsetOfItems()
        {
            const int ItemsToRetrieve = 20;

            for (int i = 0; i < 5; i++)
            {
                StorageHelper.CreateStorageFolder(
                    ApplicationData.Current.LocalFolder,
                    $"Folder{i}",
                    CreationCollisionOption.ReplaceExisting);
            }

            for (int i = 0; i < 5; i++)
            {
                StorageHelper.CreateStorageFile(
                    ApplicationData.Current.LocalFolder,
                    $"File{i}.txt",
                    CreationCollisionOption.ReplaceExisting);
            }

            IReadOnlyList<IStorageItem> items = StorageHelper.GetFolderItems(ApplicationData.Current.LocalFolder, 0, ItemsToRetrieve);

            Assert.AreEqual(ItemsToRetrieve, items.Count);
        }

        [TestMethod]
        public void StorageFolder_TryGetItemAsync_ReturnsExistingItem()
        {
            const string FileName = "Test.txt";

            StorageHelper.CreateStorageFile(
                ApplicationData.Current.LocalFolder,
                FileName,
                CreationCollisionOption.ReplaceExisting);

            IStorageItem item = StorageHelper.TryGetItem(ApplicationData.Current.LocalFolder, FileName);

            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void StorageFolder_TryGetItemAsync_ReturnsNullForNonExistingItem()
        {
            const string FileName = "DefinitelyNotAFile.txt";

            IStorageItem item = StorageHelper.TryGetItem(ApplicationData.Current.LocalFolder, FileName);

            Assert.IsNull(item);
        }
    }
}