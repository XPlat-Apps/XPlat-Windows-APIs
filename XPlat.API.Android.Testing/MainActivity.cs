namespace XPlat.API.Android.Testing
{
    using System;

    using global::Android.App;
    using global::Android.OS;

    using XPlat.API.Device.Display;
    using XPlat.API.Device.Geolocation;
    using XPlat.API.Storage;

    using PowerManager = XPlat.API.Device.Power.PowerManager;
    using BatteryStatus = XPlat.API.Device.Power.BatteryStatus;

    [Activity(Label = "XPlat.API.Android.Testing", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Geolocator geolocator;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var request = new DisplayRequest(this.Window);
            request.RequestActive();

            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                           "HelloWorld.txt",
                           CreationCollisionOption.OpenIfExists);

            await file.WriteTextAsync("Hello from the Android app!");

            var batteryStatus = PowerManager.Current.BatteryStatus;
            var remainingPercentage = PowerManager.Current.RemainingChargePercent;

            PowerManager.Current.BatteryStatusChanged += this.PowerManager_BatteryStatusChanged;
            PowerManager.Current.RemainingChargePercentChanged += this.PowerManager_RemainingChargePercentChanged;

            this.geolocator = new Geolocator(this) { DesiredAccuracy = PositionAccuracy.Default, MovementThreshold = 25 };
            this.geolocator.PositionChanged += this.Geolocator_PositionChanged;
            var access = await this.geolocator.RequestAccessAsync();

            if (access == GeolocationAccessStatus.Allowed)
            {
                var currentLocation = await this.geolocator.GetGeopositionAsync(new TimeSpan(0, 0, 10), new TimeSpan(0, 0, 5));

                if (currentLocation != null)
                {
                    // ToDo
                }
            }

            request.RequestRelease();
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