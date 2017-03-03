namespace XPlat.API.Android.Testing
{
    using global::Android.App;
    using global::Android.OS;

    using XPlat.API.Storage;

    [Activity(Label = "XPlat.API.Android.Testing", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("HelloWorld.txt", FileStoreCreationOption.OpenIfExists);

            var batteryStatus = Device.Power.PowerManager.Current.BatteryStatus;
            var remainingPercentage = Device.Power.PowerManager.Current.RemainingChargePercent;

            Device.Power.PowerManager.Current.BatteryStatusChanged += this.OnBatteryStatusChanged;
            Device.Power.PowerManager.Current.RemainingChargePercentChanged += this.OnRemainingChargePercentChanged;
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

