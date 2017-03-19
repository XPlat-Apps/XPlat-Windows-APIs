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
        private Geolocator geolocator;

        public TestViewController()
            : base("TestViewController", null)
        {
        }

        public override async void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            var request = new DisplayRequest();
            request.RequestActive();

            var file = await ApplicationData.Current.TemporaryFolder.CreateFileAsync(
                           "HelloWorld.txt",
                           CreationCollisionOption.OpenIfExists);

            await file.WriteTextAsync("Hello from the Android app!");

            var batteryStatus = PowerManager.Current.BatteryStatus;
            var remainingPercentage = PowerManager.Current.RemainingChargePercent;

            PowerManager.Current.BatteryStatusChanged += this.PowerManager_BatteryStatusChanged;
            PowerManager.Current.RemainingChargePercentChanged +=
                this.PowerManager_RemainingChargePercentChanged;

            this.geolocator = new Geolocator { DesiredAccuracy = PositionAccuracy.Default, MovementThreshold = 25 };
            this.geolocator.PositionChanged += this.Geolocator_PositionChanged;
            var access = await this.geolocator.RequestAccessAsync();

            if (access == GeolocationAccessStatus.Allowed)
            {
                var currentLocation = await this.geolocator.GetGeopositionAsync(
                                          new TimeSpan(0, 0, 10),
                                          new TimeSpan(0, 0, 5));

                if (currentLocation != null)
                {
                    // ToDo
                }
            }

            //var singleFilePick = new FileOpenPicker();
            //singleFilePick.FileTypeFilter.Add(".jpg");
            //var pickedFile = await singleFilePick.PickSingleFileAsync();


            //var multiFilePick = new FileOpenPicker();
            //multiFilePick.FileTypeFilter.Add(".jpg");
            //var pickedFiles = await multiFilePick.PickMultipleFilesAsync();

            var fileData = await file.ReadTextAsync();

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

        private void Geolocator_PositionChanged(IGeolocator sender, PositionChangedEventArgs args)
        {
            var position = args.Position;
        }
    }
}