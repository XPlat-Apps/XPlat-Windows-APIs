namespace XPlat.Storage
{
    using System;
    using System.Threading.Tasks;

    using XPlat.Storage.FileProperties;

    /// <summary>Manipulates storage items (files and folders) and their contents, and provides information about them.</summary>
    public interface IStorageItem
    {
        /// <summary>Renames the current item.</summary>
        /// <returns>No object or value is returned by this method when it completes.</returns>
        /// <param name="desiredName">The desired, new name of the item.</param>
        Task RenameAsync(string desiredName);

        /// <summary>Renames the current item. This method also specifies what to do if an existing item in the current item's location has the same name.</summary>
        /// <returns>No object or value is returned by this method when it completes.</returns>
        /// <param name="desiredName">The desired, new name of the current item. If there is an existing item in the current item's location that already has the specified desiredName, the specified NameCollisionOption determines how Windows responds to the conflict.</param>
        /// <param name="option">The enum value that determines how the system responds if the desiredName is the same as the name of an existing item in the current item's location.</param>
        Task RenameAsync(string desiredName, NameCollisionOption option);

        /// <summary>Deletes the current item.</summary>
        /// <returns>No object or value is returned by this method when it completes.</returns>
        Task DeleteAsync();

        /// <summary>Gets the basic properties of the current item (like a file or folder).</summary>
        /// <returns>When this method completes successfully, it returns the basic properties of the current item as a BasicProperties object.</returns>
        Task<IBasicProperties> GetBasicPropertiesAsync();

        /// <summary>Determines whether the current IStorageItem matches the specified StorageItemTypes value.</summary>
        /// <returns>True if the IStorageItem matches the specified value; otherwise false.</returns>
        /// <param name="type">The value to match against.</param>
        bool IsOfType(StorageItemTypes type);

        /// <summary>Gets the attributes of a storage item.</summary>
        FileAttributes Attributes { get; }

        /// <summary>Gets the date and time when the current item was created.</summary>
        DateTime DateCreated { get; }

        /// <summary>Gets the name of the item including the file name extension if there is one.</summary>
        string Name { get; }

        /// <summary>Gets the full file-system path of the item, if the item has a path.</summary>
        string Path { get; }

        /// <summary>Gets a value indicating whether the item exists.</summary>
        bool Exists { get; }
    }
}