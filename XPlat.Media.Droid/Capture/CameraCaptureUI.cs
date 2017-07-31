namespace XPlat.Media.Capture
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Android.App;
    using Android.Content;
    using Android.Graphics;
    using Android.Media;
    using Android.OS;
    using Android.Provider;

    using XPlat.Foundation;
    using XPlat.Storage;

    /// <summary>
    /// Provides a full window UI for capturing audio, video, and photos from a camera.
    /// </summary>
    public class CameraCaptureUI : ICameraCaptureUI
    {
        private readonly Context context;

        private int requestId;

        private TaskCompletionSource<IStorageFile> currentSingleTcs;

        /// <summary>
        /// Initializes a new instance of the <see cref="CameraCaptureUI"/> class.
        /// </summary>
        /// <param name="context">
        /// The application context.
        /// </param>
        public CameraCaptureUI(Context context)
        {
            this.context = context;
            this.PhotoSettings = new CameraCaptureUIPhotoCaptureSettings();
            this.VideoSettings = new CameraCaptureUIVideoCaptureSettings();
        }

        /// <inheritdoc />
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
            handler = async (sender, args) =>
                {
                    var tcs = Interlocked.Exchange(ref this.currentSingleTcs, null);

                    CameraCaptureUIActivity.CameraFileCaptured -= handler;

                    if (args.RequestId != id)
                    {
                        return;
                    }
                    
                    var resultFile = args.File;

                    if (args.File != null && !args.File.Exists)
                    {
                        resultFile = null;
                    }

                    if ((mode == CameraCaptureUIMode.Photo || mode == CameraCaptureUIMode.PhotoOrVideo)
                        && resultFile != null && resultFile.Exists)
                    {
                        var exifData = resultFile.GetExifData();

                        resultFile = await resultFile.ResizeImageFileAsync(this.PhotoSettings.MaxResolution);

                        resultFile.SetExifData(exifData);
                    }

                    tcs.SetResult(args.Cancel ? null : resultFile);
                };

            CameraCaptureUIActivity.CameraFileCaptured += handler;

            return newTcs.Task;
        }

        /// <inheritdoc />
        public CameraCaptureUIPhotoCaptureSettings PhotoSettings { get; }

        /// <inheritdoc />
        public CameraCaptureUIVideoCaptureSettings VideoSettings { get; }

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
                    cameraCaptureIntent.PutExtra(MediaStore.ExtraVideoQuality, this.GetVideoQuality());
                    if (this.VideoSettings.MaxDurationInSeconds > 0)
                    {
                        cameraCaptureIntent.PutExtra(
                            MediaStore.ExtraDurationLimit,
                            this.VideoSettings.MaxDurationInSeconds);
                    }
                    cameraCaptureIntent.PutExtra(CameraCaptureUIActivity.IntentFileName, $"{Guid.NewGuid()}.mp4");
                    break;
            }

            return cameraCaptureIntent;
        }

        private int GetVideoQuality()
        {
            switch (this.VideoSettings.MaxResolution)
            {
                case CameraCaptureUIMaxVideoResolution.StandardDefinition:
                case CameraCaptureUIMaxVideoResolution.LowDefinition: return 0;
                default: return 1;
            }
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