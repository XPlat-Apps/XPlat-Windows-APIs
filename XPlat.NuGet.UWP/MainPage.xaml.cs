namespace XPlat.NuGet.UWP
{
    using System;
    using System.Collections.Generic;

    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    using XPlat.Device.Display;
    using XPlat.Device.Geolocation;
    using XPlat.Device.Power;
    using XPlat.Media.Capture;
    using XPlat.NuGet.UWP.Models;
    using XPlat.Storage;
    using XPlat.Storage.FileProperties;

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

            DisplayRequest request = new DisplayRequest();
            request.RequestActive();

            // Test with list of strings
            List<string> values = new List<string> { "Hello", "World", "ASDF" };
            ApplicationData.Current.LocalSettings.Values["Roles"] = values;

            // Test with list of objects
            List<Test> tests = new List<Test>
                                   {
                                       new Test
                                           {
                                               Date = DateTime.Now,
                                               Name = "Hello, World!",
                                               NestedTest =
                                                   new Test
                                                       {
                                                           Date = DateTime.MinValue,
                                                           Name = "Nested Hello!"
                                                       }
                                           },
                                       new Test
                                           {
                                               Date = DateTime.MinValue,
                                               Name = "Hello, World AGAIN!",
                                               NestedTest =
                                                   new Test
                                                       {
                                                           Date = DateTime.UtcNow,
                                                           Name = "Nested Hello AGAIN!"
                                                       }
                                           }
                                   };
            ApplicationData.Current.LocalSettings.Values["Tests"] = tests;

            List<Test> settings = ApplicationData.Current.LocalSettings.Values.Get<List<Test>>("Tests");

            IStorageFile file = await ApplicationData.Current.TemporaryFolder.CreateFileAsync(
                           "HelloWorld.txt",
                           CreationCollisionOption.OpenIfExists);

            await file.WriteTextAsync("Hello from the Android app!");

            IStorageFolder parent = await file.GetParentAsync();

            BatteryStatus batteryStatus = Device.Power.PowerManager.Current.BatteryStatus;
            int remainingPercentage = Device.Power.PowerManager.Current.RemainingChargePercent;

            Device.Power.PowerManager.Current.BatteryStatusChanged += this.PowerManager_BatteryStatusChanged;
            Device.Power.PowerManager.Current.RemainingChargePercentChanged +=
                this.PowerManager_RemainingChargePercentChanged;

            this.geolocator = new Geolocator()
                                  {
                                      DesiredAccuracy = PositionAccuracy.Default,
                                      MovementThreshold = 25
                                  };
            this.geolocator.PositionChanged += this.Geolocator_PositionChanged;

            GeolocationAccessStatus access = await this.geolocator.RequestAccessAsync();
            if (access == GeolocationAccessStatus.Allowed)
            {
                Geoposition currentLocation = await this.geolocator.GetGeopositionAsync(
                                          new TimeSpan(0, 0, 10),
                                          new TimeSpan(0, 0, 5));
                if (currentLocation != null)
                {
                    // ToDo
                }
            }

            CameraCaptureUI dialog = new CameraCaptureUI();
            dialog.PhotoSettings.MaxResolution = CameraCaptureUIMaxPhotoResolution.Large3M;
            dialog.PhotoSettings.AllowCropping = false;

            IStorageFile capturedPhotoFile = await dialog.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (capturedPhotoFile != null)
            {
                IStorageFolder parentFolder = await capturedPhotoFile.GetParentAsync();

                IStorageFile copy = await capturedPhotoFile.CopyAsync(KnownFolders.CameraRoll);

                IDictionary<string, object> props = await capturedPhotoFile.Properties.RetrievePropertiesAsync(null);

                IImageProperties imageProps = await capturedPhotoFile.Properties.GetImagePropertiesAsync();

                byte[] bytes = await capturedPhotoFile.ReadBytesAsync();
            }

            dialog.VideoSettings.MaxResolution = CameraCaptureUIMaxVideoResolution.HighestAvailable;
            dialog.VideoSettings.AllowTrimming = false;
            dialog.VideoSettings.MaxDurationInSeconds = 10;

            IStorageFile capturedVideoFile = await dialog.CaptureFileAsync(CameraCaptureUIMode.Video);

            if (capturedVideoFile != null)
            {
                IStorageFolder parentFolder = await capturedVideoFile.GetParentAsync();

                IStorageFile copy = await capturedVideoFile.CopyAsync(KnownFolders.CameraRoll);

                IDictionary<string, object> props = await capturedVideoFile.Properties.RetrievePropertiesAsync(null);

                IImageProperties imageProps = await capturedVideoFile.Properties.GetImagePropertiesAsync();

                byte[] bytes = await capturedVideoFile.ReadBytesAsync();
            }

            //var singleFilePick = new FileOpenPicker(this);
            //singleFilePick.FileTypeFilter.Add(".jpg");
            //var pickedFile = await singleFilePick.PickSingleFileAsync();

            //if (pickedFile != null)
            //{
            //    var imageProps = await pickedFile.Properties.GetImagePropertiesAsync();
            //}

            //var multiFilePick = new FileOpenPicker(this);
            //multiFilePick.FileTypeFilter.Add(".jpg");
            //var pickedFiles = await multiFilePick.PickMultipleFilesAsync();

            string fileData = await file.ReadTextAsync();

            request.RequestRelease();
        }

        private void PowerManager_RemainingChargePercentChanged(object sender, int e)
        {
            int percent = e;
        }

        private void PowerManager_BatteryStatusChanged(object sender, BatteryStatus e)
        {
            BatteryStatus status = e;
        }

        private void Geolocator_PositionChanged(IGeolocator sender, PositionChangedEventArgs args)
        {
            Geoposition position = args.Position;
        }
    }
}