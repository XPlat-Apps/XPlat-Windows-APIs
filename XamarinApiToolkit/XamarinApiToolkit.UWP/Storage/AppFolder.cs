namespace XamarinApiToolkit.Storage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using Windows.Storage;

    /// <summary>
    /// Defines an application folder.
    /// </summary>
    public sealed class AppFolder : IAppFolder
    {
        private readonly IStorageFolder folder;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppFolder"/> class.
        /// </summary>
        /// <param name="parentFolder">
        /// The parent folder.
        /// </param>
        /// <param name="folder">
        /// The associated <see cref="StorageFolder"/>
        /// </param>
        internal AppFolder(IAppFolder parentFolder, IStorageFolder folder)
        {
            if (folder == null)
            {
                throw new ArgumentNullException(nameof(folder));
            }

            this.folder = folder;
            this.Parent = parentFolder;
        }

        /// <summary>
        /// Gets the date the item was created.
        /// </summary>
        public DateTime DateCreated
        {
            get
            {
                return this.folder.DateCreated.DateTime;
            }
        }

        /// <summary>
        /// Gets the name of the item.
        /// </summary>
        public string Name
        {
            get
            {
                return this.folder.Name;
            }
        }

        /// <summary>
        /// Gets the full path to the item.
        /// </summary>
        public string Path
        {
            get
            {
                return this.folder.Path;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the item exists.
        /// </summary>
        public bool Exists
        {
            get
            {
                return this.folder != null && Directory.Exists(this.folder.Path);
            }
        }

        /// <summary>
        /// Gets the parent folder for the item.
        /// </summary>
        public IAppFolder Parent { get; private set; }

        /// <summary>
        /// Renames the item with the specified new name.
        /// </summary>
        /// <param name="desiredNewName">
        /// The desired new name.
        /// </param>
        /// <returns>
        /// Returns a task.
        /// </returns>
        public Task RenameAsync(string desiredNewName)
        {
            return this.RenameAsync(desiredNewName, FileStoreNameCollisionOption.FailIfExists);
        }

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
        public async Task RenameAsync(string desiredNewName, FileStoreNameCollisionOption option)
        {
            if (!this.Exists)
            {
                throw new FileNotFoundException("Cannot rename a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(desiredNewName))
            {
                throw new ArgumentNullException(nameof(desiredNewName));
            }

            await this.folder.RenameAsync(desiredNewName, option.ToNameCollisionOption());
        }

        public Task DeleteAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDictionary<string, object>> GetPropertiesAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks whether the item is of a known type.
        /// </summary>
        /// <param name="type">
        /// The item type to check.
        /// </param>
        /// <returns>
        /// Returns true if matches; else false.
        /// </returns>
        public bool IsOfType(FileStoreItemTypes type)
        {
            return type == FileStoreItemTypes.Folder;
        }

        public Task<IAppFile> CreateFileAsync(string desiredName)
        {
            throw new NotImplementedException();
        }

        public Task<IAppFile> CreateFileAsync(string desiredName, FileStoreCreationOption options)
        {
            throw new NotImplementedException();
        }

        public Task<IAppFolder> CreateFolderAsync(string desiredName)
        {
            throw new NotImplementedException();
        }

        public Task<IAppFolder> CreateFolderAsync(string desiredName, FileStoreCreationOption options)
        {
            throw new NotImplementedException();
        }

        public Task<IAppFile> GetFileAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IAppFile> GetFileAsync(string name, bool createIfNotExists)
        {
            throw new NotImplementedException();
        }

        public Task<IAppFolder> GetFolderAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IAppFolder> GetFolderAsync(string name, bool createIfNotExists)
        {
            throw new NotImplementedException();
        }

        public Task<IAppStorageItem> GetItemAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<IAppFile>> GetFilesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<IAppFolder>> GetFoldersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<IAppStorageItem>> GetItemsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
