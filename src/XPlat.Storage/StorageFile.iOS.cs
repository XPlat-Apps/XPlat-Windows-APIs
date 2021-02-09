#if __IOS__
namespace XPlat.Storage
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using XPlat.Storage.Extensions;
    using XPlat.Storage.FileProperties;
    using XPlat.Storage.Helpers;
    using File = System.IO.File;

    /// <summary>Represents a file. Provides information about the file and its contents, and ways to manipulate them.</summary>
    public sealed class StorageFile : IStorageFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StorageFile"/> class.
        /// </summary>
        /// <param name="path">
        /// The path to the file.
        /// </param>
        public StorageFile(string path)
        {
            this.Path = path;
        }

        /// <summary>Gets the date and time when the current item was created.</summary>
        public DateTime DateCreated => File.GetCreationTime(this.Path);

        /// <summary>Gets the name of the item including the file name extension if there is one.</summary>
        public string Name => System.IO.Path.GetFileName(this.Path);

        /// <summary>Gets the user-friendly name of the item.</summary>
        public string DisplayName => System.IO.Path.GetFileNameWithoutExtension(this.Path);

        /// <summary>Gets the full file-system path of the item, if the item has a path.</summary>
        public string Path { get; private set; }

        /// <summary>Gets a value indicating whether the item exists.</summary>
        public bool Exists => File.Exists(this.Path);

        /// <summary>Gets the attributes of a storage item.</summary>
        public FileAttributes Attributes => File.GetAttributes(this.Path).ToInternalFileAttributes();

        /// <summary>Gets the type (file name extension) of the file.</summary>
        public string FileType => System.IO.Path.GetExtension(this.Path);

        /// <summary>Gets the MIME type of the contents of the file.</summary>
        public string ContentType => MimeTypeHelper.GetMimeType(this.FileType);

        /// <summary>Gets an object that provides access to the content-related properties of the item.</summary>
        public IStorageItemContentProperties Properties { get; }

        /// <summary>
        /// Retrieves a <see cref="IStorageFile"/> by the given <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>The <see cref="IStorageFile"/>.</returns>
        /// <exception cref="T:System.ArgumentNullException">Thrown if <paramref name="path"/> is <see langword="null"/>.</exception>
        public static Task<IStorageFile> GetFileFromPathAsync(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            IStorageFile resultFile = new StorageFile(path);
            return Task.FromResult(resultFile);
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

            FileInfo fileInfo = new FileInfo(this.Path);
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

        /// <summary>Deletes the current item.</summary>
        /// <returns>No object or value is returned by this method when it completes.</returns>
        public Task DeleteAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot delete a file that does not exist.");
            }

            File.Delete(this.Path);

            return Task.CompletedTask;
        }

        /// <summary>Determines whether the current IStorageItem matches the specified StorageItemTypes value.</summary>
        /// <returns>True if the IStorageItem matches the specified value; otherwise false.</returns>
        /// <param name="type">The value to match against.</param>
        public bool IsOfType(StorageItemTypes type)
        {
            return type == StorageItemTypes.File;
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

            IBasicProperties props = new BasicProperties(this.Path);

            return Task.FromResult(props);
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

        /// <summary>
        /// Opens a stream over the current file for reading file contents.
        /// </summary>
        /// <returns>
        /// When this method completes, it returns the stream.
        /// </returns>
        public Task<Stream> OpenReadAsync()
        {
            return this.OpenAsync(FileAccessMode.Read);
        }

        /// <summary>Opens a stream over the file.</summary>
        /// <returns>When this method completes, it returns the stream.</returns>
        /// <param name="accessMode">The type of access to allow.</param>
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

        /// <summary>Creates a copy of the file in the specified folder.</summary>
        /// <returns>When this method completes, it returns a StorageFile that represents the copy.</returns>
        /// <param name="destinationFolder">The destination folder where the copy is created.</param>
        public Task<IStorageFile> CopyAsync(IStorageFolder destinationFolder)
        {
            return this.CopyAsync(destinationFolder, this.Name);
        }

        /// <summary>Creates a copy of the file in the specified folder, using the desired name.</summary>
        /// <returns>When this method completes, it returns a StorageFile that represents the copy.</returns>
        /// <param name="destinationFolder">The destination folder where the copy is created.</param>
        /// <param name="desiredNewName">The desired name of the copy. If there is an existing file in the destination folder that already has the specified desiredNewName, Windows generates a unique name for the copy.</param>
        public Task<IStorageFile> CopyAsync(IStorageFolder destinationFolder, string desiredNewName)
        {
            return this.CopyAsync(destinationFolder, desiredNewName, NameCollisionOption.FailIfExists);
        }

        /// <summary>Creates a copy of the file in the specified folder, using the desired name. This method also specifies what to do if an existing file in the specified folder has the same name.</summary>
        /// <returns>When this method completes, it returns a StorageFile that represents the copy.</returns>
        /// <param name="destinationFolder">The destination folder where the copy is created.</param>
        /// <param name="desiredNewName">The desired name of the copy. If there is an existing file in the destination folder that already has the specified desiredNewName, the specified NameCollisionOption determines how Windows responds to the conflict.</param>
        /// <param name="option">An enum value that determines how Windows responds if the desiredNewName is the same as the name of an existing file in the destination folder.</param>
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

            IStorageFile file = new StorageFile(newPath);
            return Task.FromResult(file);
        }

        /// <summary>Replaces the specified file with a copy of the current file.</summary>
        /// <returns>No object or value is returned when this method completes.</returns>
        /// <param name="fileToReplace">The file to replace.</param>
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

        /// <summary>Moves the current file to the specified folder.</summary>
        /// <returns>No object or value is returned by this method.</returns>
        /// <param name="destinationFolder">The destination folder where the file is moved. This destination folder must be a physical location. Otherwise, if the destination folder exists only in memory, like a file group, this method fails and throws an exception.</param>
        public Task MoveAsync(IStorageFolder destinationFolder)
        {
            return this.MoveAsync(destinationFolder, this.Name);
        }

        /// <summary>Moves the current file to the specified folder and renames the file according to the desired name.</summary>
        /// <returns>No object or value is returned by this method.</returns>
        /// <param name="destinationFolder">The destination folder where the file is moved. This destination folder must be a physical location. Otherwise, if the destination folder exists only in memory, like a file group, this method fails and throws an exception.</param>
        /// <param name="desiredNewName">The desired name of the file after it is moved. If there is an existing file in the destination folder that already has the specified desiredNewName, Windows generates a unique name for the file.</param>
        public Task MoveAsync(IStorageFolder destinationFolder, string desiredNewName)
        {
            return this.MoveAsync(destinationFolder, desiredNewName, NameCollisionOption.ReplaceExisting);
        }

        /// <summary>Moves the current file to the specified folder and renames the file according to the desired name. This method also specifies what to do if a file with the same name already exists in the specified folder.</summary>
        /// <returns>No object or value is returned by this method.</returns>
        /// <param name="destinationFolder">The destination folder where the file is moved. This destination folder must be a physical location. Otherwise, if the destination folder exists only in memory, like a file group, this method fails and throws an exception.</param>
        /// <param name="desiredNewName">The desired name of the file after it is moved. If there is an existing file in the destination folder that already has the specified desiredNewName, the specified NameCollisionOption determines how Windows responds to the conflict.</param>
        /// <param name="option">An enum value that determines how Windows responds if the desiredNewName is the same as the name of an existing file in the destination folder.</param>
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

        /// <summary>Moves the current file to the location of the specified file and replaces the specified file in that location.</summary>
        /// <returns>No object or value is returned by this method.</returns>
        /// <param name="fileToReplace">The file to replace.</param>
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

            string newPath = fileToReplace.Path;

            File.Delete(newPath);
            File.Move(this.Path, newPath);

            this.Path = newPath;

            return Task.CompletedTask;
        }

        /// <summary>
        /// Writes a string to the current file.
        /// </summary>
        /// <param name="text">
        /// The text to write out.
        /// </param>
        /// <returns>
        /// An object that is used to manage the asynchronous operation.
        /// </returns>
        public async Task WriteTextAsync(string text)
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot write to a file that does not exist.");
            }

            using (Stream stream = await this.OpenAsync(FileAccessMode.ReadWrite))
            {
                stream.SetLength(0);
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    await writer.WriteAsync(text);
                }
            }
        }

        /// <summary>
        /// Reads the current file as a string.
        /// </summary>
        /// <returns>
        /// When this method completes, it returns the file's content as a string.
        /// </returns>
        public async Task<string> ReadTextAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot read from a file that does not exist.");
            }

            string text;

            using (Stream stream = await this.OpenAsync(FileAccessMode.Read))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    text = await reader.ReadToEndAsync();
                }
            }

            return text;
        }

        /// <summary>
        /// Writes a byte array to the current file.
        /// </summary>
        /// <param name="bytes">
        /// The byte array to write out.
        /// </param>
        /// <returns>
        /// An object that is used to manage the asynchronous operation.
        /// </returns>
        public Task WriteBytesAsync(byte[] bytes)
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot write to a file that does not exist.");
            }

            File.WriteAllBytes(this.Path, bytes);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Reads the current file as a byte array.
        /// </summary>
        /// <returns>
        /// When this method completes, it returns the file's content as a byte array.
        /// </returns>
        public Task<byte[]> ReadBytesAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot read from a file that does not exist.");
            }

            return Task.FromResult(File.ReadAllBytes(this.Path));
        }
    }
}
#endif