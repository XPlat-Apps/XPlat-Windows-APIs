namespace XPlat.Droid
{
    using System.IO;
    using System.Threading.Tasks;

    using Android.Graphics;

    public static partial class Extensions
    {
        public static async Task<Stream> GetStreamAsync(this Bitmap image, Bitmap.CompressFormat compressionFormat, int quality = 100)
        {
            using (image)
            {
                var stream = new MemoryStream();
                var result = await image.CompressAsync(compressionFormat, quality, stream);

                image.Recycle();

                if (result)
                {
                    stream.Position = 0;
                    return stream;
                }
            }
            return null;
        }
    }
}