namespace XPlat.NuGet.Droid
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Android.App;
    using Android.Media;
    using Android.OS;

    using XPlat.Device;
    using XPlat.Device.Display;
    using XPlat.Device.Geolocation;
    using XPlat.Media.Capture;
    using XPlat.NuGet.Droid.Models;
    using XPlat.Storage;
    using XPlat.Storage.Pickers;

    [Activity(Label = "XPlat.NuGet.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Geolocator geolocator;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var request = new DisplayRequest(this.Window);

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

            var settings = ApplicationData.Current.LocalSettings.Values.Get<List<Test>>("Tests");

            var file = await ApplicationData.Current.TemporaryFolder.CreateFileAsync(
                           "HelloWorld.txt",
                           CreationCollisionOption.OpenIfExists);

            await file.WriteTextAsync("Hello from the Android app!");

            var parent = await file.GetParentAsync();

            var batteryStatus = Device.Power.PowerManager.Current.BatteryStatus;
            var remainingPercentage = Device.Power.PowerManager.Current.RemainingChargePercent;

            Device.Power.PowerManager.Current.BatteryStatusChanged += this.PowerManager_BatteryStatusChanged;
            Device.Power.PowerManager.Current.RemainingChargePercentChanged +=
                this.PowerManager_RemainingChargePercentChanged;

            this.geolocator = new Geolocator(this)
                                  {
                                      DesiredAccuracy = PositionAccuracy.Default,
                                      MovementThreshold = 25
                                  };
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

            CameraCaptureUI dialog = new CameraCaptureUI(this);
            dialog.PhotoSettings.MaxResolution = CameraCaptureUIMaxPhotoResolution.Large3M;
            dialog.PhotoSettings.AllowCropping = false;

            IStorageFile capturedPhotoFile = await dialog.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (capturedPhotoFile != null)
            {
                var parentPhotoFolder = await capturedPhotoFile.GetParentAsync();

                var exif = new ExifInterface(capturedPhotoFile.Path);

                var orientation = exif.GetAttribute(ExifInterface.TagOrientation);
                var lat = exif.GetAttribute(ExifInterface.TagGpsLatitude);
                var lon = exif.GetAttribute(ExifInterface.TagGpsLongitude);

                var photoCopy = await capturedPhotoFile.CopyAsync(KnownFolders.CameraRoll);

                var photoAllProps = await capturedPhotoFile.Properties.RetrievePropertiesAsync(null);

                var photoProps = await capturedPhotoFile.Properties.GetImagePropertiesAsync();

                var photoBytes = await capturedPhotoFile.ReadBytesAsync();

#if DEBUG
                System.Diagnostics.Debug.WriteLine($"Image captured is {photoBytes.Length} and exists at {capturedPhotoFile.Path}.");
#endif
            }

            dialog.VideoSettings.MaxResolution = CameraCaptureUIMaxVideoResolution.HighestAvailable;
            dialog.VideoSettings.AllowTrimming = false;

            IStorageFile capturedVideoFile = await dialog.CaptureFileAsync(CameraCaptureUIMode.Video);

            if (capturedVideoFile != null)
            {
                var parentVideoFolder = await capturedVideoFile.GetParentAsync();

                var videoCopy = await capturedVideoFile.CopyAsync(KnownFolders.CameraRoll);

                var videoAllProps = await capturedVideoFile.Properties.RetrievePropertiesAsync(null);

                var videoProps = await capturedVideoFile.Properties.GetVideoPropertiesAsync();

                var videoBytes = await capturedVideoFile.ReadBytesAsync();

#if DEBUG
                System.Diagnostics.Debug.WriteLine($"Video captured is {videoBytes.Length} and exists at {capturedVideoFile.Path}.");
#endif
            }

            var fileData = await file.ReadTextAsync();

            request.RequestRelease();
        }

        private void PowerManager_RemainingChargePercentChanged(object sender, int i)
        {
            var percent = i;
        }

        private void PowerManager_BatteryStatusChanged(object sender, Device.Power.BatteryStatus batteryStatus)
        {
            var status = batteryStatus;
        }

        private void Geolocator_PositionChanged(IGeolocator sender, PositionChangedEventArgs args)
        {
            var position = args.Position;
        }
    }
}
