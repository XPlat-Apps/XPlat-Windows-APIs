namespace XamKit.Core.Common.Storage
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines an interface for an application folder.
    /// </summary>
    public interface IAppFolder : IAppFileStoreItem
    {
        /// <summary>
        /// Creates a new file with the specified name in the current folder.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="creationOption">
        /// An optional option which determines how to create the file if the specified name already exists in the current folder.
        /// </param>
        /// <returns>
        /// Returns an IAppFile representing the created file.
        /// </returns>
        Task<IAppFile> CreateFileAsync(
            string fileName,
            FileStoreCreationOption creationOption = FileStoreCreationOption.ThrowExceptionIfExists);

        /// <summary>
        /// Creates a new subfolder with the specified name in the current folder.
        /// </summary>
        /// <param name="folderName">
        /// The folder name.
        /// </param>
        /// <param name="creationOption">
        /// An optional option which determines how to create the folder if the specified name already exists in the current folder.
        /// </param>
        /// <returns>
        /// Returns an IAppFolder representing the created folder.
        /// </returns>
        Task<IAppFolder> CreateFolderAsync(
            string folderName,
            FileStoreCreationOption creationOption = FileStoreCreationOption.ThrowExceptionIfExists);

        /// <summary>
        /// Gets the file with the specified name from the current folder.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="createIfNotExisting">
        /// An optional value indicating whether to create the file if it does not exist.
        /// </param>
        /// <returns>
        /// Returns an IAppFile representing the file.
        /// </returns>
        Task<IAppFile> GetFileAsync(string fileName, bool createIfNotExisting = false);

        /// <summary>
        /// Gets the files from the current folder.
        /// </summary>
        /// <returns>
        /// Returns a collection of IAppFiles.
        /// </returns>
        Task<IEnumerable<IAppFile>> GetFilesAsync();
 
        /// <summary>
        /// Gets the folder with the specified name from the current folder.
        /// </summary>
        /// <param name="folderName">
        /// The folder name.
        /// </param>
        /// <param name="createIfNotExisting">
        /// An optional value indicating whether to create the file if it does not exist.
        /// </param>
        /// <returns>
        /// Returns an IAppFolder representing the folder.
        /// </returns>
        Task<IAppFolder> GetFolderAsync(string folderName, bool createIfNotExisting = false);

        /// <summary>
        /// Gets the folders from the current folder.
        /// </summary>
        /// <returns>
        /// Returns a collection of IAppFolders.
        /// </returns>
        Task<IEnumerable<IAppFolder>> GetFoldersAsync();
    }
}