#if WINDOWS_UWP
namespace XPlat.Storage
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices.WindowsRuntime;
    using System.Threading.Tasks;
    using Windows.Storage;
    using Windows.Storage.Streams;
    using XPlat.Storage.Extensions;
    using XPlat.Storage.FileProperties;

    /// <summary>Represents a file. Provides information about the file and its contents, and ways to manipulate them.</summary>
    public sealed class StorageFile : IStorageFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StorageFile"/> class.
        /// </summary>
        /// <param name="file">
        /// The associated <see cref="StorageFile"/>.
        /// </param>
        public StorageFile(Windows.Storage.StorageFile file)
        {
            this.Originator = file ?? throw new ArgumentNullException(nameof(file));
        }

        /// <summary>Gets the instance of the <see cref="Windows.Storage.StorageFile"/> object associated with this file.</summary>
        public Windows.Storage.StorageFile Originator { get; }

        /// <summary>Gets the date and time when the current item was created.</summary>
        public DateTime DateCreated => this.Originator.DateCreated.DateTime;

        /// <summary>Gets the name of the item including the file name extension if there is one.</summary>
        public string Name => this.Originator.Name;

        /// <summary>Gets the user-friendly name of the item.</summary>
        public string DisplayName => this.Originator.DisplayName;

        /// <summary>Gets the full file-system path of the item, if the item has a path.</summary>
        public string Path => this.Originator.Path;

        /// <summary>Gets a value indicating whether the item exists.</summary>
        public bool Exists => this.Originator != null;

        /// <summary>Gets the attributes of a storage item.</summary>
        public FileAttributes Attributes => this.Originator.Attributes.ToInternalFileAttributes();

        /// <summary>Gets the type (file name extension) of the file.</summary>
        public string FileType => this.Originator.FileType;

        /// <summary>Gets the MIME type of the contents of the file.</summary>
        public string ContentType => this.Originator.ContentType;

        /// <summary>Gets an object that provides access to the content-related properties of the item.</summary>
        public IStorageItemContentProperties Properties => new StorageItemContentProperties(new WeakReference(this));

        public static implicit operator StorageFile(Windows.Storage.StorageFile file)
        {
            return new StorageFile(file);
        }

        public static async Task<IStorageFile> GetFileFromPathAsync(string path)
        {
            Windows.Storage.StorageFile pathFile;

            try
            {
                pathFile = await Windows.Storage.StorageFile.GetFileFromPathAsync(path);
            }
            catch (Exception)
            {
                pathFile = null;
            }

            if (pathFile == null)
            {
                return null;
            }

            StorageFile resultFile = new StorageFile(pathFile);

            return resultFile;
        }

        public static async Task<IStorageFile> GetFileFromApplicationUriAsync(Uri uri)
        {
            Windows.Storage.StorageFile pathFile;

            try
            {
                pathFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(uri);
            }
            catch (Exception)
            {
                pathFile = null;
            }

            if (pathFile == null)
            {
                return null;
            }

            StorageFile resultFile = new StorageFile(pathFile);

            return resultFile;
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
                throw new StorageItemNotFoundException(this.Name, "Cannot rename a file that does not exist.");
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
                throw new StorageItemNotFoundException(this.Name, "Cannot delete a file that does not exist.");
            }

            await this.Originator.DeleteAsync();
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
        public async Task<IBasicProperties> GetBasicPropertiesAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(
                    this.Name,
                    "Cannot get properties for a folder that does not exist.");
            }

            Windows.Storage.StorageFile storageFolder = this.Originator;
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
            if (item is StorageFile file)
            {
                return file.Originator.IsEqual(this.Originator);
            }

            return false;
        }

        /// <summary>
        /// Opens a stream over the current file for reading file contents.
        /// </summary>
        /// <returns>
        /// When this method completes, it returns the stream.
        /// </returns>
        public async Task<Stream> OpenReadAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot open a file that does not exist.");
            }

            IRandomAccessStreamWithContentType s = await this.Originator.OpenReadAsync();
            return s.AsStream();
        }

        /// <summary>Opens a stream over the file.</summary>
        /// <returns>When this method completes, it returns the stream.</returns>
        /// <param name="accessMode">The type of access to allow.</param>
        public async Task<Stream> OpenAsync(FileAccessMode accessMode)
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot open a file that does not exist.");
            }

            IRandomAccessStream s = await this.Originator.OpenAsync(accessMode.ToWindowsFileAccessMode());
            return s.AsStream();
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
        public async Task<IStorageFile> CopyAsync(
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

            Windows.Storage.StorageFolder storageFolder =
                await Windows.Storage.StorageFolder.GetFolderFromPathAsync(System.IO.Path.GetDirectoryName(destinationFolder.Path));

            Windows.Storage.StorageFile copiedStorageFile =
                await this.Originator.CopyAsync(storageFolder, desiredNewName, option.ToWindowsNameCollisionOption());

            StorageFile copiedFile = new StorageFile(copiedStorageFile);
            return copiedFile;
        }

        /// <summary>Replaces the specified file with a copy of the current file.</summary>
        /// <returns>No object or value is returned when this method completes.</returns>
        /// <param name="fileToReplace">The file to replace.</param>
        public async Task CopyAndReplaceAsync(IStorageFile fileToReplace)
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

            Windows.Storage.StorageFile storageFile = await Windows.Storage.StorageFile.GetFileFromPathAsync(fileToReplace.Path);

            await this.Originator.CopyAndReplaceAsync(storageFile);
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
        public async Task MoveAsync(
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

            Windows.Storage.StorageFolder storageFolder =
                await Windows.Storage.StorageFolder.GetFolderFromPathAsync(System.IO.Path.GetDirectoryName(destinationFolder.Path));

            await this.Originator.MoveAsync(storageFolder, desiredNewName, option.ToWindowsNameCollisionOption());
        }

        /// <summary>Moves the current file to the location of the specified file and replaces the specified file in that location.</summary>
        /// <returns>No object or value is returned by this method.</returns>
        /// <param name="fileToReplace">The file to replace.</param>
        public async Task MoveAndReplaceAsync(IStorageFile fileToReplace)
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

            Windows.Storage.StorageFile storageFile = await Windows.Storage.StorageFile.GetFileFromPathAsync(fileToReplace.Path);

            await this.Originator.MoveAndReplaceAsync(storageFile);
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

            await FileIO.WriteTextAsync(this.Originator, text);
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

            string text = await FileIO.ReadTextAsync(this.Originator);
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
        public async Task WriteBytesAsync(byte[] bytes)
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot write to a file that does not exist.");
            }

            await FileIO.WriteBytesAsync(this.Originator, bytes);
        }

        /// <summary>
        /// Reads the current file as a byte array.
        /// </summary>
        /// <returns>
        /// When this method completes, it returns the file's content as a byte array.
        /// </returns>
        public async Task<byte[]> ReadBytesAsync()
        {
            if (!this.Exists)
            {
                throw new StorageItemNotFoundException(this.Name, "Cannot read from a file that does not exist.");
            }

            IBuffer buffer = await FileIO.ReadBufferAsync(this.Originator);
            return buffer.ToArray();
        }
    }
}
#endif