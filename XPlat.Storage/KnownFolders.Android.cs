#if __ANDROID__
namespace XPlat.Storage
{
    using System.Threading.Tasks;

    using Android.OS;

    /// <summary>Provides access to common locations that contain user content.</summary>
    public static class KnownFolders
    {
        /// <summary>Gets the Camera Roll folder.</summary>
        public static IStorageFolder CameraRoll
        {
            get
            {
                Task<IStorageFolder> cameraRoll = StorageFolder.GetFolderFromPathAsync(
                    Environment.GetExternalStoragePublicDirectory(Environment.DirectoryDcim).AbsolutePath);
                cameraRoll.Wait();
                return cameraRoll.Result;
            }
        }

        /// <summary>Gets the Music library.</summary>
        public static IStorageFolder MusicLibrary
        {
            get
            {
                Task<IStorageFolder> musicLibrary = StorageFolder.GetFolderFromPathAsync(
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic));
                musicLibrary.Wait();
                return musicLibrary.Result;
            }
        }

        /// <summary>Gets the Pictures library.</summary>
        public static IStorageFolder PicturesLibrary
        {
            get
            {
                Task<IStorageFolder> picturesLibrary = StorageFolder.GetFolderFromPathAsync(
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures));
                picturesLibrary.Wait();
                return picturesLibrary.Result;
            }
        }

        /// <summary>Gets the Videos library.</summary>
        public static IStorageFolder VideosLibrary
        {
            get
            {
                Task<IStorageFolder> videosLibrary = StorageFolder.GetFolderFromPathAsync(
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyVideos));
                videosLibrary.Wait();
                return videosLibrary.Result;
            }
        }

        /// <summary>Gets the Documents library. The Documents library is not intended for general use.</summary>
        public static IStorageFolder DocumentsLibrary
        {
            get
            {
                Task<IStorageFolder> documentsLibrary = StorageFolder.GetFolderFromPathAsync(
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments));
                documentsLibrary.Wait();
                return documentsLibrary.Result;
            }
        }
    }
}
#endif