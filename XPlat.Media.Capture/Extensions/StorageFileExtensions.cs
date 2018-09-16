namespace XPlat.Media.Capture.Extensions
{
    using System;
    using System.Threading.Tasks;
    using XPlat.Storage;

    public static class StorageFileExtensions
    {
#if __ANDROID__
        public static async Task<IStorageFile> ResizeImageFileAsync(this IStorageFile imageFile, CameraCaptureUIMaxPhotoResolution resolution)
        {
            if (imageFile == null)
            {
                return null;
            }

            if (resolution != CameraCaptureUIMaxPhotoResolution.HighestAvailable)
            {
                Android.Graphics.Bitmap bitmap = Android.Graphics.BitmapFactory.DecodeFile(imageFile.Path);
                int width = bitmap.Width;
                int height = bitmap.Height;

                bool isPortrait = width < height;

                float expectedWidth = width;
                float expectedHeight = height;

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

                float scale = Math.Min(width / expectedWidth, height / expectedHeight);

                Android.Graphics.Bitmap scaleImage = Android.Graphics.Bitmap.CreateScaledBitmap(
                    bitmap,
                    (int)(width / scale),
                    (int)(height / scale),
                    true);

                imageFile = await scaleImage.SaveBitmapAsFileAsync(
                                 ApplicationData.Current.TemporaryFolder,
                                 imageFile.Path,
                                 Android.Graphics.Bitmap.CompressFormat.Jpeg);

                bitmap.Recycle();
            }

            return imageFile;
        }
#endif
    }
}
