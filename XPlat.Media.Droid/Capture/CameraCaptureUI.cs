namespace XPlat.Media.Capture
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Android.App;
    using Android.Content;
    using Android.Graphics;
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
            this.PhotoSettings = new CameraCaptureUIPhotoCaptureSettings();
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
            handler = async (sender, args) =>
                {
                    var tcs = Interlocked.Exchange(ref this.currentSingleTcs, null);

                    CameraCaptureUIActivity.CameraFileCaptured -= handler;

                    if (args.RequestId != id)
                    {
                        return;
                    }

                    var resolution = this.PhotoSettings.MaxResolution;

                    var resultFile = args.File;

                    if (args.File != null && !args.File.Exists)
                    {
                        resultFile = null;
                    }

                    if ((mode == CameraCaptureUIMode.Photo || mode == CameraCaptureUIMode.PhotoOrVideo) && resultFile != null && resultFile.Exists)
                    {
                        if (resolution != CameraCaptureUIMaxPhotoResolution.HighestAvailable)
                        {
                            // Scale the image to the expected resolution if higher than the selected max resolution.

                            var bitmap = BitmapFactory.DecodeFile(resultFile.Path);
                            var width = bitmap.Width;
                            var height = bitmap.Height;

                            var isPortrait = width < height;

                            var expectedWidth = width;
                            var expectedHeight = height;

                            switch (resolution)
                            {
                                case CameraCaptureUIMaxPhotoResolution.VerySmallQvga:
                                    expectedWidth = isPortrait ? 240 : 320;
                                    expectedHeight = isPortrait ? 320 : 240;
                                    break;
                                case CameraCaptureUIMaxPhotoResolution.SmallVga:
                                    expectedWidth = isPortrait ? 480 : 640;
                                    expectedHeight = isPortrait ? 640 : 480;
                                    break;
                                case CameraCaptureUIMaxPhotoResolution.MediumXga:
                                    expectedWidth = isPortrait ? 768 : 1024;
                                    expectedHeight = isPortrait ? 1024 : 768;
                                    break;
                                case CameraCaptureUIMaxPhotoResolution.Large3M:
                                    expectedWidth = isPortrait ? 1080 : 1920;
                                    expectedHeight = isPortrait ? 1920 : 1080;
                                    break;
                                case CameraCaptureUIMaxPhotoResolution.VeryLarge5M:
                                    expectedWidth = isPortrait ? 1920 : 2560;
                                    expectedHeight = isPortrait ? 2560 : 1920;
                                    break;
                            }

                            var scale = Math.Max(width / expectedWidth, height / expectedHeight);

                            var scaleImage = Bitmap.CreateScaledBitmap(bitmap, width / scale, height / scale, true);

                            resultFile = await scaleImage.SaveAsFileAsync(
                                             ApplicationData.Current.TemporaryFolder,
                                             resultFile.Path,
                                             Bitmap.CompressFormat.Jpeg);

                            bitmap.Recycle();
                        }
                    }

                    tcs.SetResult(args.Cancel ? null : resultFile);
                };

            CameraCaptureUIActivity.CameraFileCaptured += handler;

            return newTcs.Task;
        }

        /// <inheritdoc />
        public CameraCaptureUIPhotoCaptureSettings PhotoSettings { get; }

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