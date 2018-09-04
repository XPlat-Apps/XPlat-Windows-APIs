namespace XPlat.Storage
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides methods to create search queries and retrieve files from a folder. This interface is implemented by StorageFolder objects, which can represent file system folders, libraries, or virtual folders that are automatically generated when queries are created using group-based CommonFolderQuery values like GroupByAlbum.
    /// </summary>
    public interface IStorageFolderQueryOperations
    {
        /// <summary>
        /// Gets an index-based range of files and folders from the list of all files and subfolders in the current folder.
        /// </summary>
        /// <returns>
        /// When this method completes successfully, it returns a list of the files and subfolders in the current folder. The list is of type IReadOnlyList IStorageItem. Each item in the list is represented by an IStorageItem object. To work with the returned items, call the IsOfType method of the IStorageItem interface to determine whether each item is a file or a folder. Then cast the item to a StorageFolder or StorageFile.
        /// </returns>
        /// <param name="startIndex">
        /// The zero-based index of the first item in the range to get.
        /// </param>
        /// <param name="maxItemsToRetrieve">
        /// The maximum number of items to get.
        /// </param>
        Task<IReadOnlyList<IStorageItem>> GetItemsAsync(int startIndex, int maxItemsToRetrieve);
    }
}