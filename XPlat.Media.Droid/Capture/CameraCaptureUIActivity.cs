namespace XPlat.Media.Capture
{
    using System;
    using System.Threading.Tasks;

    using Android;

    using Android.App;

    using Android.Content;
    using Android.OS;

    using Android.Content.PM;
    using Android.Support.V4.App;
    using Android.Provider;

    using Java.IO;

    using XPlat.Foundation;
    using XPlat.Storage;

    using System.Collections.Generic;

    using Android.Support.V4.Content;

    [Activity(NoHistory = false, LaunchMode = LaunchMode.Multiple)]
    internal class CameraCaptureUIActivity : Activity
    {
        internal const string IntentId = "id";

        internal const string IntentAction = "action";

        internal const string IntentFileName = "fileName";

        private int requestId;

        private string action;

        private string fileName;

        private File file;

        internal static event TypedEventHandler<Activity, CameraFileCaptured> CameraFileCaptured;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            List<string> permissions = new List<string>();

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) !=
                (int) Permission.Granted)
            {
                permissions.Add(Manifest.Permission.ReadExternalStorage);
            }

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) !=
                (int) Permission.Granted)
            {
                permissions.Add(Manifest.Permission.WriteExternalStorage);
            }

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) != (int)Permission.Granted)
            {
                permissions.Add(Manifest.Permission.Camera);
            }

            if (permissions.Count > 0)
            {
                ActivityCompat.RequestPermissions(this, permissions.ToArray(), 0);
            }
            else
            {
                this.StartCamera(savedInstanceState);
            }
        }

        private void StartCamera(Bundle savedInstanceState)
        {
            Bundle bundle = savedInstanceState ?? this.Intent.Extras;

            bool isComplete = bundle.GetBoolean("isComplete", false);
            this.requestId = bundle.GetInt(IntentId, 0);
            this.action = bundle.GetString(IntentAction);
            this.fileName = bundle.GetString(IntentFileName, $"{Guid.NewGuid()}.jpg");

            // Saves to the public repository (can't access internal)
            string filePath = Android.OS.Environment
                .GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim)
                .AbsolutePath;

            this.file = new File(filePath, this.fileName);

            Intent intent = null;
            try
            {
                intent = new Intent(this.action);
                intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(this.file));

                if (!isComplete)
                {
                    this.StartActivityForResult(intent, this.requestId);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif

                CameraFileCaptured?.Invoke(this, new CameraFileCaptured(this.requestId, null, true));
            }
            finally
            {
                intent?.Dispose();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions,
            Permission[] grantResults)
        {
            if (requestCode == 0)
            {
                bool granted = true;

                foreach (Permission grantResult in grantResults)
                {
                    if (!granted)
                    {
                        break;
                    }

                    granted = grantResult == Permission.Granted;
                }

                if (granted)
                {
                    this.StartCamera(null);
                }
                else
                {
                    this.Finish();
                }

            }
            else
            {
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            Task<CameraFileCaptured> result;

            if (resultCode == Result.Canceled)
            {
                result = Task.FromResult(new CameraFileCaptured(requestCode, null, true));
                result.ContinueWith(x => { CameraFileCaptured?.Invoke(this, x.Result); });
                this.Finish();
            }
            else
            {
                IStorageFile internalStorageFile =
                    await ApplicationData.Current.TemporaryFolder.CreateFileAsync(this.fileName);
                IStorageFile storageFile = await StorageFile.GetFileFromPathAsync(this.file.AbsolutePath);

                byte[] fileData = await storageFile.ReadBytesAsync();
                await internalStorageFile.WriteBytesAsync(fileData);

                await storageFile.DeleteAsync();

                CameraFileCaptured args = new CameraFileCaptured(requestCode, internalStorageFile, false);
                CameraFileCaptured?.Invoke(this, args);
                this.Finish();
            }
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutBoolean("isComplete", true);
            outState.PutInt(IntentId, this.requestId);
            outState.PutString(IntentFileName, this.fileName);
            outState.PutString(IntentAction, this.action);

            base.OnSaveInstanceState(outState);
        }
    }
}