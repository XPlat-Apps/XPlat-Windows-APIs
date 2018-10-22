#if __ANDROID__
namespace XPlat.Media.Capture
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Android.App;
    using Android.Content;
    using Android.Provider;
    using XPlat.Exceptions;
    using XPlat.Foundation;
    using XPlat.Helpers;
    using XPlat.Media.Capture.Extensions;
    using XPlat.Storage;
    using XPlat.Storage.Extensions;

    /// <summary>Provides a full window UI for capturing video and photos from a camera.</summary>
    public class CameraCaptureUI : ICameraCaptureUI
    {
        private readonly Context context;

        private int requestId;

        private TaskCompletionSource<IStorageFile> currentSingleTcs;

        private int requestCode;

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

        /// <summary>Provides settings for capturing photos.</summary>
        public CameraCaptureUIPhotoCaptureSettings PhotoSettings { get; }

        /// <summary>Provides settings for capturing videos. The settings include maximum resolution, maximum duration, and whether or not to allow trimming.</summary>
        public CameraCaptureUIVideoCaptureSettings VideoSettings { get; }

        /// <summary>Launches the CameraCaptureUI user interface.</summary>
        /// <returns>When this operation completes, an IStorageFile object is returned.</returns>
        /// <param name="mode">Specifies whether the user interface that will be shown allows the user to capture a photo, capture a video, or capture both photos and videos.</param>
        public Task<IStorageFile> CaptureFileAsync(CameraCaptureUIMode mode)
        {
            int newRequestCode = RequestCodeHelper.GenerateRequestCode();

            TaskCompletionSource<IStorageFile> newTcs = new TaskCompletionSource<IStorageFile>(newRequestCode);
            if (Interlocked.CompareExchange(ref this.currentSingleTcs, newTcs, null) != null)
            {
                throw new InvalidOperationException("Cannot activate multiple requests for files.");
            }

            this.requestCode = newRequestCode;

            this.context.StartActivity(this.GenerateIntent(this.requestCode, mode));

            TypedEventHandler<Activity, CameraFileCaptured> handler = null;
            handler = async (sender, args) =>
            {
                TaskCompletionSource<IStorageFile> tcs = Interlocked.Exchange(ref this.currentSingleTcs, null);

                CameraCaptureUIActivity.CameraFileCaptured -= handler;

                if (args.RequestId != this.requestCode)
                {
                    return;
                }

                if (args.PermissionDenied)
                {
                    tcs.SetException(new AppPermissionInvalidException("android.permission.CAMERA",
                        "Camera permission is required in order to use camera function"));
                }
                else
                {
                    IStorageFile resultFile = args.File;

                    if (args.File != null && !args.File.Exists)
                    {
                        resultFile = null;
                    }

                    if ((mode == CameraCaptureUIMode.Photo || mode == CameraCaptureUIMode.PhotoOrVideo)
                        && resultFile != null && resultFile.Exists)
                    {
                        IReadOnlyDictionary<string, string> exifData = resultFile.GetExifData();
                        try
                        {
                            resultFile = await resultFile.ResizeImageFileAsync(this.PhotoSettings.MaxResolution);
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.ToString());
                        }

                        resultFile?.SetExifData(exifData);
                    }

                    tcs.SetResult(args.Cancel ? null : resultFile);
                }
            };

            CameraCaptureUIActivity.CameraFileCaptured += handler;

            return newTcs.Task;
        }

        private Intent GenerateIntent(int id, CameraCaptureUIMode mode)
        {
            Intent cameraCaptureIntent = new Intent(this.context, typeof(CameraCaptureUIActivity));
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
    }
}
#endif