namespace XPlat.Storage.Search
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>Provides methods to create search queries and retrieve files from a folder. This interface is implemented by StorageFolder objects, which can represent file system folders, libraries, or virtual folders that are automatically generated when queries are created using group-based CommonFolderQuery values like GroupByAlbum.</summary>
    public interface IStorageFolderQueryOperations
    {
        /// <summary>Retrieves a list items like files, folders, or file groups, in a specified range (shallow enumeration).</summary>
        /// <returns>When this method completes successfully, it returns a list (type IVectorView) of items. Each item is the IStorageItem type and represents a file, folder, or file group. In this list, files are represented by StorageFile objects, and folders or file groups are represented by StorageFolder objects.</returns>
        /// <param name="startIndex">The zero-based index of the first item in the range. This parameter defaults to 0.</param>
        /// <param name="maxItemsToRetrieve">The maximum number of items to retrieve. Use -1 to retrieve all items.</param>
        Task<IReadOnlyList<IStorageItem>> GetItemsAsync(int startIndex, int maxItemsToRetrieve);
    }
}