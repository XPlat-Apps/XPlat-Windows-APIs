#if WINDOWS_UWP
namespace XPlat.Storage
{
    /// <summary>Provides access to common locations that contain user content.</summary>
    public static class KnownFolders
    {
        /// <summary>Gets the Camera Roll folder.</summary>
        public static IStorageFolder CameraRoll => new StorageFolder(Windows.Storage.KnownFolders.CameraRoll);

        /// <summary>Gets the play lists folder.</summary>
        public static IStorageFolder Playlists => new StorageFolder(Windows.Storage.KnownFolders.Playlists);

        /// <summary>Gets the Saved Pictures folder.</summary>
        public static IStorageFolder SavedPictures => new StorageFolder(Windows.Storage.KnownFolders.SavedPictures);

        /// <summary>Gets the Documents library. The Documents library is not intended for general use.</summary>
        public static IStorageFolder DocumentsLibrary => new StorageFolder(
            Windows.Storage.KnownFolders.DocumentsLibrary);

        /// <summary>Gets the HomeGroup folder.</summary>
        public static IStorageFolder HomeGroup => new StorageFolder(Windows.Storage.KnownFolders.HomeGroup);

        /// <summary>Gets the folder of media server (Digital Living Network Alliance (DLNA)) devices.</summary>
        public static IStorageFolder MediaServerDevices => new StorageFolder(
            Windows.Storage.KnownFolders.MediaServerDevices);

        /// <summary>Gets the Music library.</summary>
        public static IStorageFolder MusicLibrary => new StorageFolder(Windows.Storage.KnownFolders.MusicLibrary);

        /// <summary>Gets the Pictures library.</summary>
        public static IStorageFolder PicturesLibrary => new StorageFolder(Windows.Storage.KnownFolders.PicturesLibrary);

        /// <summary>Gets the removable devices folder.</summary>
        public static IStorageFolder RemovableDevices => new StorageFolder(
            Windows.Storage.KnownFolders.RemovableDevices);

        /// <summary>Gets the Videos library.</summary>
        public static IStorageFolder VideosLibrary => new StorageFolder(Windows.Storage.KnownFolders.VideosLibrary);

        /// <summary>Gets the Objects 3D folder.</summary>
        public static IStorageFolder Objects3D => new StorageFolder(Windows.Storage.KnownFolders.Objects3D);

        /// <summary>Gets the recorded calls folder.</summary>
        public static IStorageFolder RecordedCalls => new StorageFolder(Windows.Storage.KnownFolders.RecordedCalls);

        /// <summary>Gets the App Captures folder.</summary>
        public static IStorageFolder AppCaptures => new StorageFolder(Windows.Storage.KnownFolders.AppCaptures);
    }
}
#endif