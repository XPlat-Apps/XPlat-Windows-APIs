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
                throw new AppStorageItemNotFoundException(this.Name, "Cannot rename a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(desiredNewName))
            {
                throw new ArgumentNullException(nameof(desiredNewName));
            }

            await this.folder.RenameAsync(desiredNewName, option.ToNameCollisionOption());
        }

        /// <summary>
        /// Deletes the item.
        /// </summary>
        /// <remarks>
        /// If the item is a folder, it will delete all contents.
        /// </remarks>
        /// <returns>
        /// Returns a task.
        /// </returns>
        public async Task DeleteAsync()
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(this.Name, "Cannot delete a folder that does not exist.");
            }

            await this.folder.DeleteAsync();
        }

        /// <summary>
        /// Gets the properties of the item.
        /// </summary>
        /// <returns>
        /// Returns the properties.
        /// </returns>
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

        /// <summary>
        /// Creates a new file with the desired name in this folder.
        /// </summary>
        /// <param name="desiredName">
        /// The desired file name.
        /// </param>
        /// <returns>
        /// Returns the created file.
        /// </returns>
        public Task<IAppFile> CreateFileAsync(string desiredName)
        {
            return this.CreateFileAsync(desiredName, FileStoreCreationOption.FailIfExists);
        }

        /// <summary>
        /// Creates a new file with the desired name with creation options in this folder.
        /// </summary>
        /// <param name="desiredName">
        /// The desired file name.
        /// </param>
        /// <param name="options">
        /// The creation options.
        /// </param>
        /// <returns>
        /// Returns the created file.
        /// </returns>
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

        /// <summary>
        /// Creates a new folder with the desired name in this folder.
        /// </summary>
        /// <param name="desiredName">
        /// The desired folder name.
        /// </param>
        /// <returns>
        /// Returns the created folder.
        /// </returns>
        public Task<IAppFolder> CreateFolderAsync(string desiredName)
        {
            return this.CreateFolderAsync(desiredName, FileStoreCreationOption.FailIfExists);
        }

        /// <summary>
        /// Creates a new folder with the desired name with creation options in this folder.
        /// </summary>
        /// <param name="desiredName">
        /// The desired folder name.
        /// </param>
        /// <param name="options">
        /// The creation options.
        /// </param>
        /// <returns>
        /// Returns the created folder.
        /// </returns>
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

        /// <summary>
        /// Gets a file with the specified name from this folder.
        /// </summary>
        /// <param name="name">
        /// The name of the file to get.
        /// </param>
        /// <returns>
        /// Returns the file.
        /// </returns>
        public Task<IAppFile> GetFileAsync(string name)
        {
            return this.GetFileAsync(name, false);
        }

        /// <summary>
        /// Gets a file with the specified name from this folder.
        /// </summary>
        /// <param name="name">
        /// The name of the file to get.
        /// </param>
        /// <param name="createIfNotExists">
        /// A value indicating whether to create the file if it does not exist.
        /// </param>
        /// <returns>
        /// Returns the file.
        /// </returns>
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

        /// <summary>
        /// Gets a folder with the specified name from this folder.
        /// </summary>
        /// <param name="name">
        /// The name of the folder to get.
        /// </param>
        /// <returns>
        /// Returns the folder.
        /// </returns>
        public Task<IAppFolder> GetFolderAsync(string name)
        {
            return this.GetFolderAsync(name, false);
        }

        /// <summary>
        /// Gets a folder with the specified name from this folder.
        /// </summary>
        /// <param name="name">
        /// The name of the folder to get.
        /// </param>
        /// <param name="createIfNotExists">
        /// A value indicating whether to create the folder if it does not exist.
        /// </param>
        /// <returns>
        /// Returns the folder.
        /// </returns>
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

        /// <summary>
        /// Gets a storage item with the specified name from this folder.
        /// </summary>
        /// <remarks>
        /// A storage item can be a file or folder.
        /// </remarks>
        /// <param name="name">
        /// The name of the item to get.
        /// </param>
        /// <returns>
        /// Returns the file or folder.
        /// </returns>
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

        /// <summary>
        /// Gets all of the files from this folder.
        /// </summary>
        /// <returns>
        /// Returns a collection of files.
        /// </returns>
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

        /// <summary>
        /// Gets all of the folders from this folder.
        /// </summary>
        /// <returns>
        /// Returns a collection of folders.
        /// </returns>
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

        /// <summary>
        /// Gets all of the items from this folder.
        /// </summary>
        /// <returns>
        /// Returns a collection of folders and files.
        /// </returns>
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
        /// Returns an <see cref="AppFolder"/> for the path.
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