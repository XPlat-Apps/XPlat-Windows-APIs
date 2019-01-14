#if __ANDROID__
namespace XPlat.Storage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using XPlat.Extensions;
    using XPlat.Storage.Extensions;
    using XPlat.Storage.FileProperties;

    /// <summary>Manipulates folders and their contents, and provides information about them.</summary>
    public sealed class StorageFolder : IStorageFolder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StorageFolder"/> class.
        /// </summary>
        /// <param name="path">
        /// The path to the folder.
        /// </param>
        public StorageFolder(string path)
        {
            this.Path = path;
        }

        /// <summary>Gets the date and time when the current item was created.</summary>
        public DateTime DateCreated => Directory.GetCreationTime(this.Path);

        /// <summary>Gets the name of the item including the file name extension if there is one.</summary>
        public string Name
        {
            get
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(this.Path);
                return directoryInfo.Name;
            }
        }

        /// <summary>Gets the user-friendly name of the item.</summary>
        public string DisplayName => this.Name;

        /// <summary>Gets an object that provides access to the content-related properties of the item.</summary>
        public IStorageItemContentProperties Properties => new StorageItemContentProperties(new WeakReference(this));

        /// <summary>Gets the full file-system path of the item, if the item has a path.</summary>
        public string Path { get; private set; }

        /// <summary>Gets a value indicating whether the item exists.</summary>
        public bool Exists => Directory.Exists(this.Path);

        /// <summary>Gets the attributes of a storage item.</summary>
        public FileAttributes Attributes => File.GetAttributes(this.Path).ToInternalFileAttributes();

        /// <summary>
        /// Gets the folder that has the specified absolute path in the file system.
        /// </summary>
        /// <param name="path">
        /// The absolute path in the file system (not the Uri) of the folder to get.
        /// </param>
        /// <returns>
        /// When this method completes successfully, it returns an IAppFolder that represents the specified folder.
        /// </returns>
        public static Task<IStorageFolder> GetFolderFromPathAsync(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            IStorageFolder folder = new StorageFolder(path);
            return Task.FromResult(folder);
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
        public Task RenameAsync(string desiredName, NameCollisionOption option)
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot rename a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(desiredName))
            {
                throw new ArgumentNullException(nameof(desiredName));
            }

            if (desiredName.Equals(this.Name, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new ArgumentException("The desired new name is the same as the current name.");
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(this.Path);
            if (directoryInfo.Parent == null)
            {
                throw new InvalidOperationException("This folder cannot be renamed.");
            }

            string newPath = System.IO.Path.Combine(directoryInfo.Parent.FullName, desiredName);

            switch (option)
            {
                case NameCollisionOption.GenerateUniqueName:
                    newPath = System.IO.Path.Combine(
                        directoryInfo.Parent.FullName,
                        $"{desiredName}-{Guid.NewGuid()}");
                    directoryInfo.MoveTo(newPath);
                    break;
                case NameCollisionOption.ReplaceExisting:
                    if (Directory.Exists(newPath))
                    {
                        Directory.Delete(newPath, true);
                    }

                    directoryInfo.MoveTo(newPath);
                    break;
                default:
                    if (Directory.Exists(newPath))
                    {
                        throw new StorageItemCreationException(
                            desiredName,
                            "A folder with the same name already exists.");
                    }

                    directoryInfo.MoveTo(newPath);
                    break;
            }

            this.Path = newPath;

            return Task.CompletedTask;
        }

        /// <summary>Deletes the current item.</summary>
        /// <returns>No object or value is returned by this method when it completes.</returns>
        public Task DeleteAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot delete a folder that does not exist.");
            }

            Directory.Delete(this.Path, true);

            return Task.CompletedTask;
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
        public Task<IBasicProperties> GetBasicPropertiesAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(
                    this.Name,
                    "Cannot get properties for a folder that does not exist.");
            }

            IBasicProperties properties = new BasicProperties(this.Path, true);
            return Task.FromResult(properties);
        }

        /// <summary>Gets the parent folder of the current storage item.</summary>
        /// <returns>When this method completes, it returns the parent folder as a StorageFolder.</returns>
        public Task<IStorageFolder> GetParentAsync()
        {
            IStorageFolder result = default(IStorageFolder);
            DirectoryInfo parent = Directory.GetParent(this.Path);
            if (parent != null)
            {
                result = new StorageFolder(parent.FullName);
            }

            return Task.FromResult(result);
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
        public Task<IStorageFile> CreateFileAsync(string desiredName, CreationCollisionOption options)
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

            string filePath = System.IO.Path.Combine(this.Path, desiredName);
            if (File.Exists(filePath))
            {
                switch (options)
                {
                    case CreationCollisionOption.GenerateUniqueName:
                        desiredName = $"{Guid.NewGuid()}-{desiredName}";
                        filePath = System.IO.Path.Combine(this.Path, desiredName);
                        CreateFile(filePath);
                        break;
                    case CreationCollisionOption.ReplaceExisting:
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                        CreateFile(filePath);
                        break;
                    case CreationCollisionOption.FailIfExists:
                        if (File.Exists(filePath))
                        {
                            throw new StorageItemCreationException(
                                desiredName,
                                "A file with the same name already exists.");
                        }
                        CreateFile(filePath);
                        break;
                }
            }
            else
            {
                CreateFile(filePath);
            }

            IStorageFile file = new StorageFile(filePath);

            return Task.FromResult(file);
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
        public Task<IStorageFolder> CreateFolderAsync(string desiredName, CreationCollisionOption options)
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

            string folderPath = System.IO.Path.Combine(this.Path, desiredName);
            if (Directory.Exists(folderPath))
            {
                switch (options)
                {
                    case CreationCollisionOption.GenerateUniqueName:
                        desiredName = $"{desiredName}-{Guid.NewGuid()}";
                        folderPath = System.IO.Path.Combine(this.Path, desiredName);
                        CreateFolder(folderPath);
                        break;
                    case CreationCollisionOption.ReplaceExisting:
                        if (Directory.Exists(folderPath))
                        {
                            Directory.Delete(folderPath);
                        }
                        CreateFolder(folderPath);
                        break;
                    case CreationCollisionOption.FailIfExists:
                        if (Directory.Exists(folderPath))
                        {
                            throw new StorageItemCreationException(
                                desiredName,
                                "A folder with the same name already exists.");
                        }
                        CreateFolder(folderPath);
                        break;
                }
            }
            else
            {
                CreateFolder(folderPath);
            }

            IStorageFolder folder = new StorageFolder(folderPath);
            return Task.FromResult(folder);
        }

        /// <summary>Gets the specified file from the current folder.</summary>
        /// <returns>When this method completes successfully, it returns a StorageFile that represents the file.</returns>
        /// <param name="name">The name (or path relative to the current folder) of the file to retrieve.</param>
        public Task<IStorageFile> GetFileAsync(string name)
        {
            return this.GetFileAsync(name, false);
        }

        /// <summary>
        /// Gets the specified file from the current folder.
        /// </summary>
        /// <param name="name">
        /// The name of the file to retrieve. If the file does not exist in the current folder, the createIfNotExists flag will allow the API to create the file if set to true.
        /// </param>
        /// <param name="createIfNotExists">
        /// A value indicating whether to create the file if it does not exist.
        /// </param>
        /// <returns>
        /// When this method completes successfully, it returns an IStorageFile that represents the file.
        /// </returns>
        public Task<IStorageFile> GetFileAsync(string name, bool createIfNotExists)
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

            string filePath = System.IO.Path.Combine(this.Path, name);
            if (!File.Exists(filePath))
            {
                if (createIfNotExists)
                {
                    return this.CreateFileAsync(name);
                }

                throw new StorageItemNotFoundException(name, "The file could not be found in the folder.");
            }

            IStorageFile file = new StorageFile(filePath);
            return Task.FromResult(file);
        }

        /// <summary>Gets the specified folder from the current folder.</summary>
        /// <returns>When this method completes successfully, it returns a StorageFolder that represents the child folder.</returns>
        /// <param name="name">The name of the child folder to retrieve.</param>
        public Task<IStorageFolder> GetFolderAsync(string name)
        {
            return this.GetFolderAsync(name, false);
        }

        /// <summary>
        /// Gets the specified folder from the current folder.
        /// </summary>
        /// <param name="name">
        /// The name of the child folder to retrieve. If the child folder does not exist in the current folder, the createIfNotExists flag will allow the API to create the folder if set to true.
        /// </param>
        /// <param name="createIfNotExists">
        /// A value indicating whether to create the child folder if it does not exist.
        /// </param>
        /// <returns>
        /// When this method completes successfully, it returns an IStorageFolder that represents the child folder.
        /// </returns>
        public Task<IStorageFolder> GetFolderAsync(string name, bool createIfNotExists)
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

            string folderPath = System.IO.Path.Combine(this.Path, name);
            if (!Directory.Exists(folderPath))
            {
                if (createIfNotExists)
                {
                    return this.CreateFolderAsync(name);
                }

                throw new StorageItemNotFoundException(name, "The folder could not be found in the folder.");
            }

            IStorageFolder folder = new StorageFolder(folderPath);
            return Task.FromResult(folder);
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

            IStorageItem storageItem = null;

            try
            {
                storageItem = await this.GetFileAsync(name);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            try
            {
                storageItem = await this.GetFolderAsync(name);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            if (storageItem == null || !storageItem.Exists)
            {
                throw new StorageItemNotFoundException(name, "The item could not be found in the folder.");
            }

            return storageItem;
        }

        /// <summary>Gets the files from the current folder.</summary>
        /// <returns>When this method completes successfully, it returns a list of the files (type IVectorView) in the folder. Each file in the list is represented by a StorageFile object.</returns>
        public Task<IReadOnlyList<IStorageFile>> GetFilesAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(
                    this.Name,
                    "Cannot get files from a folder that does not exist.");
            }

            IReadOnlyList<IStorageFile> files = Directory.GetFiles(this.Path)
                .Select(filePath => new StorageFile(filePath))
                .ToList();

            return Task.FromResult(files);
        }

        /// <summary>Gets the folders in the current folder.</summary>
        /// <returns>When this method completes successfully, it returns a list of the files (type IVectorView). Each folder in the list is represented by a StorageFolder.</returns>
        public Task<IReadOnlyList<IStorageFolder>> GetFoldersAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(
                    this.Name,
                    "Cannot get folders from a folder that does not exist.");
            }

            IReadOnlyList<IStorageFolder> folders = Directory.GetDirectories(this.Path)
                .Select(folderPath => new StorageFolder(folderPath))
                .ToList();

            return Task.FromResult(folders);
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

            IReadOnlyList<IStorageFile> files = await this.GetFilesAsync();
            IReadOnlyList<IStorageFolder> folders = await this.GetFoldersAsync();

            List<IStorageItem> items = new List<IStorageItem>();
            items.AddRange(files);
            items.AddRange(folders);

            return items;
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

            IReadOnlyList<IStorageItem> allItems = await this.GetItemsAsync();

            return allItems.Take(startIndex, maxItemsToRetrieve).ToList();
        }

        /// <summary>Try to get a single file or sub-folder from the current folder by using the name of the item.</summary>
        /// <returns>When this method completes successfully, it returns the file or folder (type IStorageItem).</returns>
        /// <param name="name">The name (or path relative to the current folder) of the file or sub-folder to try to retrieve.</param>
        public async Task<IStorageItem> TryGetItemAsync(string name)
        {
            IStorageItem item = null;

            try
            {
                item = await this.GetItemAsync(name);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return item;
        }

        private static void CreateFile(string filePath)
        {
            using (File.Create(filePath))
            {
            }
        }

        private static void CreateFolder(string folderPath)
        {
            Directory.CreateDirectory(folderPath);
        }
    }
}
#endif