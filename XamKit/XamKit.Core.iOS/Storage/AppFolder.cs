namespace XamKit.Core.Storage
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using XamKit.Core.Common.Storage;

    /// <summary>
    /// Defines the app's root folder for an iOS application.
    /// </summary>
    public class AppFolder : IAppFolder
    {
        public AppFolder(string path)
        {
            this.Path = path;
        }

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

        public async Task<IAppFile> CreateFileAsync(
            string fileName,
            FileStoreCreationOption creationOption = FileStoreCreationOption.ThrowExceptionIfExists)
        {
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

            return new AppFile(filePath);
        }

        public Task<IAppFolder> CreateFolderAsync(
            string folderName,
            FileStoreCreationOption creationOption = FileStoreCreationOption.ThrowExceptionIfExists)
        {
            throw new NotImplementedException();
        }

        public Task<IAppFile> GetFileAsync(string fileName, bool createIfNotExisting = false)
        {
            throw new NotImplementedException();
        }

        public Task<IAppFolder> GetFolderAsync(string folderName, bool createIfNotExisting = false)
        {
            throw new NotImplementedException();
        }

        private static void CreateFile(string filePath)
        {
            using (File.Create(filePath))
            {
            }
        }
    }
}