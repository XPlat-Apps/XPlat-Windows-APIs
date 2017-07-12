namespace XPlat.UnitTests.iOS.Tests.Storage
{
    using System.Linq;

    using NUnit.Framework;

    using XPlat.Storage;
    using XPlat.UnitTests.iOS.Helpers;

    [TestFixture]
    public class StorageFileTests
    {
        private static string SizeUpdatesFile = "StorageFile_GetBasicPropertiesAsync_SizeUpdates.txt";

        private static string RetrievePropertiesFile = "StorageFile_RetrievePropertiesAsync_ContainsSome.txt";

        [Test]
        public void StorageFile_GetBasicPropertiesAsync_SizeUpdates()
        {
            const string Text = "Hello, World!";

            var file = StorageHelper.CreateStorageFile(ApplicationData.Current.LocalFolder, SizeUpdatesFile, CreationCollisionOption.ReplaceExisting);
            var props = StorageHelper.GetBasicProperties(file);

            Assert.AreEqual(0, props.Size);

            StorageHelper.WriteTextToFile(file, Text);

            var updatedProps = StorageHelper.GetBasicProperties(file);

            Assert.AreEqual(Text.Length, updatedProps.Size);
        }

        [Test]
        public void StorageFile_RetrievePropertiesAsync_ContainsSome()
        {
            var file = StorageHelper.CreateStorageFile(ApplicationData.Current.LocalFolder, RetrievePropertiesFile, CreationCollisionOption.ReplaceExisting);
            var props = StorageHelper.RetrieveProperties(file, null);

            Assert.IsTrue(props.Any());
        }
    }
}