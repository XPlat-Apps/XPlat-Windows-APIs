namespace XPlat.UnitTests.iOS.Tests.Storage
{
    using NUnit.Framework;

    using XPlat.Storage;
    using XPlat.UnitTests.iOS.Helpers;

    [TestFixture]
    public class ApplicationDataTests
    {
        [Test]
        public void ApplicationData_CanAccessCurrent()
        {
            Assert.IsNotNull(ApplicationData.Current);
        }

        [Test]
        public void ApplicationData_CanAccessLocalFolder()
        {
            Assert.IsNotNull(ApplicationData.Current.LocalFolder);
        }

        [Test]
        public void ApplicationData_CanAccessTemporaryFolder()
        {
            Assert.IsNotNull(ApplicationData.Current.TemporaryFolder);
        }

        [Test]
        public void ApplicationData_CannotAccessRoamingFolder()
        {
            Assert.IsNull(ApplicationData.Current.RoamingFolder);
        }

        [Test]
        public void ApplicationData_CreatedFileExists()
        {
            IStorageFile file = StorageHelper.CreateStorageFile(
                ApplicationData.Current.LocalFolder,
                "test.txt",
                CreationCollisionOption.ReplaceExisting);
            Assert.IsTrue(file.Exists);
        }

        [Test]
        public void ApplicationData_StorageItemCreationExceptionThrownWhenCreatingFileThatExists()
        {
            const string FileName = "test.txt";

            StorageHelper.CreateStorageFile(
                ApplicationData.Current.LocalFolder,
                FileName,
                CreationCollisionOption.ReplaceExisting);

            Assert.Throws<StorageItemCreationException>(
                () => StorageHelper.CreateStorageFile(ApplicationData.Current.LocalFolder, FileName));
        }

        [Test]
        public void ApplicationData_DeleteCreatedFileDoesNotExist()
        {
            IStorageFile file = StorageHelper.CreateStorageFile(
                ApplicationData.Current.LocalFolder,
                "test.txt",
                CreationCollisionOption.ReplaceExisting);

            StorageHelper.DeleteStorageItem(file);

            Assert.IsFalse(file.Exists);
        }

        [Test]
        public void ApplicationData_StorageItemNotFoundExceptionThrownWhenWritingTextToDeletedFile()
        {
            IStorageFile file = StorageHelper.CreateStorageFile(
                ApplicationData.Current.LocalFolder,
                "test.txt",
                CreationCollisionOption.ReplaceExisting);

            StorageHelper.DeleteStorageItem(file);

            Assert.Throws<StorageItemNotFoundException>(() => StorageHelper.WriteTextToFile(file, "Hello, World!"));
        }
    }
}