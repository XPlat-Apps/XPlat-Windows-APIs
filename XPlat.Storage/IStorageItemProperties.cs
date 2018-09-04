namespace XPlat.Storage
{
    using XPlat.Storage.FileProperties;

    /// <summary>
    /// Provides access to common and content properties on items (like files and folders).
    /// </summary>
    public interface IStorageItemProperties
    {
        /// <summary>
        /// Gets the user-friendly name of the current item.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Gets an object that provides access to the content-related properties of the item.
        /// </summary>
        IStorageItemContentProperties Properties { get; }
    }
}