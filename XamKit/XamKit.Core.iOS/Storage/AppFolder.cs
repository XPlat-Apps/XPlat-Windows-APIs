namespace XamKit.Core.Storage
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using XamKit.Core.Common.Serialization;
    using XamKit.Core.Common.Storage;
    using XamKit.Core.Serialization;

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