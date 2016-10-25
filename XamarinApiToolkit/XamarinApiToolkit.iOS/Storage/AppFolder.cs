namespace XamarinApiToolkit.Storage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using WinUX.Threading.Tasks;

    public sealed class AppFolder : IAppFolder
    {
        public AppFolder(IAppFolder parentFolder, string path)
        {
            this.Parent = parentFolder;
            this.Path = path;
        }

        /// <summary>
        /// Gets the date the item was created.
        /// </summary>
        public DateTime DateCreated
        {
            get
            {
                return Directory.GetCreationTime(this.Path);
            }
        }

        /// <summary>
        /// Gets the name of the item.
        /// </summary>
        public string Name
        {
            get
            {
                return System.IO.Path.GetDirectoryName(this.Path);
            }
        }

        /// <summary>
        /// Gets the full path to the item.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the item exists.
        /// </summary>
        public bool Exists
        {
            get
            {
                return Directory.Exists(this.Path);
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
        public Task RenameAsync(string desiredNewName, FileStoreNameCollisionOption option)
        {
            throw new NotImplementedException();
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

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            Directory.Delete(this.Path, true);
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

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            // ToDo, current not implemented. 
            return new Dictionary<string, object>();
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

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            var filePath = System.IO.Path.Combine(this.Path, desiredName);
            if (File.Exists(filePath))
            {
                switch (options)
                {
                    case FileStoreCreationOption.GenerateUniqueName:
                        desiredName = string.Format("{0}-{1}", Guid.NewGuid(), desiredName);
                        filePath = System.IO.Path.Combine(this.Path, desiredName);
                        CreateFile(filePath);
                        break;
                    case FileStoreCreationOption.ReplaceExisting:
                        File.Delete(filePath);
                        CreateFile(filePath);
                        break;
                    case FileStoreCreationOption.FailIfExists:
                        throw new AppStorageItemCreationException(
                                  desiredName,
                                  "A file with the same name already exists.");
                }
            }
            else
            {
                CreateFile(filePath);
            }

            return new AppFile(this, filePath);
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

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            var folderPath = System.IO.Path.Combine(this.Path, desiredName);
            if (Directory.Exists(folderPath))
            {
                switch (options)
                {
                    case FileStoreCreationOption.GenerateUniqueName:
                        desiredName = string.Format("{0}-{1}", Guid.NewGuid(), desiredName);
                        folderPath = System.IO.Path.Combine(this.Path, desiredName);
                        CreateFolder(folderPath);
                        break;
                    case FileStoreCreationOption.ReplaceExisting:
                        Directory.Delete(folderPath);
                        CreateFolder(folderPath);
                        break;
                    case FileStoreCreationOption.FailIfExists:
                        throw new AppStorageItemCreationException(
                                  desiredName,
                                  "A folder with the same name already exists.");
                }
            }
            else
            {
                CreateFolder(folderPath);
            }

            return new AppFolder(this, folderPath);
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

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            var filePath = System.IO.Path.Combine(this.Path, name);
            if (!File.Exists(filePath))
            {
                if (createIfNotExists)
                {
                    return await this.CreateFileAsync(name);
                }

                throw new AppStorageItemNotFoundException(name, "The file could not be found in the folder.");
            }

            return new AppFile(this, filePath);
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

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            var folderPath = System.IO.Path.Combine(this.Path, name);
            if (!Directory.Exists(folderPath))
            {
                if (createIfNotExists)
                {
                    return await this.CreateFolderAsync(name);
                }

                throw new AppStorageItemNotFoundException(name, "The folder could not be found in the folder.");
            }

            return new AppFolder(this, folderPath);
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
