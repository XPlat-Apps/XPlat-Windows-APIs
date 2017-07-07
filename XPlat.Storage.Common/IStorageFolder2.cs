namespace XPlat.Storage
{
    using System.Threading.Tasks;

    /// <summary>
    /// Manipulates folders and their contents, and provides information about them.
    /// </summary>
    public interface IStorageFolder2
    {
        /// <summary>
        /// Tries to get the file or folder with the specified name from the current folder. Returns null instead of raising a FileNotFoundException if the specified file or folder is not found.
        /// </summary>
        /// <returns>
        /// When this method completes successfully, it returns an IStorageItem that represents the specified file or folder. If the specified file or folder is not found, this method returns null instead of raising an exception.To work with the returned item, call the IsOfType method of the IStorageItem interface to determine whether the item is a file or a folder. Then cast the item to a StorageFolder or StorageFile.
        /// </returns>
        /// <param name="name">
        /// The name of the file or folder to get.
        /// </param>
        Task<IStorageItem> TryGetItemAsync(string name);
    }
}
