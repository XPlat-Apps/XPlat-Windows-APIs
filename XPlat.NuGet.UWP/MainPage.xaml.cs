namespace XPlat.NuGet.UWP
{
    using System;

    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    using XPlat.Device.Display;
    using XPlat.Device.Geolocation;
    using XPlat.Device.Power;
    using XPlat.Storage;
    using XPlat.Storage.Pickers;

    public sealed partial class MainPage : Page
    {
        private Geolocator geolocator;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var request = new DisplayRequest();
            request.RequestActive();

            var file = await ApplicationData.Current.TemporaryFolder.CreateFileAsync(
                           "HelloWorld.txt",
                           CreationCollisionOption.OpenIfExists);

            await file.WriteTextAsync("Hello from the Windows app!");

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

            var singleFilePick = new FileOpenPicker();
            singleFilePick.FileTypeFilter.Add(".jpg");
            var pickedFile = await singleFilePick.PickSingleFileAsync();

            var multiFilePick = new FileOpenPicker();
            multiFilePick.FileTypeFilter.Add(".jpg");
            var pickedFiles = await multiFilePick.PickMultipleFilesAsync();

            var fileData = await file.ReadTextAsync();

            request.RequestRelease();
        }

        private void PowerManager_RemainingChargePercentChanged(object sender, int e)
        {
            var percent = e;
        }

        private void PowerManager_BatteryStatusChanged(object sender, BatteryStatus e)
        {
            var status = e;
        }

        private void Geolocator_PositionChanged(IGeolocator sender, PositionChangedEventArgs args)
        {
            var position = args.Position;
        }
    }
}