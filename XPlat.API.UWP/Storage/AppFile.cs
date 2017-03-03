namespace XPlat.API.Storage
{
    using Windows.Storage;

    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

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

        /// <inheritdoc />
        public DateTime DateCreated
        {
            get
            {
                return this.file.DateCreated.DateTime;
            }
        }

        /// <inheritdoc />
        public string Name
        {
            get
            {
                return this.file.Name;
            }
        }

        /// <inheritdoc />
        public string Path
        {
            get
            {
                return this.file.Path;
            }
        }

        /// <inheritdoc />
        public bool Exists
        {
            get
            {
                return this.file != null && File.Exists(this.Path);
            }
        }

        /// <inheritdoc />
        public string FileType
        {
            get
            {
                return this.file.FileType;
            }
        }

        /// <inheritdoc />
        public IAppFolder Parent { get; private set; }

        /// <inheritdoc />
        public Task RenameAsync(string desiredName)
        {
            return this.RenameAsync(desiredName, FileStoreNameCollisionOption.FailIfExists);
        }

        /// <inheritdoc />
        public async Task RenameAsync(string desiredName, FileStoreNameCollisionOption option)
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(this.Name, "Cannot rename a file that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(desiredName))
            {
                throw new ArgumentNullException(nameof(desiredName));
            }

            await this.file.RenameAsync(desiredName, option.ToNameCollisionOption());
        }

        /// <inheritdoc />
        public async Task DeleteAsync()
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(this.Name, "Cannot delete a file that does not exist.");
            }

            await this.file.DeleteAsync();
        }

        /// <inheritdoc />
        public async Task<IDictionary<string, object>> GetPropertiesAsync()
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(
                          this.Name,
                          "Cannot get properties for a file that does not exist.");
            }

            var storageFile = this.file as StorageFile;
            return storageFile != null ? await storageFile.Properties.RetrievePropertiesAsync(null) : null;
        }

        /// <inheritdoc />
        public bool IsOfType(FileStoreItemTypes type)
        {
            return type == FileStoreItemTypes.File;
        }

        /// <inheritdoc />
        public async Task<Stream> OpenAsync(FileAccessOption accessMode)
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(this.Name, "Cannot open a file that does not exist.");
            }

            var s = await this.file.OpenAsync(accessMode.ToFileAccessMode());
            return s.AsStream();
        }

        /// <inheritdoc />
        public Task<IAppFile> CopyAsync(IAppFolder destinationFolder)
        {
            return this.CopyAsync(destinationFolder, this.Name);
        }

        /// <inheritdoc />
        public Task<IAppFile> CopyAsync(IAppFolder destinationFolder, string desiredNewName)
        {
            return this.CopyAsync(destinationFolder, desiredNewName, FileStoreNameCollisionOption.FailIfExists);
        }

        /// <inheritdoc />
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

            var storageFolder =
                await StorageFolder.GetFolderFromPathAsync(System.IO.Path.GetDirectoryName(destinationFolder.Path));

            var copiedStorageFile =
                await this.file.CopyAsync(storageFolder, desiredNewName, option.ToNameCollisionOption());

            var copiedFile = new AppFile(destinationFolder, copiedStorageFile);
            return copiedFile;
        }

        /// <inheritdoc />
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

            var storageFile = await StorageFile.GetFileFromPathAsync(fileToReplace.Path);

            await this.file.CopyAndReplaceAsync(storageFile);
        }

        /// <inheritdoc />
        public Task MoveAsync(IAppFolder destinationFolder)
        {
            return this.MoveAsync(destinationFolder, this.Name);
        }

        /// <inheritdoc />
        public Task MoveAsync(IAppFolder destinationFolder, string desiredNewName)
        {
            return this.MoveAsync(destinationFolder, desiredNewName, FileStoreNameCollisionOption.ReplaceExisting);
        }

        /// <inheritdoc />
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

            var storageFolder =
                await StorageFolder.GetFolderFromPathAsync(System.IO.Path.GetDirectoryName(destinationFolder.Path));

            await this.file.MoveAsync(storageFolder, desiredNewName, option.ToNameCollisionOption());

            this.Parent = destinationFolder;
        }

        /// <inheritdoc />
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

            var storageFile = await StorageFile.GetFileFromPathAsync(fileToReplace.Path);

            await this.file.MoveAndReplaceAsync(storageFile);

            this.Parent = fileToReplace.Parent;
        }

        /// <inheritdoc />
        public async Task WriteTextAsync(string text)
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(this.Name, "Cannot write to a file that does not exist.");
            }

            await FileIO.WriteTextAsync(this.file, text);
        }

        /// <inheritdoc />
        public async Task<string> ReadTextAsync()
        {
            if (!this.Exists)
            {
                throw new AppStorageItemNotFoundException(this.Name, "Cannot read from a file that does not exist.");
            }

            var text = await FileIO.ReadTextAsync(this.file);
            return text;
        }

        /// <summary>
        /// Gets an IAppFile object to represent the file at the specified path.
        /// </summary>
        /// <param name="path">
        /// The path of the file to get a IAppFile to represent. If your path uses slashes, make sure you use backslashes (\). Forward slashes (/) are not accepted by this method.
        /// </param>
        /// <returns>
        /// When this method completes, it returns the file as an IAppFile.
        /// </returns>
        public static async Task<IAppFile> GetFileFromPathAsync(string path)
        {
            StorageFile pathFile;
            StorageFolder pathFileParentFolder;

            AppFolder resultFileParentFolder;

            try
            {
                pathFile = await StorageFile.GetFileFromPathAsync(path);
            }
            catch (Exception)
            {
                pathFile = null;
            }

            if (pathFile == null)
            {
                return null;
            }

            try
            {
                pathFileParentFolder = await pathFile.GetParentAsync();
            }
            catch (Exception)
            {
                pathFileParentFolder = null;
            }

            if (pathFileParentFolder != null)
            {
                resultFileParentFolder = await AppFolder.GetFolderFromPathAsync(pathFileParentFolder.Path);
            }
            else
            {
                resultFileParentFolder = null;
            }

            var resultFile = new AppFile(resultFileParentFolder, pathFile);

            return resultFile;
        }

        /// <summary>
        /// Gets an IAppFile object to represent the file at the specified uri of the application.
        /// </summary>
        /// <param name="uri">
        /// The uri of the file to get a IAppFile to represent. 
        /// </param>
        /// <returns>
        /// When this method completes, it returns the file as an IAppFile.
        /// </returns>
        public static async Task<IAppFile> GetFileFromApplicationUriAsync(Uri uri)
        {
            StorageFile pathFile;
            StorageFolder pathFileParentFolder;

            AppFolder resultFileParentFolder;

            try
            {
                pathFile = await StorageFile.GetFileFromApplicationUriAsync(uri);
            }
            catch (Exception)
            {
                pathFile = null;
            }

            if (pathFile == null)
            {
                return null;
            }

            try
            {
                pathFileParentFolder = await pathFile.GetParentAsync();
            }
            catch (Exception)
            {
                pathFileParentFolder = null;
            }

            if (pathFileParentFolder != null)
            {
                resultFileParentFolder = await AppFolder.GetFolderFromPathAsync(pathFileParentFolder.Path);
            }
            else
            {
                resultFileParentFolder = null;
            }

            var resultFile = new AppFile(resultFileParentFolder, pathFile);

            return resultFile;
        }
    }
}