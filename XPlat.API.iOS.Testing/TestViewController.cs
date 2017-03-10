namespace XPlat.API.iOS.Testing
{
    using System;

    using UIKit;

    using XPlat.API.Device.Display;
    using XPlat.API.Device.Geolocation;
    using XPlat.API.Device.Power;
    using XPlat.API.Storage;

    public partial class TestViewController : UIViewController
    {
        public TestViewController()
            : base("TestViewController", null)
        {
        }

        public override async void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            var request = new DisplayRequest();
            request.RequestActive();

            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                           "HelloWorld.txt",
                           CreationCollisionOption.OpenIfExists);

            await file.WriteTextAsync("Hello from the iOS app!");

            var batteryStatus = PowerManager.Current.BatteryStatus;
            var remainingPercentage = PowerManager.Current.RemainingChargePercent;

            PowerManager.Current.BatteryStatusChanged += this.PowerManager_BatteryStatusChanged;
            PowerManager.Current.RemainingChargePercentChanged += this.PowerManager_RemainingChargePercentChanged;

            request.RequestRelease();
        }

        /// <inheritdoc />
        public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
        {
            return toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown;
        }

        private void PowerManager_RemainingChargePercentChanged(object sender, int i)
        {
            var percent = i;
        }

        private void PowerManager_BatteryStatusChanged(object sender, BatteryStatus batteryStatus)
        {
            var status = batteryStatus;
        }
    }
}