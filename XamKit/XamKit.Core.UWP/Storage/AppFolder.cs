namespace XamKit.Core.Storage
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Windows.Storage;
    using Windows.UI.Xaml.Controls;

    using XamKit.Core.Common.Serialization;
    using XamKit.Core.Common.Storage;
    using XamKit.Core.Extensions;
    using XamKit.Core.Serialization;

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
                return this.folder != null;
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
        /// Saves an object to file with the specified name in the current folder.
        /// </summary>
        /// <param name="dataToSerialize">
        /// The data to serialize.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="serializationService">
        /// The serialization service.
        /// </param>
        /// <typeparam name="T">
        /// The type of object to save.
        /// </typeparam>
        /// <returns>
        /// Returns an IAppFile represneting the saved file.
        /// </returns>
        public async Task<IAppFile> SaveToFileAsync<T>(
            T dataToSerialize,
            string fileName,
            ISerializationService serializationService)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (serializationService == null)
            {
                serializationService = SerializationService.Json;
            }

            var serializedData = serializationService.Serialize(dataToSerialize);

            var file = await this.CreateFileAsync(fileName, FileStoreCreationOption.ReplaceIfExists);
            await file.WriteTextAsync(serializedData);

            return file;
        }
    }
}