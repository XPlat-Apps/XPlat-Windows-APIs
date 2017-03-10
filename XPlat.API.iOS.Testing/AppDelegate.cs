namespace XPlat.API.iOS.Testing
{
    using global::Foundation;

    using UIKit;

    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations

        /// <inheritdoc />
        public override UIWindow Window { get; set; }

        public TestViewController ViewController { get; set; }

        /// <inheritdoc />
        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            this.Window = new UIWindow(UIScreen.MainScreen.Bounds);

            this.ViewController = new TestViewController();
            this.Window.RootViewController = this.ViewController;
            this.Window.MakeKeyAndVisible();

            return true;
        }
    }
}