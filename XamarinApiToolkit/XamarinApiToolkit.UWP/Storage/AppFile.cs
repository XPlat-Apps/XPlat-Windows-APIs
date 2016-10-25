namespace XamarinApiToolkit.Storage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using Windows.Storage;

    /// <summary>
    /// Defines an application file.
    /// </summary>
    public sealed class AppFile : IAppFile
    {
        private readonly IStorageFile file;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppFile"/> class.
        /// </summary>
        /// <param name="parentFolder">
        /// The parent folder.
        /// </param>
        /// <param name="file">
        /// The associated <see cref="StorageFile"/>.
        /// </param>
        internal AppFile(IAppFolder parentFolder, IStorageFile file)
        {
            if (parentFolder == null)
            {
                throw new ArgumentNullException(nameof(parentFolder));
            }

            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            this.file = file;
            this.Parent = parentFolder;
        }

        /// <summary>
        /// Gets the date the item was created.
        /// </summary>
        public DateTime DateCreated
        {
            get
            {
                return this.file.DateCreated.DateTime;
            }
        }

        /// <summary>
        /// Gets the name of the item.
        /// </summary>
        public string Name
        {
            get
            {
                return this.file.Name;
            }
        }

        /// <summary>
        /// Gets the full path to the item.
        /// </summary>
        public string Path
        {
            get
            {
                return this.file.Path;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the item exists.
        /// </summary>
        public bool Exists
        {
            get
            {
                return this.file != null && File.Exists(this.Path);
            }
        }

        /// <summary>
        /// Gets the file type.
        /// </summary>
        public string FileType
        {
            get
            {
                return this.file.FileType;
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
                throw new FileNotFoundException("Cannot rename a file that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(desiredNewName))
            {
                throw new ArgumentNullException(nameof(desiredNewName));
            }

            await this.file.RenameAsync(desiredNewName, option.ToNameCollisionOption());
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
                throw new FileNotFoundException("Cannot delete a file that does not exist.");
            }

            await this.file.DeleteAsync();
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
                throw new FileNotFoundException("Cannot get properties for a file that does not exist.");
            }

            var storageFile = this.file as StorageFile;
            return storageFile != null ? await storageFile.Properties.RetrievePropertiesAsync(null) : null;
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
                throw new FileNotFoundException("Cannot open a file that does not exist.");
            }

            var s = await this.file.OpenAsync(accessMode.ToFileAccessMode());
            return s.AsStream();
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
                throw new FileNotFoundException("Cannot copy a file that does not exist.");
            }

            if (destinationFolder == null)
            {
                throw new ArgumentNullException(nameof(destinationFolder));
            }

            if (!destinationFolder.Exists)
            {
                throw new FileNotFoundException("Cannot copy a file to a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(desiredNewName))
            {
                throw new ArgumentNullException(nameof(desiredNewName));
            }

            var storageFolder =
                await StorageFolder.GetFolderFromPathAsync(System.IO.Path.GetDirectoryName(destinationFolder.Path));

            var copiedStorageFile =
                await this.file.CopyAsync(storageFolder, desiredNewName, option.ToNameCollisionOption());

            var copiedFile = new AppFile(destinationFolder, copiedStorageFile);
            return copiedFile;
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
                throw new FileNotFoundException("Cannot copy a file that does not exist.");
            }

            if (fileToReplace == null)
            {
                throw new ArgumentNullException(nameof(fileToReplace));
            }

            if (!fileToReplace.Exists)
            {
                throw new FileNotFoundException("Cannot copy to and replace a file that does not exist.");
            }

            var storageFile = await StorageFile.GetFileFromPathAsync(fileToReplace.Path);

            await this.file.CopyAndReplaceAsync(storageFile);
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
                throw new FileNotFoundException("Cannot move a file that does not exist.");
            }

            if (destinationFolder == null)
            {
                throw new ArgumentNullException(nameof(destinationFolder));
            }

            if (!destinationFolder.Exists)
            {
                throw new FileNotFoundException("Cannot move a file to a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(desiredNewName))
            {
                throw new ArgumentNullException(nameof(desiredNewName));
            }

            var storageFolder =
                await StorageFolder.GetFolderFromPathAsync(System.IO.Path.GetDirectoryName(destinationFolder.Path));

            await this.file.MoveAsync(storageFolder, desiredNewName, option.ToNameCollisionOption());

            this.Parent = destinationFolder;
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
                throw new FileNotFoundException("Cannot move a file that does not exist.");
            }

            if (fileToReplace == null)
            {
                throw new ArgumentNullException(nameof(fileToReplace));
            }

            if (!fileToReplace.Exists)
            {
                throw new FileNotFoundException("Cannot move to and replace a file that does not exist.");
            }

            var storageFile = await StorageFile.GetFileFromPathAsync(fileToReplace.Path);

            await this.file.MoveAndReplaceAsync(storageFile);

            // ToDo, update parent folder.
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
                throw new FileNotFoundException("Cannot write to a file that does not exist.");
            }

            await FileIO.WriteTextAsync(this.file, text);
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
                throw new FileNotFoundException("Cannot read from a file that does not exist.");
            }

            var text = await FileIO.ReadTextAsync(this.file);
            return text;
        }
    }
}