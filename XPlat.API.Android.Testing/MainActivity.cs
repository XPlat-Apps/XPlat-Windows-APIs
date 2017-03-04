namespace XPlat.API.Android.Testing
{
    using global::Android.App;
    using global::Android.OS;

    using XPlat.API.Device.Display;
    using XPlat.API.Storage;

    [Activity(Label = "XPlat.API.Android.Testing", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var request = new DisplayRequest(this.Window);
            request.RequestActive();

            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("HelloWorld.txt", CreationCollisionOption.OpenIfExists);

            await file.WriteTextAsync("Hello from the Android app!");

            var batteryStatus = Device.Power.PowerManager.Current.BatteryStatus;
            var remainingPercentage = Device.Power.PowerManager.Current.RemainingChargePercent;

            Device.Power.PowerManager.Current.BatteryStatusChanged += this.OnBatteryStatusChanged;
            Device.Power.PowerManager.Current.RemainingChargePercentChanged += this.OnRemainingChargePercentChanged;

            request.RequestRelease();
        }

        private void OnRemainingChargePercentChanged(object sender, int i)
        {
            var percent = i;
        }

        private void OnBatteryStatusChanged(object sender, Device.Power.BatteryStatus batteryStatus)
        {
            var status = batteryStatus;
        }
    }
}

