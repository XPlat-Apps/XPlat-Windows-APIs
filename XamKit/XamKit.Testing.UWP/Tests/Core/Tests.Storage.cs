namespace XamKit.Testing.UWP
{
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

    using XamKit.Core.Common.Storage;
    using XamKit.Core.Storage;

    [TestClass]
    public partial class Tests
    {
        [TestMethod]
        public void ApplicationStorage_Current_IsNotNull()
        {
            Assert.IsNotNull(ApplicationStorage.Current);
        }

        [TestMethod]
        public void ApplicationStorage_CurrentLocalFolder_IsNotNull()
        {
            Assert.IsNotNull(ApplicationStorage.Current.LocalFolder);
        }

        [TestMethod]
        public void ApplicationStorage_CurrentRoamingFolder_IsNotNull()
        {
            Assert.IsNotNull(ApplicationStorage.Current.RoamingFolder);
        }

        [TestMethod]
        public async void ApplicationStorage_CurrentLocalFolder_CreateFileAsync_Exists()
        {
            var file = await ApplicationStorage.Current.LocalFolder.CreateFileAsync(
                "test.txt",
                FileStoreCreationOption.ReplaceIfExists);

            Assert.IsTrue(file.Exists);
        }
    }
}