namespace XamKit.Core.Common.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAppFileStoreItem
    {
        /// <summary>
        /// Gets the parent folder.
        /// </summary>
        IAppFolder ParentFolder { get; }

        /// <summary>
        /// Gets the name of the current file store item.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the full path of the current file store item.
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Gets a value indicating whether the file store item exists.
        /// </summary>
        bool Exists { get; }

        /// <summary>
        /// Gets the date the file store item was created.
        /// </summary>
        DateTime DateCreated { get; }

        /// <summary>
        /// Deletes the file store item.
        /// </summary>
        /// <returns>
        /// Returns an await-able task.
        /// </returns>
        Task DeleteAsync();

        /// <summary>
        /// Gets the properties of the file store item.
        /// </summary>
        /// <returns>
        /// Returns a collection of file store properties.
        /// </returns>
        Task<IEnumerable<FileStoreProperty>> GetPropertiesAsync();
    }
}