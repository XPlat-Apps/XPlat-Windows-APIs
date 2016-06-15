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

        /// <summary>
        /// Creates a file within the file store with the specified file name.
        /// </summary>
        /// <param name="fileName">
        ///     The file name.
        /// </param>
        /// <param name="creationOption">
        ///     An optional creation option. Default will throw an exception if the file exists.
        /// </param>
        /// <returns>
        /// Returns the created file.
        /// </returns>
        public async Task<object> CreateFileAsync(
            string fileName,
            FileStoreCreationOption creationOption = FileStoreCreationOption.ThrowExceptionIfExists)
        {
            switch (creationOption)
            {
                case FileStoreCreationOption.GenerateUniqueIdentifier:
                    return await LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.GenerateUniqueName);
                case FileStoreCreationOption.ThrowExceptionIfExists:
                    return await LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.FailIfExists);
                case FileStoreCreationOption.ReplaceIfExists:
                    return await LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                default:
                    throw new ArgumentException("The specified creation option is not valid.");
            }
        }
    }
}