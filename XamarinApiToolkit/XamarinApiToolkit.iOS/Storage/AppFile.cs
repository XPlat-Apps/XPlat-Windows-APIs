namespace XamarinApiToolkit.Storage
{
    using System.IO;
    using System.Threading.Tasks;

    using WinUX.Threading.Tasks;

    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines an application file.
    /// </summary>
    public sealed class AppFile : IAppFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppFile"/> class.
        /// </summary>
        /// <param name="parentFolder">
        /// The parent folder.
        /// </param>
        /// <param name="path">
        /// The path to the file.
        /// </param>
        internal AppFile(IAppFolder parentFolder, string path)
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
                return File.GetCreationTime(this.Path);
            }
        }

        /// <summary>
        /// Gets the name of the item.
        /// </summary>
        public string Name
        {
            get
            {
                return System.IO.Path.GetFileName(this.Path);
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
                return File.Exists(this.Path);
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
                throw new AppStorageItemNotFoundException(this.Name, "Cannot rename a file that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(desiredNewName))
            {
                throw new ArgumentNullException(nameof(desiredNewName));
            }

            if (desiredNewName.Equals(this.Name, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new ArgumentException("The desired new name is the same as the current name.");
            }

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            var fileInfo = new FileInfo(this.Path);
            if (fileInfo.Directory == null)
            {
                throw new InvalidOperationException("This file cannot be renamed.");
            }

            string newPath = System.IO.Path.Combine(fileInfo.Directory.FullName, desiredNewName);

            switch (option)
            {
                case FileStoreNameCollisionOption.GenerateUniqueName:
                    newPath = System.IO.Path.Combine(
                        fileInfo.Directory.FullName,
                        string.Format("{0}-{1}", desiredNewName, Guid.NewGuid()));
                    fileInfo.MoveTo(newPath);
                    break;
                case FileStoreNameCollisionOption.ReplaceExisting:
                    if (File.Exists(newPath))
                    {
                        File.Delete(newPath);
                    }

                    fileInfo.MoveTo(newPath);
                    break;
                default:
                    if (File.Exists(newPath))
                    {
                        throw new AppStorageItemCreationException(
                                  desiredNewName,
                                  "A file with the same name already exists.");
                    }

                    fileInfo.MoveTo(newPath);
                    break;
            }

            this.Path = newPath;
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
                throw new AppStorageItemNotFoundException(this.Name, "Cannot delete a file that does not exist.");
            }

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            File.Delete(this.Path);
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
                          "Cannot get properties for a file that does not exist.");
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
            return type == FileStoreItemTypes.File;
        }

        /// <summary>
        /// Gets the file type.
        /// </summary>
        public string FileType
        {
            get
            {
                return System.IO.Path.GetExtension(this.Path);
            }
        }

        /// <summary>
        /// Opens a stream containing the file's contents.
        /// </summary>
        /// <param name="accessMode">
        /// The access mode for the file.
        /// </param>
        /// <returns>
        /// Returns the file stream.
        /// </returns>
        public async Task<Stream> OpenAsync(FileAccessOption accessMode)
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(this.Name, "Cannot open a file that does not exist.");
            }

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            switch (accessMode)
            {
                case FileAccessOption.Read:
                    return File.OpenRead(this.Path);
                case FileAccessOption.ReadWrite:
                    return File.Open(this.Path, FileMode.Open, FileAccess.ReadWrite);
                default:
                    throw new AppFileIOException(this.Name, "The file could not be opened.");
            }
        }

        /// <summary>
        /// Copies the file to a specified destination folder.
        /// </summary>
        /// <param name="destinationFolder">
        /// The destination folder.
        /// </param>
        /// <returns>
        /// Returns the copied file.
        /// </returns>
        public Task<IAppFile> CopyAsync(IAppFolder destinationFolder)
        {
            return this.CopyAsync(destinationFolder, this.Name);
        }

        /// <summary>
        /// Copies the file to a specified destination folder with a specified new name.
        /// </summary>
        /// <param name="destinationFolder">
        /// The destination folder.
        /// </param>
        /// <param name="desiredNewName">
        /// The desired new name.
        /// </param>
        /// <returns>
        /// Returns the copied file.
        /// </returns>
        public Task<IAppFile> CopyAsync(IAppFolder destinationFolder, string desiredNewName)
        {
            return this.CopyAsync(destinationFolder, desiredNewName, FileStoreNameCollisionOption.FailIfExists);
        }

        /// <summary>
        /// Copies the file to a specified destination folder with a specified new name.
        /// </summary>
        /// <param name="destinationFolder">
        /// The destination folder.
        /// </param>
        /// <param name="desiredNewName">
        /// The desired new name.
        /// </param>
        /// <param name="option">
        /// The file name collision option.
        /// </param>
        /// <returns>
        /// Returns the copied file.
        /// </returns>
        public async Task<IAppFile> CopyAsync(
            IAppFolder destinationFolder,
            string desiredNewName,
            FileStoreNameCollisionOption option)
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(this.Name, "Cannot copy a file that does not exist.");
            }

            if (destinationFolder == null)
            {
                throw new ArgumentNullException(nameof(destinationFolder));
            }

            if (!destinationFolder.Exists)
            {
                throw new AppStorageItemNotFoundException(
                          destinationFolder.Name,
                          "Cannot copy a file to a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(desiredNewName))
            {
                throw new ArgumentNullException(nameof(desiredNewName));
            }

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            string newPath = System.IO.Path.Combine(destinationFolder.Path, desiredNewName);

            switch (option)
            {
                case FileStoreNameCollisionOption.GenerateUniqueName:
                    newPath = System.IO.Path.Combine(
                        destinationFolder.Path,
                        string.Format("{0}-{1}", Guid.NewGuid(), desiredNewName));

                    File.Copy(this.Path, newPath);
                    break;
                case FileStoreNameCollisionOption.ReplaceExisting:
                    if (File.Exists(newPath))
                    {
                        File.Delete(newPath);
                    }

                    File.Copy(this.Path, newPath);
                    break;
                default:
                    if (File.Exists(newPath))
                    {
                        throw new AppStorageItemCreationException(
                                  desiredNewName,
                                  "A file with the same name already exists.");
                    }

                    File.Copy(this.Path, newPath);
                    break;
            }

            return new AppFile(destinationFolder, newPath);
        }

        /// <summary>
        /// Copies and replaces the file to the specified file.
        /// </summary>
        /// <param name="fileToReplace">
        /// The file to replace.
        /// </param>
        /// <returns>
        /// Returns a task.
        /// </returns>
        public async Task CopyAndReplaceAsync(IAppFile fileToReplace)
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(this.Name, "Cannot copy a file that does not exist.");
            }

            if (fileToReplace == null)
            {
                throw new ArgumentNullException(nameof(fileToReplace));
            }

            if (!fileToReplace.Exists)
            {
                throw new AppStorageItemNotFoundException(
                          fileToReplace.Name,
                          "Cannot copy to and replace a file that does not exist.");
            }

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            File.Copy(this.Path, fileToReplace.Path, true);
        }

        /// <summary>
        /// Moves the file to the specified destination folder.
        /// </summary>
        /// <param name="destinationFolder">
        /// The destination folder.
        /// </param>
        /// <returns>
        /// Returns a task.
        /// </returns>
        public Task MoveAsync(IAppFolder destinationFolder)
        {
            return this.MoveAsync(destinationFolder, this.Name);
        }

        /// <summary>
        /// Moves the file to the specified destination folder with a specified new name.
        /// </summary>
        /// <param name="destinationFolder">
        /// The destination folder.
        /// </param>
        /// <param name="desiredNewName">
        /// The desired new name.
        /// </param>
        /// <returns>
        /// Returns a task.
        /// </returns>
        public Task MoveAsync(IAppFolder destinationFolder, string desiredNewName)
        {
            return this.MoveAsync(destinationFolder, desiredNewName, FileStoreNameCollisionOption.ReplaceExisting);
        }

        /// <summary>
        /// Moves the file to the specified destination folder with a specified new name.
        /// </summary>
        /// <param name="destinationFolder">
        /// The destination folder.
        /// </param>
        /// <param name="desiredNewName">
        /// The desired new name.
        /// </param>
        /// <param name="option">
        /// The file name collision option.
        /// </param>
        /// <returns>
        /// Returns a task.
        /// </returns>
        public async Task MoveAsync(
            IAppFolder destinationFolder,
            string desiredNewName,
            FileStoreNameCollisionOption option)
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(this.Name, "Cannot move a file that does not exist.");
            }

            if (destinationFolder == null)
            {
                throw new ArgumentNullException(nameof(destinationFolder));
            }

            if (!destinationFolder.Exists)
            {
                throw new AppStorageItemNotFoundException(
                          destinationFolder.Name,
                          "Cannot move a file to a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(desiredNewName))
            {
                throw new ArgumentNullException(nameof(desiredNewName));
            }

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            string newPath = System.IO.Path.Combine(destinationFolder.Path, desiredNewName);

            switch (option)
            {
                case FileStoreNameCollisionOption.GenerateUniqueName:
                    newPath = System.IO.Path.Combine(
                        destinationFolder.Path,
                        string.Format("{0}-{1}", Guid.NewGuid(), desiredNewName));

                    File.Move(this.Path, newPath);
                    break;
                case FileStoreNameCollisionOption.ReplaceExisting:
                    if (File.Exists(newPath))
                    {
                        File.Delete(newPath);
                    }

                    File.Move(this.Path, newPath);
                    break;
                default:
                    if (File.Exists(newPath))
                    {
                        throw new AppStorageItemCreationException(
                                  desiredNewName,
                                  "A file with the same name already exists.");
                    }

                    File.Move(this.Path, newPath);
                    break;
            }

            this.Path = newPath;
        }

        /// <summary>
        /// Moves and replaces the specified file.
        /// </summary>
        /// <param name="fileToReplace">
        /// The file to replace.
        /// </param>
        /// <returns>
        /// Returns a task.
        /// </returns>
        public async Task MoveAndReplaceAsync(IAppFile fileToReplace)
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(this.Name, "Cannot move a file that does not exist.");
            }

            if (fileToReplace == null)
            {
                throw new ArgumentNullException(nameof(fileToReplace));
            }

            if (!fileToReplace.Exists)
            {
                throw new AppStorageItemNotFoundException(
                          fileToReplace.Name,
                          "Cannot move to and replace a file that does not exist.");
            }

            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            var newPath = fileToReplace.Path;

            File.Delete(newPath);
            File.Move(this.Path, newPath);

            this.Path = newPath;
            this.Parent = fileToReplace.Parent;
        }

        /// <summary>
        /// Writes a string to the content of the file.
        /// </summary>
        /// <param name="text">
        /// The text to store.
        /// </param>
        /// <returns>
        /// Returns a task.
        /// </returns>
        public async Task WriteTextAsync(string text)
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(this.Name, "Cannot write to a file that does not exist.");
            }

            using (var stream = await this.OpenAsync(FileAccessOption.ReadWrite))
            {
                stream.SetLength(0);
                using (var writer = new StreamWriter(stream))
                {
                    await writer.WriteAsync(text);
                }
            }
        }

        /// <summary>
        /// Reads the content of the file as a string.
        /// </summary>
        /// <returns>
        /// Returns the content.
        /// </returns>
        public async Task<string> ReadTextAsync()
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(this.Name, "Cannot read from a file that does not exist.");
            }

            string text;

            using (var stream = await this.OpenAsync(FileAccessOption.Read))
            {
                using (var reader = new StreamReader(stream))
                {
                    text = await reader.ReadToEndAsync();
                }
            }

            return text;
        }

        /// <summary>
        /// Gets an AppFile object to represent the file at the specified path
        /// </summary>
        /// <param name="path">
        /// The path of the file to get a StorageFile to represent.
        /// </param>
        /// <returns>
        /// Returns an <see cref="AppFile"/> for the path.
        /// </returns>
        public static async Task<AppFile> GetFileFromPathAsync(string path)
        {
            await TaskSchedulerAwaiter.NewTaskSchedulerAwaiter();

            AppFolder resultFileParentFolder;

            if (File.Exists(path))
            {
                var fileInfo = new FileInfo(path);
                if (fileInfo.Directory != null && fileInfo.Directory.Exists)
                {
                    resultFileParentFolder = await AppFolder.GetFolderFromPathAsync(fileInfo.Directory.FullName);
                }
                else
                {
                    resultFileParentFolder = null;
                }
            }
            else
            {
                return null;
            }

            var resultFile = new AppFile(resultFileParentFolder, path);

            return resultFile;
        }
    }
}