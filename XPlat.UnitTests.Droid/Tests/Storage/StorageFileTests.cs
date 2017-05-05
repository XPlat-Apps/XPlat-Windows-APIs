namespace XPlat.UnitTests.Droid.Tests.Storage
{
    using NUnit.Framework;

    using XPlat.Storage;
    using XPlat.UnitTests.Droid.Helpers;

    [TestFixture]
    public class StorageFileTests
    {
        [Test]
        public void StorageFile_ReturnsBasicProperties()
        {
            const string Text = "Hello, World!";

            var file = StorageHelper.CreateStorageFile(ApplicationData.Current.LocalFolder, "StorageFileReturnsBasicProperties.txt");
            var props = StorageHelper.GetBasicProperties(file);

            Assert.AreEqual(0, props.Size);

            StorageHelper.WriteTextToFile(file, Text);

            var updatedProps = StorageHelper.GetBasicProperties(file);

            Assert.AreEqual(Text.Length, updatedProps.Size);
        }
    }
}