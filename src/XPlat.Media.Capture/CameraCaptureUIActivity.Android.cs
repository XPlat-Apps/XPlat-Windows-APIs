﻿#if __ANDROID__
namespace XPlat.Media.Capture
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Android;
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.OS;
    using Android.Provider;
    using Android.Support.V4.App;
    using Android.Support.V4.Content;
    using Java.IO;
    using XPlat.Foundation;
    using XPlat.Helpers;
    using XPlat.Storage;
    using XPlat.Storage.Helpers;
    using Uri = Android.Net.Uri;

    [Activity(NoHistory = false, LaunchMode = LaunchMode.Multiple, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    internal class CameraCaptureUIActivity : Activity
    {
        internal const string IntentId = "id";

        internal const string IntentAction = "action";

        internal const string IntentFileName = "fileName";

        private int requestId;

        private string action;

        private string fileName;

        private File publicFile;

        private File contentProviderFile;

        private ConfigChanges config;

        private bool isComplete;

        internal static event TypedEventHandler<Activity, CameraFileCaptured> CameraFileCaptured;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Bundle bundle = savedInstanceState ?? this.Intent.Extras;

            this.isComplete = bundle.GetBoolean("isComplete", false);
            this.requestId = bundle.GetInt(IntentId, 0);
            this.action = bundle.GetString(IntentAction);
            this.fileName = bundle.GetString(IntentFileName, $"{Guid.NewGuid()}.jpg");

            List<string> permissions = new List<string>();

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) !=
                (int)Permission.Granted)
            {
                permissions.Add(Manifest.Permission.ReadExternalStorage);
            }

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) !=
                (int)Permission.Granted)
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
                this.StartCamera();
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            this.config = this.ChangingConfigurations;
        }

        private void StartCamera()
        {
            Intent intent = null;
            try
            {
                intent = new Intent(this.action);

                if (PackageManagerHelper.CheckContentProviderExists(nameof(FileProvider)))
                {
                    try
                    {
                        StorageHelper.CreateStorageFile(ApplicationData.Current.TemporaryFolder, this.fileName);
                    }
                    catch (StorageItemCreationException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.ToString());
                    }

                    this.contentProviderFile = new File(ApplicationData.Current.TemporaryFolder.Path, this.fileName);

                    Uri contentUri = FileProvider.GetUriForFile(this, this.PackageName, this.contentProviderFile);

                    intent.PutExtra(MediaStore.ExtraOutput, contentUri);

                    intent.AddFlags(ActivityFlags.GrantWriteUriPermission);
                    intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                }
                else
                {
                    // Saves to the public repository (can't access internal)
                    string filePath = Android.OS.Environment
                        .GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim)
                        .AbsolutePath;

                    this.publicFile = new File(filePath, this.fileName);
                    intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(this.publicFile));
                }

                if (!this.isComplete)
                {
                    if (intent.ResolveActivity(this.PackageManager) != null)
                    {
                        this.StartActivityForResult(intent, this.requestId);
                    }
                    else
                    {
                        this.Finish();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                CameraFileCaptured?.Invoke(this, new CameraFileCaptured(this.requestId, null, true, false));
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
                    this.StartCamera();
                }
                else
                {
                    Task<CameraFileCaptured> result =
                        Task.FromResult(new CameraFileCaptured(this.requestId, null, true, true));
                    result.ContinueWith(x => { CameraFileCaptured?.Invoke(this, x.Result); });
                    this.Finish();
                }

            }
            else
            {
                base.OnRequestPermissionsResult(this.requestId, permissions, grantResults);
            }
        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (resultCode == Result.Canceled)
            {
                Task<CameraFileCaptured>
                    result = Task.FromResult(new CameraFileCaptured(requestCode, null, true, false));
                result.ContinueWith(x => { CameraFileCaptured?.Invoke(this, x.Result); });
            }
            else
            {
                this.RealizeOriginalFile();

                IStorageFile storageFile = null;
                if (this.publicFile != null)
                {
                    storageFile = await StorageFile.GetFileFromPathAsync(this.publicFile.AbsolutePath);
                    await storageFile.MoveAsync(ApplicationData.Current.TemporaryFolder);
                }
                else if (this.contentProviderFile != null)
                {
                    storageFile = await StorageFile.GetFileFromPathAsync(this.contentProviderFile.AbsolutePath);
                }

                CameraFileCaptured args = new CameraFileCaptured(requestCode, storageFile, false, false);

                CameraFileCaptured?.Invoke(this, args);
            }

            this.Finish();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutBoolean("isComplete", true);
            outState.PutInt(IntentId, this.requestId);
            outState.PutString(IntentFileName, this.fileName);
            outState.PutString(IntentAction, this.action);

            base.OnSaveInstanceState(outState);
        }

        private void RealizeOriginalFile()
        {
            if (PackageManagerHelper.CheckContentProviderExists(nameof(FileProvider)))
            {
                this.contentProviderFile = new File(ApplicationData.Current.TemporaryFolder.Path, this.fileName);
            }
            else
            {
                // Saves to the public repository (can't access internal)
                string filePath = Android.OS.Environment
                    .GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim)
                    .AbsolutePath;

                this.publicFile = new File(filePath, this.fileName);
            }
        }
    }
}

#endif