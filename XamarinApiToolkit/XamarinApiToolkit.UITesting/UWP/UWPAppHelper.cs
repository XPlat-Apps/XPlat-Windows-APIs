namespace XamarinApiToolkit.UITesting.UWP
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OpenQA.Selenium.Appium.iOS;
    using OpenQA.Selenium.Remote;

    public static class UWPAppHelper
    {
        private const string AppiumUrl = "http://127.0.0.1:4723";

        private const string AppId = "59734f6e-cecd-4f9f-bb2d-bdf86a4f2960_97kzn20wpfhj8!App";

        public static IOSDriver<IOSElement> AppSession { get; set; }

        // Temporary placeholder until Windows namespace exists

        public static IOSDriver<IOSElement> DesktopSession { get; set; }

        // Temporary placeholder until Windows namespace exists

        public static void Initialize()
        {
            if (AppSession != null)
            {
                Cleanup();
            }

            // Launch the app
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", AppId);
            AppSession = new IOSDriver<IOSElement>(new Uri(AppiumUrl), appCapabilities);
            Assert.IsNotNull(AppSession);
            AppSession.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(2));

            // Create a session for Desktop
            DesiredCapabilities desktopCapabilities = new DesiredCapabilities();
            desktopCapabilities.SetCapability("app", "Root");
            DesktopSession = new IOSDriver<IOSElement>(new Uri(AppiumUrl), desktopCapabilities);
            Assert.IsNotNull(DesktopSession);
        }

        public static void Cleanup()
        {
            if (AppSession == null)
            {
                return;
            }

            AppSession.Quit();
            AppSession = null;
        }
    }
}