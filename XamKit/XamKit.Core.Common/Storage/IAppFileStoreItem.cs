namespace XamKit.Core.Common.Storage
{
    using System;

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
    }
}