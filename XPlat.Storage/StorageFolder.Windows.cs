#if WINDOWS_UWP
namespace XPlat.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using XPlat.Storage.Extensions;
    using XPlat.Storage.FileProperties;

    /// <summary>
    /// Defines an application folder.
    /// </summary>
    public sealed class StorageFolder : IStorageFolder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StorageFolder"/> class.
        /// </summary>
        /// <param name="folder">
        /// The associated <see cref="StorageFolder"/>
        /// </param>
        internal StorageFolder(Windows.Storage.StorageFolder folder)
        {
            this.Originator = folder ?? throw new ArgumentNullException(nameof(folder));
        }

        /// <summary>Gets the instance of the <see cref="Windows.Storage.StorageFolder"/> object associated with this folder.</summary>
        public Windows.Storage.StorageFolder Originator { get; }

        /// <summary>Gets the date and time when the current item was created.</summary>
        public DateTime DateCreated => this.Originator.DateCreated.DateTime;

        /// <summary>Gets the name of the item including the file name extension if there is one.</summary>
        public string Name => this.Originator.Name;

        /// <summary>Gets the user-friendly name of the item.</summary>
        public string DisplayName => this.Originator.DisplayName;

        /// <summary>Gets an object that provides access to the content-related properties of the item.</summary>
        public IStorageItemContentProperties Properties => new StorageItemContentProperties(new WeakReference(this));

        /// <summary>Gets the full file-system path of the item, if the item has a path.</summary>
        public string Path => this.Originator.Path;

        /// <summary>Gets a value indicating whether the item exists.</summary>
        public bool Exists => this.Originator != null;

        /// <summary>Gets the attributes of a storage item.</summary>
        public FileAttributes Attributes => (FileAttributes)(int)this.Originator.Attributes;

        public static implicit operator StorageFolder(Windows.Storage.StorageFolder folder)
        {
            return new StorageFolder(folder);
        }

        public static async Task<IStorageFolder> GetFolderFromPathAsync(string path)
        {
            Windows.Storage.StorageFolder pathFolder;

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

            StorageFolder resultFolder = new StorageFolder(pathFolder);

            return resultFolder;
        }

        /// <summary>Renames the current item.</summary>
        /// <returns>No object or value is returned by this method when it completes.</returns>
        /// <param name="desiredName">The desired, new name of the item.</param>
        public Task RenameAsync(string desiredName)
        {
            return this.RenameAsync(desiredName, NameCollisionOption.FailIfExists);
        }

        /// <summary>Renames the current item. This method also specifies what to do if an existing item in the current item's location has the same name.</summary>
        /// <returns>No object or value is returned by this method when it completes.</returns>
        /// <param name="desiredName">The desired, new name of the current item. If there is an existing item in the current item's location that already has the specified desiredName, the specified NameCollisionOption determines how Windows responds to the conflict.</param>
        /// <param name="option">The enum value that determines how the system responds if the desiredName is the same as the name of an existing item in the current item's location.</param>
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

            await this.Originator.RenameAsync(desiredName, option.ToWindowsNameCollisionOption());
        }

        /// <summary>Deletes the current item.</summary>
        /// <returns>No object or value is returned by this method when it completes.</returns>
        public async Task DeleteAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot delete a folder that does not exist.");
            }

            await this.Originator.DeleteAsync();
        }

        /// <summary>Determines whether the current IStorageItem matches the specified StorageItemTypes value.</summary>
        /// <returns>True if the IStorageItem matches the specified value; otherwise false.</returns>
        /// <param name="type">The value to match against.</param>
        public bool IsOfType(StorageItemTypes type)
        {
            return type == StorageItemTypes.Folder;
        }

        /// <summary>Gets the basic properties of the current item (like a file or folder).</summary>
        /// <returns>When this method completes successfully, it returns the basic properties of the current item as a BasicProperties object.</returns>
        public async Task<IBasicProperties> GetBasicPropertiesAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(
                    this.Name,
                    "Cannot get properties for a folder that does not exist.");
            }

            Windows.Storage.StorageFolder storageFolder = this.Originator;
            if (storageFolder == null)
            {
                return null;
            }

            Windows.Storage.FileProperties.BasicProperties basicProperties = await storageFolder.GetBasicPropertiesAsync();
            return new BasicProperties(basicProperties);
        }

        /// <summary>Gets the parent folder of the current storage item.</summary>
        /// <returns>When this method completes, it returns the parent folder as a StorageFolder.</returns>
        public async Task<IStorageFolder> GetParentAsync()
        {
            Windows.Storage.StorageFolder parent = await this.Originator.GetParentAsync();
            return parent == null ? null : new StorageFolder(parent);
        }

        /// <summary>Indicates whether the current item is the same as the specified item.</summary>
        /// <returns>Returns true if the current storage item is the same as the specified storage item; otherwise false.</returns>
        /// <param name="item">The IStorageItem object that represents a storage item to compare against.</param>
        public bool IsEqual(IStorageItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return item.Path.Equals(this.Path, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>Creates a new file in the current folder.</summary>
        /// <returns>When this method completes, it returns the new file as a StorageFile.</returns>
        /// <param name="desiredName">The desired name of the file to create.</param>
        public Task<IStorageFile> CreateFileAsync(string desiredName)
        {
            return this.CreateFileAsync(desiredName, CreationCollisionOption.FailIfExists);
        }

        /// <summary>Creates a new file in the current folder, and specifies what to do if a file with the same name already exists in the current folder.</summary>
        /// <returns>When this method completes, it returns the new file as a StorageFile.</returns>
        /// <param name="desiredName">The desired name of the file to create. If there is an existing file in the current folder that already has the specified desiredName, the specified CreationCollisionOption determines how Windows responds to the conflict.</param>
        /// <param name="options">The enum value that determines how Windows responds if the desiredName is the same as the name of an existing file in the current folder.</param>
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

            Windows.Storage.StorageFile storageFile = await this.Originator.CreateFileAsync(desiredName, options.ToWindowsCreationCollisionOption());
            return new StorageFile(storageFile);
        }

        /// <summary>Creates a new folder in the current folder.</summary>
        /// <returns>When this method completes, it returns the new folder as a StorageFolder.</returns>
        /// <param name="desiredName">The desired name of the folder to create.</param>
        public Task<IStorageFolder> CreateFolderAsync(string desiredName)
        {
            return this.CreateFolderAsync(desiredName, CreationCollisionOption.FailIfExists);
        }

        /// <summary>Creates a new folder in the current folder, and specifies what to do if a folder with the same name already exists in the current folder.</summary>
        /// <returns>When this method completes, it returns the new folder as a StorageFolder.</returns>
        /// <param name="desiredName">The desired name of the folder to create. If there is an existing folder in the current folder that already has the specified desiredName, the specified CreationCollisionOption determines how Windows responds to the conflict.</param>
        /// <param name="options">The enum value that determines how Windows responds if the desiredName is the same as the name of an existing folder in the current folder.</param>
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

            Windows.Storage.StorageFolder storageFolder =
                await this.Originator.CreateFolderAsync(desiredName, options.ToWindowsCreationCollisionOption());
            return new StorageFolder(storageFolder);
        }

        /// <summary>Gets the specified file from the current folder.</summary>
        /// <returns>When this method completes successfully, it returns a StorageFile that represents the file.</returns>
        /// <param name="name">The name (or path relative to the current folder) of the file to retrieve.</param>
        public Task<IStorageFile> GetFileAsync(string name)
        {
            return this.GetFileAsync(name, false);
        }

        /// <summary>Gets the specified file from the current folder.</summary>
        /// <param name="name">The name of the file to retrieve. If the file does not exist in the current folder, the createIfNotExists flag will allow the API to create the file if set to true.</param>
        /// <param name="createIfNotExists">A value indicating whether to create the file if it does not exist.</param>
        /// <returns>When this method completes successfully, it returns an IStorageFile that represents the file.</returns>
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

            return new StorageFile(storageFile);
        }

        /// <summary>Gets the specified folder from the current folder.</summary>
        /// <returns>When this method completes successfully, it returns a StorageFolder that represents the child folder.</returns>
        /// <param name="name">The name of the child folder to retrieve.</param>
        public Task<IStorageFolder> GetFolderAsync(string name)
        {
            return this.GetFolderAsync(name, false);
        }

        /// <summary>Gets the specified folder from the current folder.</summary>
        /// <param name="name">The name of the child folder to retrieve. If the child folder does not exist in the current folder, the createIfNotExists flag will allow the API to create the folder if set to true.</param>
        /// <param name="createIfNotExists">A value indicating whether to create the child folder if it does not exist.</param>
        /// <returns>When this method completes successfully, it returns an IStorageFolder that represents the child folder.</returns>
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

            return new StorageFolder(storageFolder);
        }

        /// <summary>Clears the folder of it's contents.</summary>
        /// <returns>An object that is used to manage the asynchronous clear operation.</returns>
        public Task ClearAsync()
        {
            return Task.Run(
                async () =>
                {
                    foreach (IStorageFile file in await this.GetFilesAsync())
                    {
                        await file.DeleteAsync();
                    }

                    foreach (IStorageFolder subfolder in await this.GetFoldersAsync())
                    {
                        await subfolder.DeleteAsync();
                    }
                });
        }

        /// <summary>Gets the specified item from the IStorageFolder.</summary>
        /// <returns>When this method completes successfully, it returns the file or folder (type IStorageItem).</returns>
        /// <param name="name">The name of the item to retrieve.</param>
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

            Windows.Storage.IStorageItem storageItem = await this.Originator.GetItemAsync(name);

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
                Windows.Storage.StorageFile storageFile = storageItem as Windows.Storage.StorageFile;
                return new StorageFile(storageFile);
            }

            if (storageItem.IsOfType(Windows.Storage.StorageItemTypes.Folder))
            {
                Windows.Storage.StorageFolder storageFolder = storageItem as Windows.Storage.StorageFolder;
                return new StorageFolder(storageFolder);
            }

            return null;
        }

        /// <summary>Gets the files from the current folder.</summary>
        /// <returns>When this method completes successfully, it returns a list of the files (type IVectorView) in the folder. Each file in the list is represented by a StorageFile object.</returns>
        public async Task<IReadOnlyList<IStorageFile>> GetFilesAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(
                    this.Name,
                    "Cannot get files from a folder that does not exist.");
            }

            IReadOnlyList<Windows.Storage.StorageFile> storageFiles = await this.Originator.GetFilesAsync();
            return storageFiles.Select(storageFile => new StorageFile(storageFile)).ToList();
        }

        /// <summary>Gets the folders in the current folder.</summary>
        /// <returns>When this method completes successfully, it returns a list of the files (type IVectorView). Each folder in the list is represented by a StorageFolder.</returns>
        public async Task<IReadOnlyList<IStorageFolder>> GetFoldersAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(
                    this.Name,
                    "Cannot get folders from a folder that does not exist.");
            }

            IReadOnlyList<Windows.Storage.StorageFolder> storageFolders = await this.Originator.GetFoldersAsync();
            return storageFolders.Select(storageFolder => new StorageFolder(storageFolder)).ToList();
        }

        /// <summary>Gets the items from the current folder.</summary>
        /// <returns>When this method completes successfully, it returns a list of the files and folders (type IVectorView). The files and folders in the list are represented by objects of type IStorageItem.</returns>
        public async Task<IReadOnlyList<IStorageItem>> GetItemsAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(
                    this.Name,
                    "Cannot get items from a folder that does not exist.");
            }

            IReadOnlyList<Windows.Storage.IStorageItem> storageItems = await this.Originator.GetItemsAsync();

            List<IStorageItem> result = new List<IStorageItem>();

            foreach (Windows.Storage.IStorageItem storageItem in storageItems)
            {
                if (storageItem == null || storageItem.IsOfType(Windows.Storage.StorageItemTypes.None))
                {
                    continue;
                }

                if (storageItem.IsOfType(Windows.Storage.StorageItemTypes.File))
                {
                    Windows.Storage.StorageFile storageFile = storageItem as Windows.Storage.StorageFile;
                    result.Add(new StorageFile(storageFile));
                }
                else if (storageItem.IsOfType(Windows.Storage.StorageItemTypes.Folder))
                {
                    Windows.Storage.StorageFolder storageFolder = storageItem as Windows.Storage.StorageFolder;
                    result.Add(new StorageFolder(storageFolder));
                }
            }

            return result;
        }

        /// <summary>Retrieves a list items like files, folders, or file groups, in a specified range (shallow enumeration).</summary>
        /// <returns>When this method completes successfully, it returns a list (type IVectorView) of items. Each item is the IStorageItem type and represents a file, folder, or file group. In this list, files are represented by StorageFile objects, and folders or file groups are represented by StorageFolder objects.</returns>
        /// <param name="startIndex">The zero-based index of the first item in the range. This parameter defaults to 0.</param>
        /// <param name="maxItemsToRetrieve">The maximum number of items to retrieve. Use -1 to retrieve all items.</param>
        public async Task<IReadOnlyList<IStorageItem>> GetItemsAsync(int startIndex, int maxItemsToRetrieve)
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(
                    this.Name,
                    "Cannot get items from a folder that does not exist.");
            }

            IReadOnlyList<Windows.Storage.IStorageItem> storageItems = await this.Originator.GetItemsAsync((uint)startIndex, (uint)maxItemsToRetrieve);

            List<IStorageItem> result = new List<IStorageItem>();

            foreach (Windows.Storage.IStorageItem storageItem in storageItems)
            {
                if (storageItem == null || storageItem.IsOfType(Windows.Storage.StorageItemTypes.None))
                {
                    continue;
                }

                if (storageItem.IsOfType(Windows.Storage.StorageItemTypes.File))
                {
                    Windows.Storage.StorageFile storageFile = storageItem as Windows.Storage.StorageFile;
                    result.Add(new StorageFile(storageFile));
                }
                else if (storageItem.IsOfType(Windows.Storage.StorageItemTypes.Folder))
                {
                    Windows.Storage.StorageFolder storageFolder = storageItem as Windows.Storage.StorageFolder;
                    result.Add(new StorageFolder(storageFolder));
                }
            }

            return result;
        }

        /// <summary>Try to get a single file or sub-folder from the current folder by using the name of the item.</summary>
        /// <returns>When this method completes successfully, it returns the file or folder (type IStorageItem).</returns>
        /// <param name="name">The name (or path relative to the current folder) of the file or sub-folder to try to retrieve.</param>
        public async Task<IStorageItem> TryGetItemAsync(string name)
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

            Windows.Storage.IStorageItem storageItem = await this.Originator.TryGetItemAsync(name);
            if (storageItem == null)
            {
                return null;
            }

            if (storageItem.IsOfType(Windows.Storage.StorageItemTypes.File))
            {
                Windows.Storage.StorageFile storageFile = storageItem as Windows.Storage.StorageFile;
                return new StorageFile(storageFile);
            }

            if (storageItem.IsOfType(Windows.Storage.StorageItemTypes.Folder))
            {
                Windows.Storage.StorageFolder storageFolder = storageItem as Windows.Storage.StorageFolder;
                return new StorageFolder(storageFolder);
            }

            return null;
        }
    }
}
#endif