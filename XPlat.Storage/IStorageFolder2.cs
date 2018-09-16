namespace XPlat.Storage
{
    using System.Threading.Tasks;

    /// <summary>Manipulates folders and their contents, and provides information about them.</summary>
    public interface IStorageFolder2
    {
        /// <summary>Try to get a single file or sub-folder from the current folder by using the name of the item.</summary>
        /// <returns>When this method completes successfully, it returns the file or folder (type IStorageItem).</returns>
        /// <param name="name">The name (or path relative to the current folder) of the file or sub-folder to try to retrieve.</param>
        Task<IStorageItem> TryGetItemAsync(string name);
    }
}