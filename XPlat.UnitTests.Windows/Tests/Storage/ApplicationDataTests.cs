using Microsoft.VisualStudio.TestTools.UnitTesting;
using XPlat.Storage;
using XPlat.UnitTests.Windows.Helpers;

namespace XPlat.UnitTests.Windows.Tests.Storage
{
    [TestClass]
    public class ApplicationDataTests
    {
        [TestMethod]
        public void ApplicationData_CanAccessCurrent()
        {
            Assert.IsNotNull(ApplicationData.Current);
        }

        [TestMethod]
        public void ApplicationData_CanAccessLocalFolder()
        {
            Assert.IsNotNull(ApplicationData.Current.LocalFolder);
        }

        [TestMethod]
        public void ApplicationData_CanAccessTemporaryFolder()
        {
            Assert.IsNotNull(ApplicationData.Current.TemporaryFolder);
        }

        [TestMethod]
        public void ApplicationData_CanAccessRoamingFolder()
        {
            Assert.IsNotNull(ApplicationData.Current.RoamingFolder);
        }

        [TestMethod]
        public void ApplicationData_CreatedFileExists()
        {
            IStorageFile file = StorageHelper.CreateStorageFile(
                ApplicationData.Current.LocalFolder,
                "test.txt",
                CreationCollisionOption.ReplaceExisting);
            Assert.IsTrue(file.Exists);
        }

        [TestMethod]
        public void ApplicationData_StorageItemCreationExceptionThrownWhenCreatingFileThatExists()
        {
            const string FileName = "test.txt";

            StorageHelper.CreateStorageFile(
                ApplicationData.Current.LocalFolder,
                FileName,
                CreationCollisionOption.ReplaceExisting);

            Assert.ThrowsException<StorageItemCreationException>(
                () => StorageHelper.CreateStorageFile(ApplicationData.Current.LocalFolder, FileName));
        }

        [TestMethod]
        public void ApplicationData_DeleteCreatedFileDoesNotExist()
        {
            IStorageFile file = StorageHelper.CreateStorageFile(
                ApplicationData.Current.LocalFolder,
                "test.txt",
                CreationCollisionOption.ReplaceExisting);

            StorageHelper.DeleteStorageItem(file);

            Assert.IsFalse(file.Exists);
        }

        [TestMethod]
        public void ApplicationData_StorageItemNotFoundExceptionThrownWhenWritingTextToDeletedFile()
        {
            IStorageFile file = StorageHelper.CreateStorageFile(
                ApplicationData.Current.LocalFolder,
                "test.txt",
                CreationCollisionOption.ReplaceExisting);

            StorageHelper.DeleteStorageItem(file);

            Assert.ThrowsException<StorageItemNotFoundException>(() => StorageHelper.WriteTextToFile(file, "Hello, World!"));
        }
    }
}