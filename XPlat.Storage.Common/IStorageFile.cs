namespace XPlat.Storage
{
    using System.IO;
    using System.Threading.Tasks;

    using XPlat.Storage.FileProperties;

    public interface IStorageFile : IStorageItem
    {
        /// <summary>
        /// Gets the type (file name extension) of the file.
        /// </summary>
        string FileType { get; }

        /// <summary>
        /// Gets the MIME type of the contents of the file.
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// Opens a stream over the current file for reading file contents.
        /// </summary>
        /// <returns>
        /// When this method completes, it returns the stream.
        /// </returns>
        Task<Stream> OpenReadAsync();

        /// <summary>
        /// Opens a stream over the file.
        /// </summary>
        /// <param name="accessMode">
        /// The type of access to allow.
        /// </param>
        /// <returns>
        /// When this method completes, it returns the stream.
        /// </returns>
        Task<Stream> OpenAsync(FileAccessMode accessMode);

        /// <summary>
        /// Creates a copy of the file in the specified folder.
        /// </summary>
        /// <param name="destinationFolder">
        /// The destination folder where the copy is created.
        /// </param>
        /// <returns>
        /// When this method completes, it returns an IStorageFile that represents the copy.
        /// </returns>
        Task<IStorageFile> CopyAsync(IStorageFolder destinationFolder);

        /// <summary>
        /// Creates a copy of the file in the specified folder, using the desired name.
        /// </summary>
        /// <param name="destinationFolder">
        /// The destination folder where the copy is created.
        /// </param>
        /// <param name="desiredNewName">
        /// The desired name of the copy. If there is an existing file in the destination folder that already has the specified desiredNewName, this method will throw an exception.
        /// </param>
        /// <returns>
        /// When this method completes, it returns an IStorageFile that represents the copy.
        /// </returns>
        Task<IStorageFile> CopyAsync(IStorageFolder destinationFolder, string desiredNewName);

        /// <summary>
        /// Creates a copy of the file in the specified folder, using the desired name. This method also specifies what to do if an existing file in the specified folder has the same name.
        /// </summary>
        /// <param name="destinationFolder">
        /// The destination folder where the copy is created.
        /// </param>
        /// <param name="desiredNewName">
        /// The desired name of the copy. If there is an existing file in the destination folder that already has the specified desiredNewName, the specified NameCollisionOption determines how the API responds to the conflict.
        /// </param>
        /// <param name="option">
        /// An enum value that determines how the API responds if the desiredNewName is the same as the name of an existing file in the destination folder.
        /// </param>
        /// <returns>
        /// When this method completes, it returns an IStorageFile that represents the copy.
        /// </returns>
        Task<IStorageFile> CopyAsync(
            IStorageFolder destinationFolder,
            string desiredNewName,
            NameCollisionOption option);

        /// <summary>
        /// Replaces the specified file with a copy of the current file.
        /// </summary>
        /// <param name="fileToReplace">
        /// The file to replace.
        /// </param>
        /// <returns>
        /// An object that is used to manage the asynchronous operation.
        /// </returns>
        Task CopyAndReplaceAsync(IStorageFile fileToReplace);

        /// <summary>
        /// Moves the current file to the specified folder.
        /// </summary>
        /// <param name="destinationFolder">
        /// The destination folder where the file is moved. This destination folder must be a physical location. Otherwise, this method fails and throws an exception.
        /// </param>
        /// <returns>
        /// An object that is used to manage the asynchronous operation.
        /// </returns>
        Task MoveAsync(IStorageFolder destinationFolder);

        /// <summary>
        /// Moves the current file to the specified folder and renames the file according to the desired name.
        /// </summary>
        /// <param name="destinationFolder">
        /// The destination folder where the file is moved. This destination folder must be a physical location. Otherwise, this method fails and throws an exception.
        /// </param>
        /// <param name="desiredNewName">
        /// The desired name of the file after it is moved. If there is an existing file in the destination folder that already has the specified desiredNewName, the API will replace the existing file.
        /// </param>
        /// <returns>
        /// An object that is used to manage the asynchronous operation.
        /// </returns>
        Task MoveAsync(IStorageFolder destinationFolder, string desiredNewName);

        /// <summary>
        /// Moves the current file to the specified folder and renames the file according to the desired name. This method also specifies what to do if a file with the same name already exists in the specified folder.
        /// </summary>
        /// <param name="destinationFolder">
        /// The destination folder where the file is moved. This destination folder must be a physical location. Otherwise, this method fails and throws an exception.
        /// </param>
        /// <param name="desiredNewName">
        /// The desired name of the file after it is moved. If there is an existing file in the destination folder that already has the specified desiredNewName, the specified NameCollisionOption determines how the API responds to the conflict.
        /// </param>
        /// <param name="option">
        /// An enum value that determines how the API responds if the desiredNewName is the same as the name of an existing file in the destination folder.
        /// </param>
        /// <returns>
        /// An object that is used to manage the asynchronous operation.
        /// </returns>
        Task MoveAsync(IStorageFolder destinationFolder, string desiredNewName, NameCollisionOption option);

        /// <summary>
        /// Moves the current file to the location of the specified file and replaces the specified file in that location.
        /// </summary>
        /// <param name="fileToReplace">
        /// The file to replace.
        /// </param>
        /// <returns>
        /// An object that is used to manage the asynchronous operation.
        /// </returns>
        Task MoveAndReplaceAsync(IStorageFile fileToReplace);

        /// <summary>
        /// Writes a string to the current file.
        /// </summary>
        /// <param name="text">
        /// The text to write out.
        /// </param>
        /// <returns>
        /// An object that is used to manage the asynchronous operation.
        /// </returns>
        Task WriteTextAsync(string text);

        /// <summary>
        /// Reads the current file as a string.
        /// </summary>
        /// <returns>
        /// When this method completes, it returns the file's content as a string.
        /// </returns>
        Task<string> ReadTextAsync();

        /// <summary>
        /// Writes a byte array to the current file.
        /// </summary>
        /// <param name="bytes">
        /// The byte array to write out.
        /// </param>
        /// <returns>
        /// An object that is used to manage the asynchronous operation.
        /// </returns>
        Task WriteBytesAsync(byte[] bytes);

        /// <summary>
        /// Reads the current file as a byte array.
        /// </summary>
        /// <returns>
        /// When this method completes, it returns the file's content as a byte array.
        /// </returns>
        Task<byte[]> ReadBytesAsync();

        /// <summary>
        /// Gets an object that provides access to the content-related properties of the current item.
        /// </summary>
        IStorageItemContentProperties Properties { get; }
    }
}