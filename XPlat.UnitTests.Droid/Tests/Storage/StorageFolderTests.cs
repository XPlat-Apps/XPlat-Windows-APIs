namespace XPlat.UnitTests.Droid.Tests.Storage
{
    using NUnit.Framework;

    using XPlat.Storage;
    using XPlat.UnitTests.Droid.Helpers;

    [TestFixture]
    public class StorageFolderTests
    {
        [Test]
        public void StorageFolderReturnsBasicProperties()
        {
            const string Text = "Hello, World!";

            var folder = StorageHelper.CreateStorageFolder(ApplicationData.Current.LocalFolder, "FolderTest");

            var props = StorageHelper.GetBasicProperties(folder);

            Assert.AreEqual(0, props.Size);

            var file1 = StorageHelper.CreateStorageFile(folder, "TestFile1.txt");

            StorageHelper.WriteTextToFile(file1, Text);

            var updatedProps = StorageHelper.GetBasicProperties(folder);

            Assert.AreEqual(Text.Length, updatedProps.Size);
        }
    }
}