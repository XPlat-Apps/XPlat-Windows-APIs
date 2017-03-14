namespace XPlat.API.UWP.Testing
{
    using System;
    using System.Linq;

    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    using XPlat.API.Device.Display;
    using XPlat.API.Device.Geolocation;
    using XPlat.API.Device.Power;
    using XPlat.API.Storage;
    using XPlat.API.Storage.Pickers;

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

            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("HelloWorld.txt", CreationCollisionOption.OpenIfExists);
            await file.WriteTextAsync("Hello from the UWP app!");

            var props = (await file.GetPropertiesAsync()).ToList();

            var batteryStatus = PowerManager.Current.BatteryStatus;
            var remainingPercentage = PowerManager.Current.RemainingChargePercent;

            PowerManager.Current.BatteryStatusChanged += this.PowerManager_BatteryStatusChanged;
            PowerManager.Current.RemainingChargePercentChanged += this.PowerManager_RemainingChargePercentChanged;

            this.geolocator = new Geolocator { DesiredAccuracy = PositionAccuracy.Default, MovementThreshold = 25 };
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

            var singleFilePick = new FileOpenPicker();
            singleFilePick.FileTypeFilter.Add(".png");
            singleFilePick.FileTypeFilter.Add(".jpg");
            singleFilePick.FileTypeFilter.Add(".mp3");
            singleFilePick.FileTypeFilter.Add(".mp4");
            var pickedFile = await singleFilePick.PickSingleFileAsync();

            if (pickedFile != null)
            {
                var pickedFileProps = await pickedFile.GetPropertiesAsync();
            }

            var multiFilePick = new FileOpenPicker();
            multiFilePick.FileTypeFilter.Add(".png");
            multiFilePick.FileTypeFilter.Add(".jpg");
            multiFilePick.FileTypeFilter.Add(".mp3");
            multiFilePick.FileTypeFilter.Add(".mp4");
            var pickedFiles = await multiFilePick.PickMultipleFilesAsync();

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