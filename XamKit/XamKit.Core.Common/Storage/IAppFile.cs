namespace XamKit.Core.Common.Storage
{
    using System.IO;
    using System.Threading.Tasks;

    using XamKit.Core.Common.Serialization;

    /// <summary>
    /// Defines an interface for an application file.
    /// </summary>
    public interface IAppFile : IAppFileStoreItem
    {
        /// <summary>
        /// Opens a stream containing the file's data.
        /// </summary>
        /// <param name="option">
        /// Optional, specifies the type of access to allow for the file. Default to read only.
        /// </param>
        /// <returns>
        /// Returns a Stream that contains the requested file's data.
        /// </returns>
        Task<Stream> OpenAsync(FileAccessOption option = FileAccessOption.ReadOnly);

        /// <summary>
        /// Moves the file to the specified folder.
        /// </summary>
        /// <param name="destinationFolder">
        /// The destination folder.
        /// </param>
        /// <param name="newName">
        /// Optional, specifies the new name for the file. If null or empty, will use existing name.
        /// </param>
        /// <param name="option">
        /// Optional, specifies how to handle moving the file when there is a conflict. Default to replace if already exists.
        /// </param>
        /// <returns>
        /// Returns an await-able task.
        /// </returns>
        Task MoveAsync(
            IAppFolder destinationFolder,
            string newName = "",
            FileNameCreationOption option = FileNameCreationOption.ReplaceIfExists);

        /// <summary>
        /// Renames the file.
        /// </summary>
        /// <param name="fileName">
        /// The new file name.
        /// </param>
        /// <param name="option">
        /// Optional, specifies how to handle renaming the file when there is a conflict. Default to throw exception if already exists.
        /// </param>
        /// <returns>
        /// Returns an await-able task.
        /// </returns>
        Task RenameAsync(string fileName, FileNameCreationOption option = FileNameCreationOption.ThrowExceptionIfExists);

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
        Task SaveDataToFileAsync<T>(T dataToSerialize, ISerializationService serializationService);

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
        Task<T> LoadDataFromFileAsync<T>(ISerializationService serializationService);

        /// <summary>
        /// Writes text to the file.
        /// </summary>
        /// <param name="text">
        /// The text to write out.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task WriteTextAsync(string text);

        /// <summary>
        /// Reads text from the file.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<string> ReadTextAsync();
    }
}