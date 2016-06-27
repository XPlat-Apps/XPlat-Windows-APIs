namespace XamKit.Core.Storage
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using XamKit.Core.Common.Serialization;
    using XamKit.Core.Common.Storage;
    using XamKit.Core.Serialization;

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

        /// <summary>
        /// Serializes an object to the file as a string. Will overwrite any data already stored in the file.
        /// </summary>
        /// <param name="dataToSerialize">
        /// The data to serialize.
        /// </param>
        /// <param name="serializationService">
        /// The service for serialization. If null, will use JSON.
        /// </param>
        /// <typeparam name="T">
        /// The type of object to save.
        /// </typeparam>
        /// <returns>
        /// Returns an await-able task.
        /// </returns>
        public async Task SaveDataToFileAsync<T>(T dataToSerialize, ISerializationService serializationService)
        {
            if (!this.Exists)
            {
                throw new NotSupportedException("Cannot save data to a file that does not exist.");
            }

            if (serializationService == null)
            {
                serializationService = SerializationService.Json;
            }

            var serializedData = serializationService.Serialize(dataToSerialize);

            await this.WriteTextAsync(serializedData);
        }

        /// <summary>
        /// Deserializes an object from the file.
        /// </summary>
        /// <param name="serializationService">
        /// The service for seialization. If null, will use JSON.
        /// </param>
        /// <typeparam name="T">
        /// The type of object to load.
        /// </typeparam>
        /// <returns>
        /// Returns the deserialized data.
        /// </returns>
        public async Task<T> LoadDataFromFileAsync<T>(ISerializationService serializationService)
        {
            if (!this.Exists)
            {
                throw new NotSupportedException("Cannot read data from a file that does not exist.");
            }

            if (serializationService == null)
            {
                serializationService = SerializationService.Json;
            }

            var serializedData = await this.ReadTextAsync();

            var deserializedData = serializationService.Deserialize<T>(serializedData);
            return deserializedData;
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