namespace XPlat.Media.Capture
{
    using System;
    using System.Threading.Tasks;

    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.OS;
    using Android.Provider;

    using Java.IO;

    using XPlat.Foundation;
    using XPlat.Storage;

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

            var bundle = savedInstanceState ?? this.Intent.Extras;

            var isComplete = bundle.GetBoolean("isComplete", false);
            this.requestId = bundle.GetInt(IntentId, 0);
            this.action = bundle.GetString(IntentAction);
            this.fileName = bundle.GetString(IntentFileName, $"{Guid.NewGuid()}.jpg");

            // Saves to the public repository (can't access internal)
            var filePath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim).AbsolutePath;

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
                //var internalStorageFile = await ApplicationData.Current.TemporaryFolder.CreateFileAsync(this.fileName);
                var storageFile = await StorageFile.GetFileFromPathAsync(this.file.AbsolutePath);
                //await storageFile.MoveAndReplaceAsync(internalStorageFile);

                var args = new CameraFileCaptured(requestCode, storageFile, false);
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