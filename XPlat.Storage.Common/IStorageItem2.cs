namespace XPlat.Storage
{
    using System.Threading.Tasks;

    /// <summary>
    /// Manipulates storage items (files and folders) and their contents, and provides information about them.
    /// </summary>
    public interface IStorageItem2 : IStorageItem
    {
        /// <summary>
        /// Gets the parent folder of the current storage item.
        /// </summary>
        /// <returns>
        /// When this method completes, it returns the parent folder as a IStorageFolder.
        /// </returns>
        Task<IStorageFolder> GetParentAsync();

        /// <summary>
        /// Indicates whether the current item is the same as the specified item.
        /// </summary>
        /// <returns>
        /// Returns true if the current storage item is the same as the specified storage item; otherwise false.
        /// </returns>
        /// <param name="item">
        /// The IStorageItem object that represents a storage item to compare against.
        /// </param>
        bool IsEqual(IStorageItem item);
    }
}