namespace XamarinApiToolkit.Storage
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines an interface for an application folder.
    /// </summary>
    public interface IAppFolder : IAppStorageItem
    {
        /// <summary>
        /// Creates a new file with the desired name in this folder.
        /// </summary>
        /// <param name="desiredName">
        /// The desired file name.
        /// </param>
        /// <returns>
        /// Returns the created file.
        /// </returns>
        Task<IAppFile> CreateFileAsync(string desiredName);

        /// <summary>
        /// Creates a new file with the desired name with creation options in this folder.
        /// </summary>
        /// <param name="desiredName">
        /// The desired file name.
        /// </param>
        /// <param name="options">
        /// The creation options.
        /// </param>
        /// <returns>
        /// Returns the created file.
        /// </returns>
        Task<IAppFile> CreateFileAsync(string desiredName, FileStoreCreationOption options);

        /// <summary>
        /// Creates a new folder with the desired name in this folder.
        /// </summary>
        /// <param name="desiredName">
        /// The desired folder name.
        /// </param>
        /// <returns>
        /// Returns the created folder.
        /// </returns>
        Task<IAppFolder> CreateFolderAsync(string desiredName);

        /// <summary>
        /// Creates a new folder with the desired name with creation options in this folder.
        /// </summary>
        /// <param name="desiredName">
        /// The desired folder name.
        /// </param>
        /// <param name="options">
        /// The creation options.
        /// </param>
        /// <returns>
        /// Returns the created folder.
        /// </returns>
        Task<IAppFolder> CreateFolderAsync(string desiredName, FileStoreCreationOption options);

        /// <summary>
        /// Gets a file with the specified name from this folder.
        /// </summary>
        /// <param name="name">
        /// The name of the file to get.
        /// </param>
        /// <returns>
        /// Returns the file.
        /// </returns>
        Task<IAppFile> GetFileAsync(string name);

        /// <summary>
        /// Gets a file with the specified name from this folder.
        /// </summary>
        /// <param name="name">
        /// The name of the file to get.
        /// </param>
        /// <param name="createIfNotExists">
        /// A value indicating whether to create the file if it does not exist.
        /// </param>
        /// <returns>
        /// Returns the file.
        /// </returns>
        Task<IAppFile> GetFileAsync(string name, bool createIfNotExists);

        /// <summary>
        /// Gets a folder with the specified name from this folder.
        /// </summary>
        /// <param name="name">
        /// The name of the folder to get.
        /// </param>
        /// <returns>
        /// Returns the folder.
        /// </returns>
        Task<IAppFolder> GetFolderAsync(string name);

        /// <summary>
        /// Gets a folder with the specified name from this folder.
        /// </summary>
        /// <param name="name">
        /// The name of the folder to get.
        /// </param>
        /// <param name="createIfNotExists">
        /// A value indicating whether to create the folder if it does not exist.
        /// </param>
        /// <returns>
        /// Returns the folder.
        /// </returns>
        Task<IAppFolder> GetFolderAsync(string name, bool createIfNotExists);

        /// <summary>
        /// Gets a storage item with the specified name from this folder.
        /// </summary>
        /// <remarks>
        /// A storage item can be a file or folder.
        /// </remarks>
        /// <param name="name">
        /// The name of the item to get.
        /// </param>
        /// <returns>
        /// Returns the file or folder.
        /// </returns>
        Task<IAppStorageItem> GetItemAsync(string name);

        /// <summary>
        /// Gets all of the files from this folder.
        /// </summary>
        /// <returns>
        /// Returns a collection of files.
        /// </returns>
        Task<IReadOnlyList<IAppFile>> GetFilesAsync();

        /// <summary>
        /// Gets all of the folders from this folder.
        /// </summary>
        /// <returns>
        /// Returns a collection of folders.
        /// </returns>
        Task<IReadOnlyList<IAppFolder>> GetFoldersAsync();

        /// <summary>
        /// Gets all of the items from this folder.
        /// </summary>
        /// <returns>
        /// Returns a collection of folders and files.
        /// </returns>
        Task<IReadOnlyList<IAppStorageItem>> GetItemsAsync();
    }
}