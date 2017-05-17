namespace XPlat.Media.Capture
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Android.App;
    using Android.Content;
    using Android.Provider;

    using XPlat.Foundation;
    using XPlat.Storage;

    public class CameraCaptureUI : ICameraCaptureUI
    {
        private readonly Context context;

        private int requestId;

        private TaskCompletionSource<IStorageFile> currentSingleTcs;

        public CameraCaptureUI(Context context)
        {
            this.context = context;
        }

        public Task<IStorageFile> CaptureFileAsync(CameraCaptureUIMode mode)
        {
            var id = this.GenerateRequestId();

            var newTcs = new TaskCompletionSource<IStorageFile>(id);
            if (Interlocked.CompareExchange(ref this.currentSingleTcs, newTcs, null) != null)
            {
                throw new InvalidOperationException("Cannot activate multiple requests for files.");
            }

            this.context.StartActivity(this.GenerateIntent(id, mode));

            TypedEventHandler<Activity, CameraFileCaptured> handler = null;
            handler = (sender, args) =>
                {
                    var tcs = Interlocked.Exchange(ref this.currentSingleTcs, null);

                    CameraCaptureUIActivity.CameraFileCaptured -= handler;

                    if (args.RequestId != id)
                    {
                        return;
                    }

                    tcs.SetResult(args.Cancel ? null : args.File);
                };

            CameraCaptureUIActivity.CameraFileCaptured += handler;

            return newTcs.Task;
        }

        private Intent GenerateIntent(int id, CameraCaptureUIMode mode)
        {
            var cameraCaptureIntent = new Intent(this.context, typeof(CameraCaptureUIActivity));
            cameraCaptureIntent.PutExtra(CameraCaptureUIActivity.IntentId, id);

            switch (mode)
            {
                case CameraCaptureUIMode.PhotoOrVideo:
                case CameraCaptureUIMode.Photo:
                    cameraCaptureIntent.PutExtra(CameraCaptureUIActivity.IntentAction, MediaStore.ActionImageCapture);
                    cameraCaptureIntent.PutExtra(CameraCaptureUIActivity.IntentFileName, $"{Guid.NewGuid()}.jpg");
                    break;
                case CameraCaptureUIMode.Video:
                    cameraCaptureIntent.PutExtra(CameraCaptureUIActivity.IntentAction, MediaStore.ActionVideoCapture);
                    cameraCaptureIntent.PutExtra(CameraCaptureUIActivity.IntentFileName, $"{Guid.NewGuid()}.mp4");
                    break;
            }

            return cameraCaptureIntent;
        }

        private int GenerateRequestId()
        {
            // Gets a code that will be used for the intent.
            if (this.requestId == int.MaxValue)
            {
                this.requestId = 0;
            }

            return this.requestId++;
        }
    }
}