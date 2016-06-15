namespace XamKit.Core.Storage
{
    using System;
    using System.Threading.Tasks;

    using Windows.Storage;

    using XamKit.Core.Common.Storage;

    /// <summary>
    /// Defines the app's root folder for a UWP application.
    /// </summary>
    public class AppRootFolder : IAppFileStore
    {
        private static StorageFolder LocalFolder
        {
            get
            {
                return ApplicationData.Current.LocalFolder;
            }
        }

        /// <summary>
        /// Gets the path to the file store.
        /// </summary>
        public string Path
        {
            get
            {
                return LocalFolder.Path;
            }
        }

        public Task<IAppFile> CreateFileAsync(
            string fileName,
            FileStoreCreationOption creationOption = FileStoreCreationOption.ThrowExceptionIfExists)
        {
            throw new NotImplementedException();
        }
    }
}