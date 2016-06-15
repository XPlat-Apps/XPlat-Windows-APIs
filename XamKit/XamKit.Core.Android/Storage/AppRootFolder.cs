namespace XamKit.Core.Storage
{
    using System;
    using System.Threading.Tasks;

    using XamKit.Core.Common.Storage;

    /// <summary>
    /// Defines the app's root folder for an Android application.
    /// </summary>
    public class AppRootFolder : IAppFileStore
    {
        /// <summary>
        /// Gets the path to the file store.
        /// </summary>
        public string Path { get; }

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
        public Task<object> CreateFileAsync(
            string fileName,
            FileStoreCreationOption creationOption = FileStoreCreationOption.ThrowExceptionIfExists)
        {
            throw new NotImplementedException();
        }
    }
}