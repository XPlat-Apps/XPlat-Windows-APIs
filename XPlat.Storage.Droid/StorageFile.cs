namespace XPlat.Storage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using XPlat.Storage.FileProperties;
    using XPlat.Threading.Tasks;

    using File = System.IO.File;

    /// <summary>
    /// Defines an application file.
    /// </summary>
    public sealed class StorageFile : IStorageFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StorageFile"/> class.
        /// </summary>
        /// <param name="parentFolder">
        /// The parent folder.
        /// </param>
        /// <param name="path">
        /// The path to the file.
        /// </param>
        internal StorageFile(IStorageFolder parentFolder, string path)
        {
            this.Parent = parentFolder;
            this.Path = path;
        }

        /// <inheritdoc />
        public DateTime DateCreated => File.GetCreationTime(this.Path);

        /// <inheritdoc />
        public string Name => System.IO.Path.GetFileName(this.Path);

        /// <inheritdoc />
        public string Path { get; private set; }

        /// <inheritdoc />
        public bool Exists => File.Exists(this.Path);

        /// <inheritdoc />
        public IStorageFolder Parent { get; private set; }

        /// <inheritdoc />
        public Task RenameAsync(string desiredName)
        {
            return this.RenameAsync(desiredName, NameCollisionOption.FailIfExists);
        }

        /// <inheritdoc />
        public Task RenameAsync(string desiredName, NameCollisionOption option)
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot rename a file that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(desiredName))
            {
                throw new ArgumentNullException(nameof(desiredName));
            }

            if (desiredName.Equals(this.Name, StringComparison.CurrentCultureIgnoreCase))
            {
                throw new ArgumentException("The desired new name is the same as the current name.");
            }

            var fileInfo = new FileInfo(this.Path);
            if (fileInfo.Directory == null)
            {
                throw new InvalidOperationException("This file cannot be renamed.");
            }

            string newPath = System.IO.Path.Combine(fileInfo.Directory.FullName, desiredName);

            switch (option)
            {
                case NameCollisionOption.GenerateUniqueName:
                    newPath = System.IO.Path.Combine(
                        fileInfo.Directory.FullName,
                        $"{desiredName}-{Guid.NewGuid()}");
                    fileInfo.MoveTo(newPath);
                    break;
                case NameCollisionOption.ReplaceExisting:
                    if (File.Exists(newPath))
                    {
                        File.Delete(newPath);
                    }

                    fileInfo.MoveTo(newPath);
                    break;
                default:
                    if (File.Exists(newPath))
                    {
                        throw new StorageItemCreationException(
                            desiredName,
                            "A file with the same name already exists.");
                    }

                    fileInfo.MoveTo(newPath);
                    break;
            }

            this.Path = newPath;

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task DeleteAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot delete a file that does not exist.");
            }

            File.Delete(this.Path);

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task<IDictionary<string, object>> GetPropertiesAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(
                    this.Name,
                    "Cannot get properties for a file that does not exist.");
            }

            IDictionary<string, object> props = new Dictionary<string, object>();

            // ToDo; find a library or implement metadata extraction from file.

            return Task.FromResult(props);
        }

        /// <inheritdoc />
        public bool IsOfType(StorageItemTypes type)
        {
            return type == StorageItemTypes.File;
        }

        /// <inheritdoc />
        public Task<IBasicProperties> GetBasicPropertiesAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(
                    this.Name,
                    "Cannot get properties for a folder that does not exist.");
            }

            IBasicProperties props = new BasicProperties(this.Path);

            return Task.FromResult(props);
        }

        /// <inheritdoc />
        public string FileType => System.IO.Path.GetExtension(this.Path);

        /// <inheritdoc />
        public Task<Stream> OpenAsync(FileAccessMode accessMode)
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot open a file that does not exist.");
            }

            Stream stream;

            switch (accessMode)
            {
                case FileAccessMode.Read:
                    stream = File.OpenRead(this.Path);
                    break;
                case FileAccessMode.ReadWrite:
                    stream = File.Open(this.Path, FileMode.Open, FileAccess.ReadWrite);
                    break;
                default:
                    throw new StorageFileIOException(this.Name, "The file could not be opened.");
            }

            return Task.FromResult(stream);
        }

        /// <inheritdoc />
        public Task<IStorageFile> CopyAsync(IStorageFolder destinationFolder)
        {
            return this.CopyAsync(destinationFolder, this.Name);
        }

        /// <inheritdoc />
        public Task<IStorageFile> CopyAsync(IStorageFolder destinationFolder, string desiredNewName)
        {
            return this.CopyAsync(destinationFolder, desiredNewName, NameCollisionOption.FailIfExists);
        }

        /// <inheritdoc />
        public Task<IStorageFile> CopyAsync(
            IStorageFolder destinationFolder,
            string desiredNewName,
            NameCollisionOption option)
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot copy a file that does not exist.");
            }

            if (destinationFolder == null)
            {
                throw new ArgumentNullException(nameof(destinationFolder));
            }

            if (!destinationFolder.Exists)
            {
                throw new StorageItemNotFoundException(
                    destinationFolder.Name,
                    "Cannot copy a file to a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(desiredNewName))
            {
                throw new ArgumentNullException(nameof(desiredNewName));
            }

            string newPath = System.IO.Path.Combine(destinationFolder.Path, desiredNewName);

            switch (option)
            {
                case NameCollisionOption.GenerateUniqueName:
                    newPath = System.IO.Path.Combine(
                        destinationFolder.Path,
                        $"{Guid.NewGuid()}-{desiredNewName}");

                    File.Copy(this.Path, newPath);
                    break;
                case NameCollisionOption.ReplaceExisting:
                    if (File.Exists(newPath))
                    {
                        File.Delete(newPath);
                    }

                    File.Copy(this.Path, newPath);
                    break;
                default:
                    if (File.Exists(newPath))
                    {
                        throw new StorageItemCreationException(
                            desiredNewName,
                            "A file with the same name already exists.");
                    }

                    File.Copy(this.Path, newPath);
                    break;
            }

            IStorageFile file = new StorageFile(destinationFolder, newPath);
            return Task.FromResult(file);
        }

        /// <inheritdoc />
        public Task CopyAndReplaceAsync(IStorageFile fileToReplace)
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot copy a file that does not exist.");
            }

            if (fileToReplace == null)
            {
                throw new ArgumentNullException(nameof(fileToReplace));
            }

            if (!fileToReplace.Exists)
            {
                throw new StorageItemNotFoundException(
                    fileToReplace.Name,
                    "Cannot copy to and replace a file that does not exist.");
            }

            File.Copy(this.Path, fileToReplace.Path, true);

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task MoveAsync(IStorageFolder destinationFolder)
        {
            return this.MoveAsync(destinationFolder, this.Name);
        }

        /// <inheritdoc />
        public Task MoveAsync(IStorageFolder destinationFolder, string desiredNewName)
        {
            return this.MoveAsync(destinationFolder, desiredNewName, NameCollisionOption.ReplaceExisting);
        }

        /// <inheritdoc />
        public Task MoveAsync(
            IStorageFolder destinationFolder,
            string desiredNewName,
            NameCollisionOption option)
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot move a file that does not exist.");
            }

            if (destinationFolder == null)
            {
                throw new ArgumentNullException(nameof(destinationFolder));
            }

            if (!destinationFolder.Exists)
            {
                throw new StorageItemNotFoundException(
                    destinationFolder.Name,
                    "Cannot move a file to a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(desiredNewName))
            {
                throw new ArgumentNullException(nameof(desiredNewName));
            }

            string newPath = System.IO.Path.Combine(destinationFolder.Path, desiredNewName);

            switch (option)
            {
                case NameCollisionOption.GenerateUniqueName:
                    newPath = System.IO.Path.Combine(
                        destinationFolder.Path,
                        $"{Guid.NewGuid()}-{desiredNewName}");

                    File.Move(this.Path, newPath);
                    break;
                case NameCollisionOption.ReplaceExisting:
                    if (File.Exists(newPath))
                    {
                        File.Delete(newPath);
                    }

                    File.Move(this.Path, newPath);
                    break;
                default:
                    if (File.Exists(newPath))
                    {
                        throw new StorageItemCreationException(
                            desiredNewName,
                            "A file with the same name already exists.");
                    }

                    File.Move(this.Path, newPath);
                    break;
            }

            this.Path = newPath;

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public Task MoveAndReplaceAsync(IStorageFile fileToReplace)
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot move a file that does not exist.");
            }

            if (fileToReplace == null)
            {
                throw new ArgumentNullException(nameof(fileToReplace));
            }

            if (!fileToReplace.Exists)
            {
                throw new StorageItemNotFoundException(
                    fileToReplace.Name,
                    "Cannot move to and replace a file that does not exist.");
            }

            var newPath = fileToReplace.Path;

            File.Delete(newPath);
            File.Move(this.Path, newPath);

            this.Path = newPath;
            this.Parent = fileToReplace.Parent;

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public async Task WriteTextAsync(string text)
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot write to a file that does not exist.");
            }

            using (var stream = await this.OpenAsync(FileAccessMode.ReadWrite))
            {
                stream.SetLength(0);
                using (var writer = new StreamWriter(stream))
                {
                    await writer.WriteAsync(text);
                }
            }
        }

        /// <inheritdoc />
        public async Task<string> ReadTextAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot read from a file that does not exist.");
            }

            string text;

            using (var stream = await this.OpenAsync(FileAccessMode.Read))
            {
                using (var reader = new StreamReader(stream))
                {
                    text = await reader.ReadToEndAsync();
                }
            }

            return text;
        }

        /// <summary>
        /// Gets an IStorageFile object to represent the file at the specified path.
        /// </summary>
        /// <param name="path">
        /// The path of the file to get an IStorageFile to represent. 
        /// </param>
        /// <returns>
        /// When this method completes, it returns the file as an IStorageFile.
        /// </returns>
        public static async Task<IStorageFile> GetFileFromPathAsync(string path)
        {
            IStorageFolder resultFileParentFolder;

            if (File.Exists(path))
            {
                var fileInfo = new FileInfo(path);
                if (fileInfo.Directory != null && fileInfo.Directory.Exists)
                {
                    resultFileParentFolder = await StorageFolder.GetFolderFromPathAsync(fileInfo.Directory.FullName);
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

            var resultFile = new StorageFile(resultFileParentFolder, path);

            return resultFile;
        }
    }
}