namespace XPlat.Media.Capture
{
    using System;
    using System.IO;
    using System.Linq;

    using Android.App;
    using Android.Content;
    using Android.Database;
    using Android.OS;

    using XPlat.Storage;
    using XPlat.Storage.Helpers;

    using File = Java.IO.File;
    using Uri = Android.Net.Uri;

    public class CameraCaptureContentProvider : ContentProvider
    {
        public static Uri ContentUri => Uri.Parse($"content://{Application.Context.PackageName}/");

        public static string CurrentFilePath;

        public override int Delete(Uri uri, string selection, string[] selectionArgs)
        {
            return 0;
        }

        public override string GetType(Uri uri)
        {
            var path = uri.ToString();
            var pathParts = path.Split('.');
            var extension = pathParts.LastOrDefault();

            return MimeTypeHelper.GetMimeType(extension);
        }

        public override Uri Insert(Uri uri, ContentValues values)
        {
            return null;
        }

        public override bool OnCreate()
        {
            try
            {
                this.Context.ContentResolver.NotifyChange(ContentUri, null);
                return true;
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif
                return false;
            }
        }

        public override ParcelFileDescriptor OpenFile(Uri uri, string mode)
        {
            var storageFile = StorageHelper.CreateStorageFile(
                ApplicationData.Current.TemporaryFolder,
                nameof(CameraCaptureUI),
                CreationCollisionOption.ReplaceExisting);
            
            var file = new File(storageFile.Path);
            if (!file.Exists())
            {
                file.Mkdirs();
            }

            if (file.Exists())
            {
                CurrentFilePath = storageFile.Path;

                return ParcelFileDescriptor.Open(file, ParcelFileMode.ReadWrite);
            }

            throw new FileNotFoundException("Cannot find the file", storageFile.Path);
        }

        public override ICursor Query(
            Uri uri,
            string[] projection,
            string selection,
            string[] selectionArgs,
            string sortOrder)
        {
            return null;
        }

        public override int Update(Uri uri, ContentValues values, string selection, string[] selectionArgs)
        {
            return 0;
        }
    }
}