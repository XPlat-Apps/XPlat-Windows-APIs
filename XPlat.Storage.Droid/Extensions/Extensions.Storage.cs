namespace XPlat.Storage
{
    using System;
    using System.Threading.Tasks;

    using Android.Graphics;

    using XPlat.Droid;

    public static partial class Extensions
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
        /// IStorageFile imageFile = await this.bitmap.SaveAsFileAsync(ApplicationData.Current.LocalFolder, "image.jpg", Bitmap.CompressFormat.Jpeg);
        /// </example>
        public static async Task<IStorageFile> SaveAsFileAsync(
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

                using (var imageStream = await image.GetStreamAsync(compressionFormat))
                {
                    using (var fileStream = await result.OpenAsync(FileAccessMode.ReadWrite))
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

        public static FileAttributes AsFileAttributes(this System.IO.FileAttributes attributes)
        {
            var result = FileAttributes.Normal;

            if (attributes.HasFlag(System.IO.FileAttributes.ReadOnly))
            {
                result |= FileAttributes.ReadOnly;
            }

            if (attributes.HasFlag(System.IO.FileAttributes.Directory))
            {
                result |= FileAttributes.Directory;
            }

            if (attributes.HasFlag(System.IO.FileAttributes.Archive))
            {
                result |= FileAttributes.Archive;
            }

            if (attributes.HasFlag(System.IO.FileAttributes.Temporary))
            {
                result |= FileAttributes.Temporary;
            }

            return result;
        }

        public static Task ClearAsync(this IStorageFolder folder)
        {
            return Task.Run(
                async () =>
                    {
                        foreach (var subfolder in await folder.GetFoldersAsync())
                        {
                            await subfolder.DeleteAsync();
                        }

                        foreach (var file in await folder.GetFilesAsync())
                        {
                            await file.DeleteAsync();
                        }
                    });
        }
    }
}