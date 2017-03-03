namespace XPlat.API.Storage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
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

        /// <inheritdoc />
        public DateTime DateCreated
        {
            get
            {
                return this.folder.DateCreated.DateTime;
            }
        }

        /// <inheritdoc />
        public string Name
        {
            get
            {
                return this.folder.Name;
            }
        }

        /// <inheritdoc />
        public string Path
        {
            get
            {
                return this.folder.Path;
            }
        }

        /// <inheritdoc />
        public bool Exists
        {
            get
            {
                return this.folder != null && Directory.Exists(this.folder.Path);
            }
        }

        /// <inheritdoc />
        public IAppFolder Parent { get; private set; }

        /// <inheritdoc />
        public Task RenameAsync(string desiredName)
        {
            return this.RenameAsync(desiredName, FileStoreNameCollisionOption.FailIfExists);
        }

        /// <inheritdoc />
        public async Task RenameAsync(string desiredName, FileStoreNameCollisionOption option)
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(this.Name, "Cannot rename a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(desiredName))
            {
                throw new ArgumentNullException(nameof(desiredName));
            }

            await this.folder.RenameAsync(desiredName, option.ToNameCollisionOption());
        }

        /// <inheritdoc />
        public async Task DeleteAsync()
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(this.Name, "Cannot delete a folder that does not exist.");
            }

            await this.folder.DeleteAsync();
        }

        /// <inheritdoc />
        public async Task<IDictionary<string, object>> GetPropertiesAsync()
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(
                          this.Name,
                          "Cannot get properties for a folder that does not exist.");
            }

            var storageFolder = this.folder as StorageFolder;
            return storageFolder != null ? await storageFolder.Properties.RetrievePropertiesAsync(null) : null;
        }

        /// <inheritdoc />
        public bool IsOfType(FileStoreItemTypes type)
        {
            return type == FileStoreItemTypes.Folder;
        }

        /// <inheritdoc />
        public Task<IAppFile> CreateFileAsync(string desiredName)
        {
            return this.CreateFileAsync(desiredName, FileStoreCreationOption.FailIfExists);
        }

        /// <inheritdoc />
        public async Task<IAppFile> CreateFileAsync(string desiredName, FileStoreCreationOption options)
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(
                          this.Name,
                          "Cannot create a file in a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(desiredName))
            {
                throw new ArgumentNullException(nameof(desiredName));
            }

            var storageFile = await this.folder.CreateFileAsync(desiredName, options.ToCreationCollisionOption());
            return new AppFile(this, storageFile);
        }

        /// <inheritdoc />
        public Task<IAppFolder> CreateFolderAsync(string desiredName)
        {
            return this.CreateFolderAsync(desiredName, FileStoreCreationOption.FailIfExists);
        }

        /// <inheritdoc />
        public async Task<IAppFolder> CreateFolderAsync(string desiredName, FileStoreCreationOption options)
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(
                          this.Name,
                          "Cannot create a folder in a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(desiredName))
            {
                throw new ArgumentNullException(nameof(desiredName));
            }

            var storageFolder = await this.folder.CreateFolderAsync(desiredName, options.ToCreationCollisionOption());
            return new AppFolder(this, storageFolder);
        }

        /// <inheritdoc />
        public Task<IAppFile> GetFileAsync(string name)
        {
            return this.GetFileAsync(name, false);
        }

        /// <inheritdoc />
        public async Task<IAppFile> GetFileAsync(string name, bool createIfNotExists)
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(
                          this.Name,
                          "Cannot get a file from a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            StorageFile storageFile = null;

            try
            {
                storageFile = await this.folder.GetFileAsync(name);
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
                return await this.CreateFileAsync(name, FileStoreCreationOption.OpenIfExists);
            }

            return new AppFile(this, storageFile);
        }

        /// <inheritdoc />
        public Task<IAppFolder> GetFolderAsync(string name)
        {
            return this.GetFolderAsync(name, false);
        }

        /// <inheritdoc />
        public async Task<IAppFolder> GetFolderAsync(string name, bool createIfNotExists)
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(
                          this.Name,
                          "Cannot get a folder from a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            StorageFolder storageFolder = null;

            try
            {
                storageFolder = await this.folder.GetFolderAsync(name);
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
                return await this.CreateFolderAsync(name, FileStoreCreationOption.OpenIfExists);
            }

            return new AppFolder(this, storageFolder);
        }

        /// <inheritdoc />
        public async Task<IAppStorageItem> GetItemAsync(string name)
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(
                          this.Name,
                          "Cannot get an item from a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var storageItem = await this.folder.GetItemAsync(name);

            if (storageItem == null)
            {
                throw new AppStorageItemNotFoundException(name, "The item could not be found in the folder.");
            }

            if (storageItem.IsOfType(StorageItemTypes.None))
            {
                throw new InvalidOperationException("The item is not a valid storage type.");
            }

            if (storageItem.IsOfType(StorageItemTypes.File))
            {
                var storageFile = storageItem as StorageFile;
                return new AppFile(this, storageFile);
            }

            if (storageItem.IsOfType(StorageItemTypes.Folder))
            {
                var storageFolder = storageItem as StorageFolder;
                return new AppFolder(this, storageFolder);
            }

            return null;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<IAppFile>> GetFilesAsync()
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(
                          this.Name,
                          "Cannot get files from a folder that does not exist.");
            }

            var storageFiles = await this.folder.GetFilesAsync();
            return storageFiles.Select(storageFile => new AppFile(this, storageFile)).ToList();
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<IAppFolder>> GetFoldersAsync()
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(
                          this.Name,
                          "Cannot get folders from a folder that does not exist.");
            }

            var storageFolders = await this.folder.GetFoldersAsync();
            return storageFolders.Select(storageFolder => new AppFolder(this, storageFolder)).ToList();
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<IAppStorageItem>> GetItemsAsync()
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(
                          this.Name,
                          "Cannot get items from a folder that does not exist.");
            }

            var storageItems = await this.folder.GetItemsAsync();

            var result = new List<IAppStorageItem>();

            foreach (var storageItem in storageItems)
            {
                if (storageItem == null || storageItem.IsOfType(StorageItemTypes.None))
                {
                    continue;
                }

                if (storageItem.IsOfType(StorageItemTypes.File))
                {
                    var storageFile = storageItem as StorageFile;
                    result.Add(new AppFile(this, storageFile));
                }
                else if (storageItem.IsOfType(StorageItemTypes.Folder))
                {
                    var storageFolder = storageItem as StorageFolder;
                    result.Add(new AppFolder(this, storageFolder));
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
        public static async Task<AppFolder> GetFolderFromPathAsync(string path)
        {
            StorageFolder pathFolder;
            StorageFolder parentPathFolder;

            AppFolder resultParentFolder;

            try
            {
                pathFolder = await StorageFolder.GetFolderFromPathAsync(path);
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

            var resultFolder = new AppFolder(resultParentFolder, pathFolder);

            return resultFolder;
        }
    }
}