namespace XamarinApiToolkit.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using XamarinApiToolkit.Storage.FileProperties;

    /// <summary>
    /// Defines an interface for an application storage item.
    /// </summary>
    public interface IAppStorageItem
    {
        /// <summary>
        /// Gets the date the item was created.
        /// </summary>
        DateTime DateCreated { get; }

        /// <summary>
        /// Gets the name of the item.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the full path to the item.
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Gets a value indicating whether the item exists.
        /// </summary>
        bool Exists { get; }

        /// <summary>
        /// Gets the parent folder for the item.
        /// </summary>
        IAppFolder Parent { get; }

        /// <summary>
        /// Renames the item with the specified new name.
        /// </summary>
        /// <param name="desiredNewName">
        /// The desired new name.
        /// </param>
        /// <returns>
        /// Returns a task.
        /// </returns>
        Task RenameAsync(string desiredNewName);

        /// <summary>
        /// Renames the item with the specified new name.
        /// </summary>
        /// <param name="desiredNewName">
        /// The desired new name.
        /// </param>
        /// <param name="option">
        /// The item name collision option.
        /// </param>
        /// <returns>
        /// Returns a task.
        /// </returns>
        Task RenameAsync(string desiredNewName, FileStoreNameCollisionOption option);

        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <remarks>
        /// If the item is a folder, it will delete all contents.
        /// </remarks>
        /// <returns>
        /// Returns a task.
        /// </returns>
        Task DeleteAsync();

        /// <summary>
        /// Gets the properties of the item.
        /// </summary>
        /// <returns>
        /// Returns the properties.
        /// </returns>
        Task<IDictionary<string, object>> GetPropertiesAsync();

        /// <summary>
        /// Checks whether the item is of a known type.
        /// </summary>
        /// <param name="type">
        /// The item type to check.
        /// </param>
        /// <returns>
        /// Returns true if matches; else false.
        /// </returns>
        bool IsOfType(FileStoreItemTypes type);
    }
}