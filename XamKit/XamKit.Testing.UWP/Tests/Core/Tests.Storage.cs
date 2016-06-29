namespace XamKit.Testing.UWP.Tests.Core
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

    using XamKit.Core.Common.Storage;
    using XamKit.Core.Storage;
    using XamKit.Testing.UWP.UnitTestFramework;

    [TestClass]
    public partial class Tests
    {
        [TestMethod]
        public void ApplicationStorage_Current_IsNotNull()
        {
            Assert.IsNotNull(ApplicationStorage.Current);
        }

        [TestMethod]
        public void ApplicationStorage_Current_LocalFolder_IsNotNull()
        {
            Assert.IsNotNull(ApplicationStorage.Current.LocalFolder);
        }

        [TestMethod]
        public void ApplicationStorage_Current_RoamingFolder_IsNotNull()
        {
            Assert.IsNotNull(ApplicationStorage.Current.RoamingFolder);
        }

        [TestMethod]
        public async Task ApplicationStorage_Current_LocalFolder_CreatedFileExists()
        {
            var file =
                await
                ApplicationStorage.Current.LocalFolder.CreateFileAsync(
                    "test.txt",
                    FileStoreCreationOption.ReplaceIfExists);

            Assert.IsTrue(file.Exists);
        }

        [TestMethod]
        public async Task ApplicationStorage_Current_LocalFolder_NotSupportedExceptionThrownWhenCreatingFileThatExists()
        {
            const string FileName = "test.txt";

            await
                ApplicationStorage.Current.LocalFolder.CreateFileAsync(
                    FileName,
                    FileStoreCreationOption.ReplaceIfExists);

            await
                AssertAsync.ThrowsExceptionAsync<Exception>(
                    async () => await ApplicationStorage.Current.LocalFolder.CreateFileAsync(FileName));
        }

        [TestMethod]
        public async Task ApplicationStorage_Current_LocalFolder_CreatedFileIsDeleted()
        {
            var file =
                await
                ApplicationStorage.Current.LocalFolder.CreateFileAsync(
                    "test.txt",
                    FileStoreCreationOption.ReplaceIfExists);

            await file.DeleteAsync();

            Assert.IsFalse(file.Exists);
        }

        [TestMethod]
        public async Task ApplicationStorage_Current_LocalFolder_NotSupportedExceptionThrownWhenWritingTextToDeletedFile
            ()
        {
            var file =
                await
                ApplicationStorage.Current.LocalFolder.CreateFileAsync(
                    "test.txt",
                    FileStoreCreationOption.ReplaceIfExists);

            await file.DeleteAsync();

            await
                AssertAsync.ThrowsExceptionAsync<NotSupportedException>(
                    async () => await file.WriteTextAsync("Hello, World"));
        }
    }
}