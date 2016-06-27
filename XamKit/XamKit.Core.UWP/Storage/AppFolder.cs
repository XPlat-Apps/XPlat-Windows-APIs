namespace XamKit.Core.Storage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Windows.Storage;

    using XamKit.Core.Common.Storage;
    using XamKit.Core.Extensions;

    /// <summary>
    /// Defines the app's root folder for a UWP application.
    /// </summary>
    public class AppFolder : IAppFolder
    {
        private readonly IStorageFolder folder;

        public AppFolder(IAppFolder parentFolder, IStorageFolder folder)
        {
            if (folder == null)
            {
                throw new ArgumentNullException(nameof(folder));
            }

            this.folder = folder;
            this.ParentFolder = parentFolder;
        }

        /// <summary>
        /// Gets the parent folder.
        /// </summary>
        public IAppFolder ParentFolder { get; }

        /// <summary>
        /// Gets the user-friendly name of the current folder.
        /// </summary>
        public string Name
        {
            get
            {
                return this.folder.Name;
            }
        }

        /// <summary>
        /// Gets the full path of the current folder in the file system.
        /// </summary>
        public string Path
        {
            get
            {
                return this.folder.Path;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the file store item exists.
        /// </summary>
        public bool Exists
        {
            get
            {
                return this.folder != null && Directory.Exists(this.folder.Path);
            }
        }

        /// <summary>
        /// Gets the date the file store item was created.
        /// </summary>
        public DateTime DateCreated
        {
            get
            {
                return this.folder.DateCreated.DateTime;
            }
        }

        /// <summary>
        /// Creates a new file with the specified name in the current folder.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="creationOption">
        /// An optional option which determines how to create the file if the specified name already exists in the current folder.
        /// </param>
        /// <returns>
        /// Returns an IAppFile representing the created file.
        /// </returns>
        public async Task<IAppFile> CreateFileAsync(
            string fileName,
            FileStoreCreationOption creationOption = FileStoreCreationOption.ThrowExceptionIfExists)
        {
            if (!this.Exists)
            {
                throw new NotSupportedException("Cannot create a file in a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            var containedFile = await this.folder.CreateFileAsync(fileName, creationOption.ToCreationCollisionOption());
            return new AppFile(this, containedFile);
        }

        /// <summary>
        /// Creates a new subfolder with the specified name in the current folder.
        /// </summary>
        /// <param name="folderName">
        /// The folder name.
        /// </param>
        /// <param name="creationOption">
        /// An optional option which determines how to create the folder if the specified name already exists in the current folder.
        /// </param>
        /// <returns>
        /// Returns an IAppFolder representing the created folder.
        /// </returns>
        public async Task<IAppFolder> CreateFolderAsync(
            string folderName,
            FileStoreCreationOption creationOption = FileStoreCreationOption.ThrowExceptionIfExists)
        {
            if (!this.Exists)
            {
                throw new NotSupportedException("Cannot create a folder in a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(folderName))
            {
                throw new ArgumentNullException(nameof(folderName));
            }

            var containedFolder =
                await this.folder.CreateFolderAsync(folderName, creationOption.ToCreationCollisionOption());
            return new AppFolder(this, containedFolder);
        }

        /// <summary>
        /// Gets the file with the specified name from the current folder.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="createIfNotExisting">
        /// An optional value indicating whether to create the file if it does not exist.
        /// </param>
        /// <returns>
        /// Returns an IAppFile representing the file.
        /// </returns>
        public async Task<IAppFile> GetFileAsync(string fileName, bool createIfNotExisting = false)
        {
            if (!this.Exists)
            {
                throw new NotSupportedException("Cannot get a file from a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            StorageFile containedFile = null;

            try
            {
                containedFile = await this.folder.GetFileAsync(fileName);
            }
            catch (FileNotFoundException)
            {
                if (!createIfNotExisting)
                {
                    throw;
                }
            }

            if (createIfNotExisting && containedFile == null)
            {
                return await this.CreateFileAsync(fileName, FileStoreCreationOption.OpenIfExists);
            }

            return new AppFile(this, containedFile);
        }

        /// <summary>
        /// Gets the files from the current folder.
        /// </summary>
        /// <returns>
        /// Returns a collection of IAppFiles.
        /// </returns>
        public async Task<IEnumerable<IAppFile>> GetFilesAsync()
        {
            if (!this.Exists)
            {
                throw new NotSupportedException("Cannot get files from a folder that does not exist.");
            }

            var files = await this.folder.GetFilesAsync();

            return files.Select(storageFile => new AppFile(this, storageFile));
        }

        /// <summary>
        /// Gets the folder with the specified name from the current folder.
        /// </summary>
        /// <param name="folderName">
        /// The folder name.
        /// </param>
        /// <param name="createIfNotExisting">
        /// An optional value indicating whether to create the file if it does not exist.
        /// </param>
        /// <returns>
        /// Returns an IAppFolder representing the folder.
        /// </returns>
        public async Task<IAppFolder> GetFolderAsync(string folderName, bool createIfNotExisting = false)
        {
            if (!this.Exists)
            {
                throw new NotSupportedException("Cannot get a folder from a folder that does not exist.");
            }

            if (string.IsNullOrWhiteSpace(folderName))
            {
                throw new ArgumentNullException(nameof(folderName));
            }

            StorageFolder containedFolder = null;

            try
            {
                containedFolder = await this.folder.GetFolderAsync(folderName);
            }
            catch (FileNotFoundException)
            {
                if (!createIfNotExisting)
                {
                    throw;
                }
            }

            if (createIfNotExisting && containedFolder == null)
            {
                return await this.CreateFolderAsync(folderName, FileStoreCreationOption.OpenIfExists);
            }

            return new AppFolder(this, containedFolder);
        }

        /// <summary>
        /// Gets the folders from the current folder.
        /// </summary>
        /// <returns>
        /// Returns a collection of IAppFolders.
        /// </returns>
        public async Task<IEnumerable<IAppFolder>> GetFoldersAsync()
        {
            if (!this.Exists)
            {
                throw new NotSupportedException("Cannot get folders from a folder that does not exist.");
            }

            var folders = await this.folder.GetFoldersAsync();

            return folders.Select(storageFolder => new AppFolder(this, storageFolder));
        }

        /// <summary>
        /// Deletes the folder and contents.
        /// </summary>
        /// <returns>
        /// Returns an await-able task.
        /// </returns>
        public async Task DeleteAsync()
        {
            if (!this.Exists)
            {
                throw new NotSupportedException("Cannot delete a folder that does not exist.");
            }

            await this.folder.DeleteAsync(StorageDeleteOption.PermanentDelete);
        }
    }
}