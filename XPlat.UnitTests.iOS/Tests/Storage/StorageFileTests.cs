namespace XPlat.UnitTests.iOS.Tests.Storage
{
    using System.Collections.Generic;
    using System.Linq;

    using NUnit.Framework;

    using XPlat.Storage;
    using XPlat.Storage.FileProperties;
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

            IStorageFile file = StorageHelper.CreateStorageFile(ApplicationData.Current.LocalFolder, SizeUpdatesFile, CreationCollisionOption.ReplaceExisting);
            IBasicProperties props = StorageHelper.GetBasicProperties(file);

            Assert.AreEqual(0, props.Size);

            StorageHelper.WriteTextToFile(file, Text);

            IBasicProperties updatedProps = StorageHelper.GetBasicProperties(file);

            Assert.AreEqual(Text.Length, updatedProps.Size);
        }

        [Test]
        public void StorageFile_RetrievePropertiesAsync_ContainsSome()
        {
            IStorageFile file = StorageHelper.CreateStorageFile(ApplicationData.Current.LocalFolder, RetrievePropertiesFile, CreationCollisionOption.ReplaceExisting);
            IDictionary<string, object> props = StorageHelper.RetrieveProperties(file, null);

            Assert.IsTrue(props.Any());
        }
    }
}