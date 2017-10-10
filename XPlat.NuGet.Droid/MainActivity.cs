namespace XPlat.NuGet.Droid
{
    using System;
    using System.Collections.Generic;

    using Android.App;
    using Android.Content.PM;
    using Android.Media;
    using Android.OS;

    using XPlat.Device.Display;
    using XPlat.Device.Geolocation;
    using XPlat.Media.Capture;
    using XPlat.NuGet.Droid.Models;
    using XPlat.Storage;
    using XPlat.Storage.FileProperties;
    using XPlat.Storage.Pickers;

    using BatteryStatus = XPlat.Device.Power.BatteryStatus;

    [Activity(Label = "XPlat.NuGet.Droid", MainLauncher = true, Icon = "@drawable/icon", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class MainActivity : Activity
    {
        private Geolocator geolocator;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            DisplayRequest request = new DisplayRequest(this.Window);

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

            this.geolocator = new Geolocator(this)
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

            CameraCaptureUI dialog = new CameraCaptureUI(this);
            dialog.PhotoSettings.MaxResolution = CameraCaptureUIMaxPhotoResolution.Large3M;
            dialog.PhotoSettings.AllowCropping = false;

            IStorageFile capturedPhotoFile = await dialog.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (capturedPhotoFile != null)
            {
                IStorageFolder parentPhotoFolder = await capturedPhotoFile.GetParentAsync();

                ExifInterface exif = new ExifInterface(capturedPhotoFile.Path);

                string orientation = exif.GetAttribute(ExifInterface.TagOrientation);
                string lat = exif.GetAttribute(ExifInterface.TagGpsLatitude);
                string lon = exif.GetAttribute(ExifInterface.TagGpsLongitude);

                IStorageFile photoCopy = await capturedPhotoFile.CopyAsync(KnownFolders.CameraRoll);

                IDictionary<string, object> photoAllProps = await capturedPhotoFile.Properties.RetrievePropertiesAsync(null);

                IImageProperties photoProps = await capturedPhotoFile.Properties.GetImagePropertiesAsync();

                byte[] photoBytes = await capturedPhotoFile.ReadBytesAsync();

#if DEBUG
                System.Diagnostics.Debug.WriteLine($"Image captured is {photoBytes.Length} and exists at {capturedPhotoFile.Path}.");
#endif
            }

            dialog.VideoSettings.MaxResolution = CameraCaptureUIMaxVideoResolution.HighestAvailable;
            dialog.VideoSettings.AllowTrimming = false;

            IStorageFile capturedVideoFile = await dialog.CaptureFileAsync(CameraCaptureUIMode.Video);

            if (capturedVideoFile != null)
            {
                IStorageFolder parentVideoFolder = await capturedVideoFile.GetParentAsync();

                IStorageFile videoCopy = await capturedVideoFile.CopyAsync(KnownFolders.CameraRoll);

                IDictionary<string, object> videoAllProps = await capturedVideoFile.Properties.RetrievePropertiesAsync(null);

                IVideoProperties videoProps = await capturedVideoFile.Properties.GetVideoPropertiesAsync();

                byte[] videoBytes = await capturedVideoFile.ReadBytesAsync();

#if DEBUG
                System.Diagnostics.Debug.WriteLine($"Video captured is {videoBytes.Length} and exists at {capturedVideoFile.Path}.");
#endif
            }

            FileOpenPicker openPicker = new FileOpenPicker(this);
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".mp4");
            openPicker.FileTypeFilter.Add(".pdf");
            IStorageFile openedFile = await openPicker.PickSingleFileAsync();

            if (openedFile != null)
            {
                IStorageFolder parentPhotoFolder = await openedFile.GetParentAsync();

                IStorageFile photoCopy = await openedFile.CopyAsync(KnownFolders.CameraRoll);

                IDictionary<string, object> photoAllProps = await openedFile.Properties.RetrievePropertiesAsync(null);

                IImageProperties photoProps = await openedFile.Properties.GetImagePropertiesAsync();

                byte[] photoBytes = await openedFile.ReadBytesAsync();

#if DEBUG
                System.Diagnostics.Debug.WriteLine($"File captured is {photoBytes.Length} and exists at {openedFile.Path}.");
#endif
            }
            
            string fileData = await file.ReadTextAsync();

            request.RequestRelease();
        }

        private void PowerManager_RemainingChargePercentChanged(object sender, int i)
        {
            int percent = i;
        }

        private void PowerManager_BatteryStatusChanged(object sender, BatteryStatus batteryStatus)
        {
            BatteryStatus status = batteryStatus;
        }

        private void Geolocator_PositionChanged(IGeolocator sender, PositionChangedEventArgs args)
        {
            Geoposition position = args.Position;
        }
    }
}
