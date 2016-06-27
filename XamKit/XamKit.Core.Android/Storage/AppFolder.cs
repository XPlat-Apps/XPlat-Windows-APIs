namespace XamKit.Core.Storage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using XamKit.Core.Common.Storage;

    /// <summary>
    /// Defines the app's root folder for an iOS application.
    /// </summary>
    public class AppFolder : IAppFolder
    {
        public AppFolder(IAppFolder parentFolder, string path)
        {
            this.ParentFolder = parentFolder;
            this.Path = path;
        }

        /// <summary>
        /// Gets the parent folder.
        /// </summary>
        public IAppFolder ParentFolder { get; }

        /// <summary>
        /// Gets the name of the current file store item.
        /// </summary>
        public string Name
        {
            get
            {
                return System.IO.Path.GetFileName(this.Path);
            }
        }

        /// <summary>
        /// Gets the full path of the current file store item.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Gets a value indicating whether the file store item exists.
        /// </summary>
        public bool Exists
        {
            get
            {
                return Directory.Exists(this.Path);
            }
        }

        /// <summary>
        /// Gets the date the file store item was created.
        /// </summary>
        public DateTime DateCreated
        {
            get
            {
                return Directory.GetCreationTime(this.Path);
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

            await Helpers.CreateNewTaskSchedulerAwaiter();

            var filePath = System.IO.Path.Combine(this.Path, fileName);
            if (File.Exists(filePath))
            {
                switch (creationOption)
                {
                    case FileStoreCreationOption.GenerateUniqueIdentifier:
                        fileName = $"{Guid.NewGuid()}-{fileName}";
                        filePath = System.IO.Path.Combine(this.Path, fileName);
                        CreateFile(filePath);
                        break;
                    case FileStoreCreationOption.ReplaceIfExists:
                        File.Delete(filePath);
                        CreateFile(filePath);
                        break;
                    case FileStoreCreationOption.ThrowExceptionIfExists:
                        throw new IOException($"A file with the name '{fileName}' already exists in '{this.Path}'.");
                }
            }
            else
            {
                CreateFile(filePath);
            }

            return new AppFile(this, filePath);
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

            await Helpers.CreateNewTaskSchedulerAwaiter();

            var folderPath = System.IO.Path.Combine(this.Path, folderName);
            if (Directory.Exists(folderPath))
            {
                switch (creationOption)
                {
                    case FileStoreCreationOption.ThrowExceptionIfExists:
                        throw new IOException($"A folder with the name '{folderName}' already exists in '{this.Path}'.");
                    case FileStoreCreationOption.ReplaceIfExists:
                        Directory.Delete(folderPath);
                        CreateFolder(folderPath);
                        break;
                    case FileStoreCreationOption.GenerateUniqueIdentifier:
                        folderName = $"{Guid.NewGuid()}-{folderName}";
                        folderPath = System.IO.Path.Combine(this.Path, folderName);
                        CreateFolder(folderPath);
                        break;
                }
            }
            else
            {
                CreateFolder(folderPath);
            }

            return new AppFolder(this, folderPath);
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

            await Helpers.CreateNewTaskSchedulerAwaiter();

            var filePath = System.IO.Path.Combine(this.Path, fileName);
            if (!File.Exists(filePath))
            {
                if (createIfNotExisting)
                {
                    return await this.CreateFileAsync(fileName);
                }

                throw new FileNotFoundException($"A file with the name '{fileName}' does not exist in '{this.Path}'.");
            }

            return new AppFile(this, filePath);
        }

        public async Task<IEnumerable<IAppFile>> GetFilesAsync()
        {
            if (!this.Exists)
            {
                throw new NotSupportedException("Cannot get files from a folder that does not exist.");
            }

            await Helpers.CreateNewTaskSchedulerAwaiter();

            var files = Directory.GetFiles(this.Path).Select(filePath => new AppFile(this, filePath));
            return files;
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

            await Helpers.CreateNewTaskSchedulerAwaiter();

            var folderPath = System.IO.Path.Combine(this.Path, folderName);
            if (!Directory.Exists(folderPath))
            {
                if (createIfNotExisting)
                {
                    return await this.CreateFolderAsync(folderName);
                }

                throw new DirectoryNotFoundException(
                    $"A folder with the name '{folderName}' does not exist in '{this.Path}'");
            }

            return new AppFolder(this, folderPath);
        }

        public async Task<IEnumerable<IAppFolder>> GetFoldersAsync()
        {
            if (!this.Exists)
            {
                throw new NotSupportedException("Cannot get folders from a folder that does not exist.");
            }

            await Helpers.CreateNewTaskSchedulerAwaiter();

            var folders = Directory.GetDirectories(this.Path).Select(directoryPath => new AppFolder(this, directoryPath));
            return folders;
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

            await Helpers.CreateNewTaskSchedulerAwaiter();

            Directory.Delete(this.Path, true);
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