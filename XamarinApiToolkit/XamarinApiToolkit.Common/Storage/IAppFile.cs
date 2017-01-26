namespace XamarinApiToolkit.Storage
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IAppFile : IAppStorageItem
    {
        /// <summary>
        /// Gets the file type.
        /// </summary>
        string FileType { get; }

        /// <summary>
        /// Opens a stream containing the file's contents.
        /// </summary>
        /// <param name="accessMode">
        /// The access mode for the file.
        /// </param>
        /// <returns>
        /// Returns the file stream.
        /// </returns>
        Task<Stream> OpenAsync(FileAccessOption accessMode);

        /// <summary>
        /// Copies the file to a specified destination folder.
        /// </summary>
        /// <param name="destinationFolder">
        /// The destination folder.
        /// </param>
        /// <returns>
        /// Returns the copied file.
        /// </returns>
        Task<IAppFile> CopyAsync(IAppFolder destinationFolder);

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
        Task<IAppFile> CopyAsync(IAppFolder destinationFolder, string desiredNewName);

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
        Task<IAppFile> CopyAsync(
            IAppFolder destinationFolder,
            string desiredNewName,
            FileStoreNameCollisionOption option);

        /// <summary>
        /// Copies and replaces the file to the specified file.
        /// </summary>
        /// <param name="fileToReplace">
        /// The file to replace.
        /// </param>
        /// <returns>
        /// Returns a task.
        /// </returns>
        Task CopyAndReplaceAsync(IAppFile fileToReplace);

        /// <summary>
        /// Moves the file to the specified destination folder.
        /// </summary>
        /// <param name="destinationFolder">
        /// The destination folder.
        /// </param>
        /// <returns>
        /// Returns a task.
        /// </returns>
        Task MoveAsync(IAppFolder destinationFolder);

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
        Task MoveAsync(IAppFolder destinationFolder, string desiredNewName);

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
        Task MoveAsync(IAppFolder destinationFolder, string desiredNewName, FileStoreNameCollisionOption option);

        /// <summary>
        /// Moves and replaces the specified file.
        /// </summary>
        /// <param name="fileToReplace">
        /// The file to replace.
        /// </param>
        /// <returns>
        /// Returns a task.
        /// </returns>
        Task MoveAndReplaceAsync(IAppFile fileToReplace);

        /// <summary>
        /// Writes a string to the content of the file.
        /// </summary>
        /// <param name="text">
        /// The text to store.
        /// </param>
        /// <returns>
        /// Returns a task.
        /// </returns>
        Task WriteTextAsync(string text);

        /// <summary>
        /// Reads the content of the file as a string.
        /// </summary>
        /// <returns>
        /// Returns the content.
        /// </returns>
        Task<string> ReadTextAsync();
    }
}