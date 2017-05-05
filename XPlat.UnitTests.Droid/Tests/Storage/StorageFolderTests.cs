namespace XPlat.UnitTests.Droid.Tests.Storage
{
    using NUnit.Framework;

    using XPlat.Storage;
    using XPlat.UnitTests.Droid.Helpers;

    [TestFixture]
    public class StorageFolderTests
    {
        [Test]
        public void StorageFolder_ReturnsBasicProperties()
        {
            const string Text = "Hello, World!";

            var folder = StorageHelper.CreateStorageFolder(ApplicationData.Current.LocalFolder, "FolderTest", CreationCollisionOption.GenerateUniqueName);

            var props = StorageHelper.GetBasicProperties(folder);

            Assert.AreEqual(0, props.Size);

            var file1 = StorageHelper.CreateStorageFile(folder, "TestFile1.txt");

            StorageHelper.WriteTextToFile(file1, Text);

            var updatedProps = StorageHelper.GetBasicProperties(folder);

            Assert.AreEqual(Text.Length, updatedProps.Size);
        }

        [Test]
        public void StorageFolder_GetItemsAsync_ReturnsSubsetOfItems()
        {
            const int ItemsToRetrieve = 6;

            for (var i = 0; i < 5; i++)
            {
                StorageHelper.CreateStorageFolder(
                    ApplicationData.Current.LocalFolder,
                    $"Folder{i}",
                    CreationCollisionOption.ReplaceExisting);
            }

            for (var i = 0; i < 5; i++)
            {
                StorageHelper.CreateStorageFile(
                    ApplicationData.Current.LocalFolder,
                    $"File{i}.txt",
                    CreationCollisionOption.ReplaceExisting);
            }

            var items = StorageHelper.GetFolderItems(ApplicationData.Current.LocalFolder, 7, ItemsToRetrieve);

            Assert.AreEqual(ItemsToRetrieve, items.Count);
        }
    }
}