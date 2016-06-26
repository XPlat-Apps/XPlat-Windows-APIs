namespace XamKit.Core.Storage
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using XamKit.Core.Common.Storage;

    public class AppFile : IAppFile
    {
        public AppFile(IAppFolder parentFolder, string path)
        {
            this.ParentFolder = parentFolder;
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
        public string Path { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the file store item exists.
        /// </summary>
        public bool Exists
        {
            get
            {
                return File.Exists(this.Path);
            }
        }

        public IAppFolder ParentFolder { get; private set; }

        /// <summary>
        /// Opens a stream containing the file's data.
        /// </summary>
        /// <param name="option">
        /// Optional, specifies the type of access to allow for the file. Default to read only.
        /// </param>
        /// <returns>
        /// Returns a Stream that contains the requested file's data.
        /// </returns>
        public async Task<Stream> OpenAsync(FileAccessOption option = FileAccessOption.ReadOnly)
        {
            await Helpers.CreateNewTaskSchedulerAwaiter();

            switch (option)
            {
                case FileAccessOption.ReadOnly:
                    return File.OpenRead(this.Path);
                case FileAccessOption.ReadAndWrite:
                    return File.Open(this.Path, FileMode.Open, FileAccess.ReadWrite);
                default:
                    throw new ArgumentException();
            }
        }

        public async Task MoveAsync(
            IAppFolder destinationFolder,
            string newName = "",
            FileNameCreationOption option = FileNameCreationOption.ReplaceIfExists)
        {
            if (destinationFolder == null)
            {
                throw new ArgumentNullException(nameof(destinationFolder));
            }

            if (!destinationFolder.Exists)
            {
                throw new DirectoryNotFoundException(
                    "The directory for the specified destination folder does not exist.");
            }

            await Helpers.CreateNewTaskSchedulerAwaiter();

            var filePath = System.IO.Path.Combine(destinationFolder.Path, newName);

            if (File.Exists(filePath))
            {
                switch (option)
                {
                    case FileNameCreationOption.ThrowExceptionIfExists:
                        throw new IOException(
                            $"A file with the name '{newName}' already exists in '{destinationFolder.Path}'.");
                    case FileNameCreationOption.ReplaceIfExists:
                        File.Delete(filePath);
                        break;
                    case FileNameCreationOption.GenerateUniqueIdentifier:
                        newName = $"{Guid.NewGuid()}-{newName}";
                        filePath = System.IO.Path.Combine(destinationFolder.Path, newName);
                        break;
                }
            }

            File.Move(this.Path, filePath);

            this.ParentFolder = destinationFolder;
            this.Path = filePath;
        }

        public async Task RenameAsync(
            string fileName,
            FileNameCreationOption option = FileNameCreationOption.ThrowExceptionIfExists)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            await this.MoveAsync(this.ParentFolder, fileName, option);
        }

        public async Task DeleteAsync()
        {
            await Helpers.CreateNewTaskSchedulerAwaiter();

            if (!this.Exists)
            {
                throw new FileNotFoundException($"The file does not exist at '{this.Path}'.");
            }

            File.Delete(this.Path);
        }

        public async Task WriteTextAsync(string text)
        {
            using (var stream = await this.OpenAsync(FileAccessOption.ReadAndWrite))
            {
                stream.SetLength(0);
                using (var writer = new StreamWriter(stream))
                {
                    await writer.WriteAsync(text);
                }
            }
        }

        public async Task<string> ReadTextAsync()
        {
            using (var stream = await this.OpenAsync())
            {
                using (var reader = new StreamReader(stream))
                {
                    var text = await reader.ReadToEndAsync();
                    return text;
                }
            }
        }
    }
}