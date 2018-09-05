namespace XPlat.Storage
{
    using System.Threading.Tasks;

    /// <summary>Provides additional extra methods for getting items from within storage folders.</summary>
    public interface IStorageFolderExtras
    {
        /// <summary>
        /// Gets the specified file from the current folder.
        /// </summary>
        /// <param name="name">
        /// The name of the file to retrieve. If the file does not exist in the current folder, the createIfNotExists flag will allow the API to create the file if set to true.
        /// </param>
        /// <param name="createIfNotExists">
        /// A value indicating whether to create the file if it does not exist.
        /// </param>
        /// <returns>
        /// When this method completes successfully, it returns an IStorageFile that represents the file.
        /// </returns>
        Task<IStorageFile> GetFileAsync(string name, bool createIfNotExists);

        /// <summary>
        /// Gets the specified folder from the current folder.
        /// </summary>
        /// <param name="name">
        /// The name of the child folder to retrieve. If the child folder does not exist in the current folder, the createIfNotExists flag will allow the API to create the folder if set to true.
        /// </param>
        /// <param name="createIfNotExists">
        /// A value indicating whether to create the child folder if it does not exist.
        /// </param>
        /// <returns>
        /// When this method completes successfully, it returns an IStorageFolder that represents the child folder.
        /// </returns>
        Task<IStorageFolder> GetFolderAsync(string name, bool createIfNotExists);
    }
}
