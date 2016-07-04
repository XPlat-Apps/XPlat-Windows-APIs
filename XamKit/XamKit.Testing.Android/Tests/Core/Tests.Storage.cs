namespace XamKit.Testing.Android.Tests.Core
{
    using NUnit.Framework;

    using XamKit.Core.Storage;

    [TestFixture]
    public partial class Tests
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
        public void ApplicationStorage_CanAccessCurrent()
        {
            Assert.IsNotNull(ApplicationStorage.Current);
        }

        [Test]
        public void ApplicationStorage_CanAccessLocalFolder()
        {
            Assert.IsNotNull(ApplicationStorage.Current.LocalFolder);
        }

        [Test]
        public void ApplicationStorage_CannotAccessRoamingFolder()
        {
            Assert.IsNull(ApplicationStorage.Current.RoamingFolder);
        }
    }
}