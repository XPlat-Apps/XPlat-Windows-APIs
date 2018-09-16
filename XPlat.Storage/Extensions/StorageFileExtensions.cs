namespace XPlat.Storage.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public static class StorageFileExtensions
    {
#if __ANDROID__
        private static readonly List<string> exifKeys = new List<string>
        {
            Android.Media.ExifInterface.TagAperture,
            Android.Media.ExifInterface.TagDatetime,
            Android.Media.ExifInterface.TagDatetimeDigitized,
            Android.Media.ExifInterface.TagExposureTime,
            Android.Media.ExifInterface.TagFlash,
            Android.Media.ExifInterface.TagFocalLength,
            Android.Media.ExifInterface.TagGpsAltitude,
            Android.Media.ExifInterface.TagGpsAltitudeRef,
            Android.Media.ExifInterface.TagGpsDatestamp,
            Android.Media.ExifInterface.TagGpsLatitude,
            Android.Media.ExifInterface.TagGpsLatitudeRef,
            Android.Media.ExifInterface.TagGpsLongitude,
            Android.Media.ExifInterface.TagGpsLongitudeRef,
            Android.Media.ExifInterface.TagGpsProcessingMethod,
            Android.Media.ExifInterface.TagGpsTimestamp,
            Android.Media.ExifInterface.TagIso,
            Android.Media.ExifInterface.TagMake,
            Android.Media.ExifInterface.TagModel,
            Android.Media.ExifInterface.TagOrientation,
            Android.Media.ExifInterface.TagSubsecTime,
            Android.Media.ExifInterface.TagSubsecTimeDig,
            Android.Media.ExifInterface.TagSubsecTimeOrig,
            Android.Media.ExifInterface.TagWhiteBalance
        };

        public static IStorageFile GetFileFromPath(string path)
        {
            Task<IStorageFile> storageFileTask = StorageFile.GetFileFromPathAsync(path);
            storageFileTask.Wait();
            return storageFileTask.Result;
        }

        public static IReadOnlyDictionary<string, string> GetExifData(this IStorageFile imageFile)
        {
            if (imageFile == null || !imageFile.Exists)
            {
                return null;
            }

            Dictionary<string, string> exif = new Dictionary<string, string>();

            Android.Media.ExifInterface exifInterface = new Android.Media.ExifInterface(imageFile.Path);

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
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
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

            Android.Media.ExifInterface exifInterface = new Android.Media.ExifInterface(imageFile.Path);
            foreach (KeyValuePair<string, string> exifKvp in exifData)
            {
                try
                {
                    exifInterface.SetAttribute(exifKvp.Key, exifKvp.Value);
                    exifInterface.SaveAttributes();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }
        }
#endif
    }
}