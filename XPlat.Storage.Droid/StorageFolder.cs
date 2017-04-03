namespace XPlat.Storage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using XPlat.Threading.Tasks;

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
        /// <param name="path">
        /// The path to the folder.
        /// </param>
        internal StorageFolder(IStorageFolder parentFolder, string path)
        {
            this.Parent = parentFolder;
            this.Path = path;
        }

        /// <inheritdoc />
        public DateTime DateCreated => Directory.GetCreationTime(this.Path);

        /// <inheritdoc />
        public string Name => System.IO.Path.GetDirectoryName(this.Path);

        /// <inheritdoc />
        public string Path { get; private set; }

        /// <inheritdoc />
        public bool Exists => Directory.Exists(this.Path);

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

            if (desiredName.Equals(this.Name, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new ArgumentException("The desired new name is the same as the current name.");
            }

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            var directoryInfo = new DirectoryInfo(this.Path);
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
                        string.Format("{0}-{1}", desiredName, Guid.NewGuid()));
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
        }

        /// <inheritdoc />
        public async Task DeleteAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot delete a folder that does not exist.");
            }

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            Directory.Delete(this.Path, true);
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

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            // ToDo, current not implemented. 
            return new Dictionary<string, object>();
        }

        /// <inheritdoc />
        public bool IsOfType(StorageItemTypes type)
        {
            return type == StorageItemTypes.Folder;
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

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            var filePath = System.IO.Path.Combine(this.Path, desiredName);
            if (File.Exists(filePath))
            {
                switch (options)
                {
                    case CreationCollisionOption.GenerateUniqueName:
                        desiredName = string.Format("{0}-{1}", Guid.NewGuid(), desiredName);
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

            return new StorageFile(this, filePath);
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

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            var folderPath = System.IO.Path.Combine(this.Path, desiredName);
            if (Directory.Exists(folderPath))
            {
                switch (options)
                {
                    case CreationCollisionOption.GenerateUniqueName:
                        desiredName = string.Format("{0}-{1}", desiredName, Guid.NewGuid());
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

            return new StorageFolder(this, folderPath);
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

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            var filePath = System.IO.Path.Combine(this.Path, name);
            if (!File.Exists(filePath))
            {
                if (createIfNotExists)
                {
                    return await this.CreateFileAsync(name);
                }

                throw new StorageItemNotFoundException(name, "The file could not be found in the folder.");
            }

            return new StorageFile(this, filePath);
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

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            var folderPath = System.IO.Path.Combine(this.Path, name);
            if (!Directory.Exists(folderPath))
            {
                if (createIfNotExists)
                {
                    return await this.CreateFolderAsync(name);
                }

                throw new StorageItemNotFoundException(name, "The folder could not be found in the folder.");
            }

            return new StorageFolder(this, folderPath);
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

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            IStorageItem storageItem = null;

            try
            {
                storageItem = await this.GetFileAsync(name);
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.Message);
#endif
            }

            try
            {
                storageItem = await this.GetFolderAsync(name);
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.Message);
#endif
            }

            if (storageItem == null || !storageItem.Exists)
            {
                throw new StorageItemNotFoundException(name, "The item could not be found in the folder.");
            }

            return storageItem;
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

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            return Directory.GetFiles(this.Path).Select(filePath => new StorageFile(this, filePath)).ToList();
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

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            return Directory.GetDirectories(this.Path).Select(folderPath => new StorageFolder(this, folderPath)).ToList();
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

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            var files = await this.GetFilesAsync();
            var folders = await this.GetFoldersAsync();

            var items = new List<IStorageItem>();
            items.AddRange(files);
            items.AddRange(folders);

            return items;
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
        public static async Task<IStorageFolder> GetFolderFromPathAsync(string path)
        {
            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            IStorageFolder resultParentFolder;

            if (Directory.Exists(path))
            {
                var directoryInfo = new DirectoryInfo(path);
                if (directoryInfo.Parent != null && directoryInfo.Parent.Exists)
                {
                    resultParentFolder = await GetFolderFromPathAsync(directoryInfo.Parent.FullName);
                }
                else
                {
                    resultParentFolder = null;
                }
            }
            else
            {
                return null;
            }

            var resultFolder = new StorageFolder(resultParentFolder, path);

            return resultFolder;
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