namespace XamarinApiToolkit.Android.Tests.Tests.Storage
{
    using NUnit.Framework;

    using XamarinApiToolkit.Storage;

    [TestFixture]
    public class Test_AppData
    {

        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public void Tear()
        {
        }

        [Test]
        public void Test_AppDataExists()
        {
            Assert.True(AppData.Current != null);
        }

        [Test]
        public void Test_AppDataLocalFolderExists()
        {
            Assert.True(AppData.Current.LocalFolder != null && AppData.Current.LocalFolder.Exists);
        }

        [Test]
        public void Test_AppDataRoamingFolderDoesNotExist()
        {
            Assert.True(AppData.Current.RoamingFolder == null);
        }

        [Test]
        public void Test_AppDataTemporaryFolderDoesNotExist()
        {
            Assert.True(AppData.Current.TemporaryFolder == null);
        }

        [Test]
        public void Test_AppDataLocalSettingsExists()
        {
            Assert.True(AppData.Current.LocalSettings != null);
        }
    }
}