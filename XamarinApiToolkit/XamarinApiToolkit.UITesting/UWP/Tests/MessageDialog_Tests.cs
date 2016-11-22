namespace XamarinApiToolkit.UITesting.UWP.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenQA.Selenium;

    using XamarinApiToolkit.Tests.Common.TestHelpers;

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
        public void MessageDialog_ShowBasicDialogWithContent_Test()
        {
            // Navigate into message dialog tests.
            var messageDialogBtn = UWPAppHelper.AppSession.FindElementByAccessibilityId("MessageDialogBtn");
            Assert.IsNotNull(messageDialogBtn, "Expected to find the 'MessageDialogBtn'.");
            messageDialogBtn.Click();

            // Launch a basic content dialog.
            var basicContentDialogBtn = UWPAppHelper.AppSession.FindElementByAccessibilityId("BasicContentDialogBtn");
            Assert.IsNotNull(basicContentDialogBtn, "Expected to find the 'BasicContentDialogBtn'.");
            basicContentDialogBtn.Click();

            var desktopApp = UWPAppHelper.DesktopSession.FindElementByName("XamarinApiToolkit.UWP.App");
            var popup = desktopApp.FindElementByAccessibilityId("Popup");
            if (popup != null)
            {
                var dialogString = popup.FindElementByAccessibilityId("Content_String");
                if (dialogString != null)
                {
                    // Assures we have the right dialog.
                    Assert.AreEqual(dialogString.Text, MessageDialogHelpers.BasicContent);
                }
                else
                {
                    Assert.Fail("Expected to find a content string for the popup.");
                }

                var toolbar = popup.FindElementByAccessibilityId("ButtonBar");
                if (toolbar != null)
                {
                    // ToDo, check if we have the correct button (close)
                }
                else
                {
                    Assert.Fail("Expected to find a button bar for the popup.");
                }
            }
            else
            {
                Assert.Fail("Expected to find a popup.");
            }
        }
    }
}