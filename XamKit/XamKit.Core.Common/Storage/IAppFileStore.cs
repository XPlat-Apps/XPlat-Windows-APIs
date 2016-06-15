namespace XamKit.Core.Common.Storage
{
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the interface for a file store.
    /// </summary>
    public interface IAppFileStore
    {
        /// <summary>
        /// Gets the path to the file store.
        /// </summary>
        string Path { get; }

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
        Task<IAppFile> CreateFileAsync(
            string fileName,
            FileStoreCreationOption creationOption = FileStoreCreationOption.ThrowExceptionIfExists);
    }
}