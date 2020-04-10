namespace XPlat.Media.Capture.Extensions
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using XPlat.Storage;

    public static class BitmapExtensions
    {
#if __ANDROID__
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
            this Android.Graphics.Bitmap image,
            IStorageFolder saveLocation,
            string fileName,
            Android.Graphics.Bitmap.CompressFormat compressionFormat)
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
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return result;
        }

        public static async Task<Stream> GetStreamAsync(this Android.Graphics.Bitmap image,
            Android.Graphics.Bitmap.CompressFormat compressionFormat, int quality = 100)
        {
            using (image)
            {
                MemoryStream stream = new MemoryStream();
                bool result = await image.CompressAsync(compressionFormat, quality, stream);

                image.Recycle();

                if (!result)
                {
                    return null;
                }

                stream.Position = 0;
                return stream;
            }

            return null;
        }

#endif
    }
}