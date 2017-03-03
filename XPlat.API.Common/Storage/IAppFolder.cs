namespace XPlat.API.Storage
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines an interface for an application folder.
    /// </summary>
    public interface IAppFolder : IAppStorageItem
    {
        /// <summary>
        /// Creates a new file in the current folder.
        /// </summary>
        /// <param name="desiredName">
        /// The desired name of the file to create.
        /// </param>
        /// <returns>
        /// When this method completes, it returns the new file as an IAppFile.
        /// </returns>
        Task<IAppFile> CreateFileAsync(string desiredName);

        /// <summary>
        /// Creates a new file in the current folder, and specifies what to do if a file with the same name already exists in the current folder.
        /// </summary>
        /// <param name="desiredName">
        /// The desired name of the file to create. If there is an existing file in the current folder that already has the specified desiredName, the specified FileStoreCreationOption determines how the API responds to the conflict.
        /// </param>
        /// <param name="options">
        /// The enum value that determines how the API responds if the desiredName is the same as the name of an existing file in the current folder.
        /// </param>
        /// <returns>
        /// When this method completes, it returns the new file as an IAppFile.
        /// </returns>
        Task<IAppFile> CreateFileAsync(string desiredName, FileStoreCreationOption options);

        /// <summary>
        /// Creates a new folder in the current folder.
        /// </summary>
        /// <param name="desiredName">
        /// The desired name of the folder to create.
        /// </param>
        /// <returns>
        /// When this method completes, it returns the new folder as an IAppFolder.
        /// </returns>
        Task<IAppFolder> CreateFolderAsync(string desiredName);

        /// <summary>
        /// Creates a new folder in the current folder, and specifies what to do if a folder with the same name already exists in the current folder.
        /// </summary>
        /// <param name="desiredName">
        /// The desired name of the folder to create. If there is an existing folder in the current folder that already has the specified desiredName, the specified FileStoreCreationOption determines how the API responds to the conflict.
        /// </param>
        /// <param name="options">
        /// The enum value that determines how Windows responds if the desiredName is the same as the name of an existing folder in the current folder.
        /// </param>
        /// <returns>
        /// When this method completes, it returns the new folder as an IAppFolder.
        /// </returns>
        Task<IAppFolder> CreateFolderAsync(string desiredName, FileStoreCreationOption options);

        /// <summary>
        /// Gets the specified file from the current folder.
        /// </summary>
        /// <param name="name">
        /// The name of the file to retrieve.
        /// </param>
        /// <returns>
        /// When this method completes successfully, it returns an IAppFile that represents the file.
        /// </returns>
        Task<IAppFile> GetFileAsync(string name);

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
        /// When this method completes successfully, it returns an IAppFile that represents the file.
        /// </returns>
        Task<IAppFile> GetFileAsync(string name, bool createIfNotExists);

        /// <summary>
        /// Gets the specified folder from the current folder.
        /// </summary>
        /// <param name="name">
        /// The name of the child folder to retrieve.
        /// </param>
        /// <returns>
        /// When this method completes successfully, it returns an IAppFolder that represents the child folder.
        /// </returns>
        Task<IAppFolder> GetFolderAsync(string name);

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
        /// When this method completes successfully, it returns an IAppFolder that represents the child folder.
        /// </returns>
        Task<IAppFolder> GetFolderAsync(string name, bool createIfNotExists);

        /// <summary>
        /// Gets the specified item from the current folder.
        /// </summary>
        /// <param name="name">
        /// The name of the item to retrieve.
        /// </param>
        /// <returns>
        /// When this method completes successfully, it returns the file or folder (type IAppStorageItem).
        /// </returns>
        Task<IAppStorageItem> GetItemAsync(string name);

        /// <summary>
        /// Gets the files from the current folder.
        /// </summary>
        /// <returns>
        /// When this method completes successfully, it returns a list of the files in the folder. Each file in the list is represented by an IAppFile.
        /// </returns>
        Task<IReadOnlyList<IAppFile>> GetFilesAsync();

        /// <summary>
        /// Gets the folders in the current folder.
        /// </summary>
        /// <returns>
        /// When this method completes successfully, it returns a list of the child folders in the folder. Each folder in the list is represented by an IAppFolder.
        /// </returns>
        Task<IReadOnlyList<IAppFolder>> GetFoldersAsync();

        /// <summary>
        /// Gets the items from the current folder.
        /// </summary>
        /// <returns>
        /// When this method completes successfully, it returns a list of the files and folders. The files and folders in the list are represented by objects of type IAppStorageItem.
        /// </returns>
        Task<IReadOnlyList<IAppStorageItem>> GetItemsAsync();
    }
}