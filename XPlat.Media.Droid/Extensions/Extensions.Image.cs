namespace XPlat.Media
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Android.Graphics;
    using Android.Media;

    using XPlat.Media.Capture;
    using XPlat.Storage;

    using Stream = System.IO.Stream;

    public static class Extensions
    {
        /// <summary>
        /// Saves an Android Bitmap to an IStorageFile at the specified StorageFolder location by the specified file name.
        /// </summary>
        /// <param name="image">
        /// The image to save.
        /// </param>
        /// <param name="saveLocation">
        /// The location to save the file.
        /// </param>
        /// <param name="fileName">
        /// The file name (should include the extension).
        /// </param>
        /// <param name="compressionFormat">
        /// The compression format.
        /// </param>
        /// <returns>
        /// When this method completes, it returns the stored image file.
        /// </returns>
        /// <example>
        /// IStorageFile imageFile = await this.bitmap.SaveBitmapAsFileAsync(ApplicationData.Current.LocalFolder, "image.jpg", Bitmap.CompressFormat.Jpeg);
        /// </example>
        public static async Task<IStorageFile> SaveBitmapAsFileAsync(
            this Bitmap image,
            IStorageFolder saveLocation,
            string fileName,
            Bitmap.CompressFormat compressionFormat)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            if (saveLocation == null || !saveLocation.Exists)
            {
                throw new ArgumentNullException(nameof(saveLocation));
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(fileName);
            }

            IStorageFile result;

            try
            {
                result = await saveLocation.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);

                using (Stream imageStream = await image.GetStreamAsync(compressionFormat))
                {
                    using (Stream fileStream = await result.OpenAsync(FileAccessMode.ReadWrite))
                    {
                        await imageStream.CopyToAsync(fileStream);
                    }
                }
            }
            catch (Exception ex)
            {
                result = null;
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif
            }

            return result;
        }

        private static List<string> exifKeys = new List<string>
                                                   {
                                                       ExifInterface.TagAperture,
                                                       ExifInterface.TagDatetime,
                                                       ExifInterface.TagDatetimeDigitized,
                                                       ExifInterface.TagExposureTime,
                                                       ExifInterface.TagFlash,
                                                       ExifInterface.TagFocalLength,
                                                       ExifInterface.TagGpsAltitude,
                                                       ExifInterface.TagGpsAltitudeRef,
                                                       ExifInterface.TagGpsDatestamp,
                                                       ExifInterface.TagGpsLatitude,
                                                       ExifInterface.TagGpsLatitudeRef,
                                                       ExifInterface.TagGpsLongitude,
                                                       ExifInterface.TagGpsLongitudeRef,
                                                       ExifInterface.TagGpsProcessingMethod,
                                                       ExifInterface.TagGpsTimestamp,
                                                       ExifInterface.TagIso,
                                                       ExifInterface.TagMake,
                                                       ExifInterface.TagModel,
                                                       ExifInterface.TagOrientation,
                                                       ExifInterface.TagSubsecTime,
                                                       ExifInterface.TagSubsecTimeDig,
                                                       ExifInterface.TagSubsecTimeOrig,
                                                       ExifInterface.TagWhiteBalance
                                                   };

        public static IReadOnlyDictionary<string, string> GetExifData(this IStorageFile imageFile)
        {
            if (imageFile == null || !imageFile.Exists)
            {
                return null;
            }

            Dictionary<string, string> exif = new Dictionary<string, string>();

            ExifInterface exifInterface = new ExifInterface(imageFile.Path);

            foreach (string exifKey in exifKeys)
            {
                try
                {
                    string exifVal = exifInterface.GetAttribute(exifKey);
                    if (!string.IsNullOrWhiteSpace(exifVal))
                    {
                        exif.Add(exifKey, exifVal);
                    }
                }
                catch (Exception)
                {
                    // Ignored
                }
            }

            return exif;
        }

        public static void SetExifData(this IStorageFile imageFile, IReadOnlyDictionary<string, string> exifData)
        {
            if (imageFile == null || !imageFile.Exists)
            {
                return;
            }

            ExifInterface exifInterface = new ExifInterface(imageFile.Path);
            foreach (KeyValuePair<string, string> exifKvp in exifData)
            {
                try
                {
                    exifInterface.SetAttribute(exifKvp.Key, exifKvp.Value);
                    exifInterface.SaveAttributes();
                }
                catch (Exception)
                {
                    // Ignored
                }
            }
        }

        public static async Task<IStorageFile> ResizeImageFileAsync(this IStorageFile imageFile, CameraCaptureUIMaxPhotoResolution resolution)
        {
            if (imageFile == null)
            {
                return null;
            }

            if (resolution != CameraCaptureUIMaxPhotoResolution.HighestAvailable)
            {
                Bitmap bitmap = BitmapFactory.DecodeFile(imageFile.Path);
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

                Bitmap scaleImage = Bitmap.CreateScaledBitmap(
                    bitmap,
                    (int)(width / scale),
                    (int)(height / scale),
                    true);

                imageFile = await scaleImage.SaveBitmapAsFileAsync(
                                 ApplicationData.Current.TemporaryFolder,
                                 imageFile.Path,
                                 Bitmap.CompressFormat.Jpeg);

                bitmap.Recycle();
            }

            return imageFile;
        }
    }
}