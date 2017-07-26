namespace XPlat.Media.Capture
{
    using System;
    using System.Linq;

    using Android.App;
    using Android.Content;
    using Android.Database;
    using Android.OS;

    using Java.IO;

    using XPlat.Storage;
    using XPlat.Storage.Helpers;

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
                var file = StorageHelper.CreateStorageFile(
                    ApplicationData.Current.TemporaryFolder,
                    Guid.NewGuid().ToString(),
                    CreationCollisionOption.ReplaceExisting);

                CurrentFilePath = file.Path;

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
            var file = new File(CurrentFilePath);
            if (file.Exists())
            {
                return ParcelFileDescriptor.Open(file, ParcelFileMode.ReadWrite);
            }

            throw new FileNotFoundException(uri.Path);
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