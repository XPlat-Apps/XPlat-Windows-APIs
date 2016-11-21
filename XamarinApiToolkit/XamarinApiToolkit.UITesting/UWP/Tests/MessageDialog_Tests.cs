namespace XamarinApiToolkit.UITesting.UWP.Tests
{
    using System;

    using System.Globalization;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenQA.Selenium.Appium;

    using OpenQA.Selenium.Appium.iOS; // Temporary placeholder until Windows namespace exists

    using OpenQA.Selenium.Remote;

    [TestClass]
    public class MessageDialog_Tests
    {
        [ClassInitialize]
        public static void Setup(TestContext context)
        {
            UWPAppHelper.Initialize();
        }

        [ClassCleanup]
        public static void TearDown()
        {
            UWPAppHelper.Cleanup();
        }

        [TestMethod]
        public void AppLaunch_Test()
        {
            Thread.Sleep(6000);

            Assert.IsTrue(true);
        }
    }
}