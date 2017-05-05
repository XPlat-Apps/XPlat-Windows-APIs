namespace XPlat.Storage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using XPlat.Storage.FileProperties;

    /// <summary>
    /// Defines an application folder.
    /// </summary>
    public sealed class StorageFolder : IStorageFolder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StorageFolder"/> class.
        /// </summary>
        /// <param name="parentFolder">
        /// The parent folder.
        /// </param>
        /// <param name="folder">
        /// The associated <see cref="StorageFolder"/>
        /// </param>
        internal StorageFolder(IStorageFolder parentFolder, Windows.Storage.StorageFolder folder)
        {
            if (folder == null)
            {
                throw new ArgumentNullException(nameof(folder));
            }

            this.Originator = folder;
            this.Parent = parentFolder;
        }

        /// <summary>
        /// Gets the originating Windows storage folder.
        /// </summary>
        public Windows.Storage.StorageFolder Originator { get; }

        /// <inheritdoc />
        public DateTime DateCreated => this.Originator.DateCreated.DateTime;

        /// <inheritdoc />
        public string Name => this.Originator.Name;

        /// <inheritdoc />
        public string Path => this.Originator.Path;

        /// <inheritdoc />
        public bool Exists => this.Originator != null && Directory.Exists(this.Originator.Path);

        /// <inheritdoc />
        public IStorageFolder Parent { get; }

        /// <inheritdoc />
        public Task RenameAsync(string desiredName)
        {
            return this.RenameAsync(desiredName, NameCollisionOption.FailIfExists);
        }

        /// <inheritdoc />
        public async Task RenameAsync(string desiredName, NameCollisionOption option)
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot rename a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(desiredName))
            {
                throw new ArgumentNullException(nameof(desiredName));
            }

            await this.Originator.RenameAsync(desiredName, option.ToNameCollisionOption());
        }

        /// <inheritdoc />
        public async Task DeleteAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot delete a folder that does not exist.");
            }

            await this.Originator.DeleteAsync();
        }

        /// <inheritdoc />
        public async Task<IDictionary<string, object>> GetPropertiesAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(
                          this.Name,
                          "Cannot get properties for a folder that does not exist.");
            }

            var storageFolder = this.Originator as Windows.Storage.StorageFolder;
            return storageFolder != null ? await storageFolder.Properties.RetrievePropertiesAsync(null) : null;
        }

        /// <inheritdoc />
        public bool IsOfType(StorageItemTypes type)
        {
            return type == StorageItemTypes.Folder;
        }

        /// <inheritdoc />
        public async Task<IBasicProperties> GetBasicPropertiesAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(
                    this.Name,
                    "Cannot get properties for a folder that does not exist.");
            }

            var storageFolder = this.Originator as Windows.Storage.StorageFolder;
            if (storageFolder == null) return null;

            var basicProperties = await storageFolder.GetBasicPropertiesAsync();
            return basicProperties.ToBasicProperties();
        }

        /// <inheritdoc />
        public Task<IStorageFile> CreateFileAsync(string desiredName)
        {
            return this.CreateFileAsync(desiredName, CreationCollisionOption.FailIfExists);
        }

        /// <inheritdoc />
        public async Task<IStorageFile> CreateFileAsync(string desiredName, CreationCollisionOption options)
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(
                          this.Name,
                          "Cannot create a file in a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(desiredName))
            {
                throw new ArgumentNullException(nameof(desiredName));
            }

            var storageFile = await this.Originator.CreateFileAsync(desiredName, options.ToCreationCollisionOption());
            return new StorageFile(this, storageFile);
        }

        /// <inheritdoc />
        public Task<IStorageFolder> CreateFolderAsync(string desiredName)
        {
            return this.CreateFolderAsync(desiredName, CreationCollisionOption.FailIfExists);
        }

        /// <inheritdoc />
        public async Task<IStorageFolder> CreateFolderAsync(string desiredName, CreationCollisionOption options)
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(
                          this.Name,
                          "Cannot create a folder in a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(desiredName))
            {
                throw new ArgumentNullException(nameof(desiredName));
            }

            var storageFolder = await this.Originator.CreateFolderAsync(desiredName, options.ToCreationCollisionOption());
            return new StorageFolder(this, storageFolder);
        }

        /// <inheritdoc />
        public Task<IStorageFile> GetFileAsync(string name)
        {
            return this.GetFileAsync(name, false);
        }

        /// <inheritdoc />
        public async Task<IStorageFile> GetFileAsync(string name, bool createIfNotExists)
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(
                          this.Name,
                          "Cannot get a file from a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Windows.Storage.StorageFile storageFile = null;

            try
            {
                storageFile = await this.Originator.GetFileAsync(name);
            }
            catch (Exception)
            {
                if (!createIfNotExists)
                {
                    throw;
                }
            }

            if (createIfNotExists && storageFile == null)
            {
                return await this.CreateFileAsync(name, CreationCollisionOption.OpenIfExists);
            }

            return new StorageFile(this, storageFile);
        }

        /// <inheritdoc />
        public Task<IStorageFolder> GetFolderAsync(string name)
        {
            return this.GetFolderAsync(name, false);
        }

        /// <inheritdoc />
        public async Task<IStorageFolder> GetFolderAsync(string name, bool createIfNotExists)
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(
                          this.Name,
                          "Cannot get a folder from a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            Windows.Storage.StorageFolder storageFolder = null;

            try
            {
                storageFolder = await this.Originator.GetFolderAsync(name);
            }
            catch (Exception)
            {
                if (!createIfNotExists)
                {
                    throw;
                }
            }

            if (createIfNotExists && storageFolder == null)
            {
                return await this.CreateFolderAsync(name, CreationCollisionOption.OpenIfExists);
            }

            return new StorageFolder(this, storageFolder);
        }

        /// <inheritdoc />
        public async Task<IStorageItem> GetItemAsync(string name)
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(
                          this.Name,
                          "Cannot get an item from a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var storageItem = await this.Originator.GetItemAsync(name);

            if (storageItem == null)
            {
                throw new StorageItemNotFoundException(name, "The item could not be found in the folder.");
            }

            if (storageItem.IsOfType(Windows.Storage.StorageItemTypes.None))
            {
                throw new InvalidOperationException("The item is not a valid storage type.");
            }

            if (storageItem.IsOfType(Windows.Storage.StorageItemTypes.File))
            {
                var storageFile = storageItem as Windows.Storage.StorageFile;
                return new StorageFile(this, storageFile);
            }

            if (storageItem.IsOfType(Windows.Storage.StorageItemTypes.Folder))
            {
                var storageFolder = storageItem as Windows.Storage.StorageFolder;
                return new StorageFolder(this, storageFolder);
            }

            return null;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<IStorageFile>> GetFilesAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(
                          this.Name,
                          "Cannot get files from a folder that does not exist.");
            }

            var storageFiles = await this.Originator.GetFilesAsync();
            return storageFiles.Select(storageFile => new StorageFile(this, storageFile)).ToList();
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<IStorageFolder>> GetFoldersAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(
                          this.Name,
                          "Cannot get folders from a folder that does not exist.");
            }

            var storageFolders = await this.Originator.GetFoldersAsync();
            return storageFolders.Select(storageFolder => new StorageFolder(this, storageFolder)).ToList();
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<IStorageItem>> GetItemsAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(
                          this.Name,
                          "Cannot get items from a folder that does not exist.");
            }

            var storageItems = await this.Originator.GetItemsAsync();

            var result = new List<IStorageItem>();

            foreach (var storageItem in storageItems)
            {
                if (storageItem == null || storageItem.IsOfType(Windows.Storage.StorageItemTypes.None))
                {
                    continue;
                }

                if (storageItem.IsOfType(Windows.Storage.StorageItemTypes.File))
                {
                    var storageFile = storageItem as Windows.Storage.StorageFile;
                    result.Add(new StorageFile(this, storageFile));
                }
                else if (storageItem.IsOfType(Windows.Storage.StorageItemTypes.Folder))
                {
                    var storageFolder = storageItem as Windows.Storage.StorageFolder;
                    result.Add(new StorageFolder(this, storageFolder));
                }
            }

            return result;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<IStorageItem>> GetItemsAsync(int startIndex, int maxItemsToRetrieve)
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(
                    this.Name,
                    "Cannot get items from a folder that does not exist.");
            }

            var storageItems = await this.Originator.GetItemsAsync((uint)startIndex, (uint)maxItemsToRetrieve);

            var result = new List<IStorageItem>();

            foreach (var storageItem in storageItems)
            {
                if (storageItem == null || storageItem.IsOfType(Windows.Storage.StorageItemTypes.None))
                {
                    continue;
                }

                if (storageItem.IsOfType(Windows.Storage.StorageItemTypes.File))
                {
                    var storageFile = storageItem as Windows.Storage.StorageFile;
                    result.Add(new StorageFile(this, storageFile));
                }
                else if (storageItem.IsOfType(Windows.Storage.StorageItemTypes.Folder))
                {
                    var storageFolder = storageItem as Windows.Storage.StorageFolder;
                    result.Add(new StorageFolder(this, storageFolder));
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the folder that has the specified absolute path in the file system.
        /// </summary>
        /// <param name="path">
        /// The absolute path in the file system (not the Uri) of the folder to get.
        /// </param>
        /// <returns>
        /// When this method completes successfully, it returns an IAppFolder that represents the specified folder.
        /// </returns>
        public static async Task<StorageFolder> GetFolderFromPathAsync(string path)
        {
            Windows.Storage.StorageFolder pathFolder;
            Windows.Storage.StorageFolder parentPathFolder;

            StorageFolder resultParentFolder;

            try
            {
                pathFolder = await Windows.Storage.StorageFolder.GetFolderFromPathAsync(path);
            }
            catch (Exception)
            {
                pathFolder = null;
            }

            if (pathFolder == null)
            {
                return null;
            }

            try
            {
                parentPathFolder = await pathFolder.GetParentAsync();
            }
            catch (Exception)
            {
                parentPathFolder = null;
            }

            if (parentPathFolder != null)
            {
                resultParentFolder = await GetFolderFromPathAsync(parentPathFolder.Path);
            }
            else
            {
                resultParentFolder = null;
            }

            var resultFolder = new StorageFolder(resultParentFolder, pathFolder);

            return resultFolder;
        }
    }
}