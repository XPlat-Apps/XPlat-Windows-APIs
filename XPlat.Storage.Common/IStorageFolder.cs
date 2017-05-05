namespace XPlat.Storage
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines an interface for an application folder.
    /// </summary>
    public interface IStorageFolder : IStorageItem
    {
        /// <summary>
        /// Creates a new file in the current folder.
        /// </summary>
        /// <param name="desiredName">
        /// The desired name of the file to create.
        /// </param>
        /// <returns>
        /// When this method completes, it returns the new file as an IStorageFile.
        /// </returns>
        Task<IStorageFile> CreateFileAsync(string desiredName);

        /// <summary>
        /// Creates a new file in the current folder, and specifies what to do if a file with the same name already exists in the current folder.
        /// </summary>
        /// <param name="desiredName">
        /// The desired name of the file to create. If there is an existing file in the current folder that already has the specified desiredName, the specified CreationCollisionOption determines how the API responds to the conflict.
        /// </param>
        /// <param name="options">
        /// The enum value that determines how the API responds if the desiredName is the same as the name of an existing file in the current folder.
        /// </param>
        /// <returns>
        /// When this method completes, it returns the new file as an IStorageFile.
        /// </returns>
        Task<IStorageFile> CreateFileAsync(string desiredName, CreationCollisionOption options);

        /// <summary>
        /// Creates a new folder in the current folder.
        /// </summary>
        /// <param name="desiredName">
        /// The desired name of the folder to create.
        /// </param>
        /// <returns>
        /// When this method completes, it returns the new folder as an IStorageFolder.
        /// </returns>
        Task<IStorageFolder> CreateFolderAsync(string desiredName);

        /// <summary>
        /// Creates a new folder in the current folder, and specifies what to do if a folder with the same name already exists in the current folder.
        /// </summary>
        /// <param name="desiredName">
        /// The desired name of the folder to create. If there is an existing folder in the current folder that already has the specified desiredName, the specified CreationCollisionOption determines how the API responds to the conflict.
        /// </param>
        /// <param name="options">
        /// The enum value that determines how Windows responds if the desiredName is the same as the name of an existing folder in the current folder.
        /// </param>
        /// <returns>
        /// When this method completes, it returns the new folder as an IStorageFolder.
        /// </returns>
        Task<IStorageFolder> CreateFolderAsync(string desiredName, CreationCollisionOption options);

        /// <summary>
        /// Gets the specified file from the current folder.
        /// </summary>
        /// <param name="name">
        /// The name of the file to retrieve.
        /// </param>
        /// <returns>
        /// When this method completes successfully, it returns an IStorageFile that represents the file.
        /// </returns>
        Task<IStorageFile> GetFileAsync(string name);

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
        /// The name of the child folder to retrieve.
        /// </param>
        /// <returns>
        /// When this method completes successfully, it returns an IStorageFolder that represents the child folder.
        /// </returns>
        Task<IStorageFolder> GetFolderAsync(string name);

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

        /// <summary>
        /// Gets the specified item from the current folder.
        /// </summary>
        /// <param name="name">
        /// The name of the item to retrieve.
        /// </param>
        /// <returns>
        /// When this method completes successfully, it returns the file or folder (type IStorageItem).
        /// </returns>
        Task<IStorageItem> GetItemAsync(string name);

        /// <summary>
        /// Gets the files from the current folder.
        /// </summary>
        /// <returns>
        /// When this method completes successfully, it returns a list of the files in the folder. Each file in the list is represented by an IStorageFile.
        /// </returns>
        Task<IReadOnlyList<IStorageFile>> GetFilesAsync();

        /// <summary>
        /// Gets the folders in the current folder.
        /// </summary>
        /// <returns>
        /// When this method completes successfully, it returns a list of the child folders in the folder. Each folder in the list is represented by an IStorageFolder.
        /// </returns>
        Task<IReadOnlyList<IStorageFolder>> GetFoldersAsync();

        /// <summary>
        /// Gets the items from the current folder.
        /// </summary>
        /// <returns>
        /// When this method completes successfully, it returns a list of the files and folders. The files and folders in the list are represented by objects of type IStorageItem.
        /// </returns>
        Task<IReadOnlyList<IStorageItem>> GetItemsAsync();

        /// <summary>
        /// Gets an index-based range of files and folders from the list of all files and subfolders in the current folder.
        /// </summary>
        /// <returns>
        /// When this method completes successfully, it returns a list of the files and subfolders in the current folder. The list is of type IReadOnlyList IStorageItem. Each item in the list is represented by an IStorageItem object. To work with the returned items, call the IsOfType method of the IStorageItem interface to determine whether each item is a file or a folder. Then cast the item to a StorageFolder or StorageFile.
        /// </returns>
        /// <param name="startIndex">
        ///     The zero-based index of the first item in the range to get.
        /// </param>
        /// <param name="maxItemsToRetrieve">
        ///     The maximum number of items to get.
        /// </param>
        Task<IReadOnlyList<IStorageItem>> GetItemsAsync(int startIndex, int maxItemsToRetrieve);
    }
}