namespace XPlat.Storage
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>Represents a file. Provides information about the file and its contents, and ways to manipulate them.</summary>
    public interface IStorageFile : IStorageItemProperties, IStorageItem2, IStorageFileExtras
    {
        /// <summary>Gets the type (file name extension) of the file.</summary>
        string FileType { get; }

        /// <summary>Gets the MIME type of the contents of the file.</summary>
        string ContentType { get; }

        /// <summary>Opens a stream over the file.</summary>
        /// <returns>When this method completes, it returns the stream.</returns>
        /// <param name="accessMode">The type of access to allow.</param>
        Task<Stream> OpenAsync(FileAccessMode accessMode);

        /// <summary>Creates a copy of the file in the specified folder.</summary>
        /// <returns>When this method completes, it returns a StorageFile that represents the copy.</returns>
        /// <param name="destinationFolder">The destination folder where the copy is created.</param>
        Task<IStorageFile> CopyAsync(IStorageFolder destinationFolder);

        /// <summary>Creates a copy of the file in the specified folder, using the desired name.</summary>
        /// <returns>When this method completes, it returns a StorageFile that represents the copy.</returns>
        /// <param name="destinationFolder">The destination folder where the copy is created.</param>
        /// <param name="desiredNewName">The desired name of the copy. If there is an existing file in the destination folder that already has the specified desiredNewName, Windows generates a unique name for the copy.</param>
        Task<IStorageFile> CopyAsync(IStorageFolder destinationFolder, string desiredNewName);

        /// <summary>Creates a copy of the file in the specified folder, using the desired name. This method also specifies what to do if an existing file in the specified folder has the same name.</summary>
        /// <returns>When this method completes, it returns a StorageFile that represents the copy.</returns>
        /// <param name="destinationFolder">The destination folder where the copy is created.</param>
        /// <param name="desiredNewName">The desired name of the copy. If there is an existing file in the destination folder that already has the specified desiredNewName, the specified NameCollisionOption determines how Windows responds to the conflict.</param>
        /// <param name="option">An enum value that determines how Windows responds if the desiredNewName is the same as the name of an existing file in the destination folder.</param>
        Task<IStorageFile> CopyAsync(
            IStorageFolder destinationFolder,
            string desiredNewName,
            NameCollisionOption option);

        /// <summary>Replaces the specified file with a copy of the current file.</summary>
        /// <returns>No object or value is returned when this method completes.</returns>
        /// <param name="fileToReplace">The file to replace.</param>
        Task CopyAndReplaceAsync(IStorageFile fileToReplace);

        /// <summary>Moves the current file to the specified folder.</summary>
        /// <returns>No object or value is returned by this method.</returns>
        /// <param name="destinationFolder">The destination folder where the file is moved. This destination folder must be a physical location. Otherwise, if the destination folder exists only in memory, like a file group, this method fails and throws an exception.</param>
        Task MoveAsync(IStorageFolder destinationFolder);

        /// <summary>Moves the current file to the specified folder and renames the file according to the desired name.</summary>
        /// <returns>No object or value is returned by this method.</returns>
        /// <param name="destinationFolder">The destination folder where the file is moved. This destination folder must be a physical location. Otherwise, if the destination folder exists only in memory, like a file group, this method fails and throws an exception.</param>
        /// <param name="desiredNewName">The desired name of the file after it is moved. If there is an existing file in the destination folder that already has the specified desiredNewName, Windows generates a unique name for the file.</param>
        Task MoveAsync(IStorageFolder destinationFolder, string desiredNewName);

        /// <summary>Moves the current file to the specified folder and renames the file according to the desired name. This method also specifies what to do if a file with the same name already exists in the specified folder.</summary>
        /// <returns>No object or value is returned by this method.</returns>
        /// <param name="destinationFolder">The destination folder where the file is moved. This destination folder must be a physical location. Otherwise, if the destination folder exists only in memory, like a file group, this method fails and throws an exception.</param>
        /// <param name="desiredNewName">The desired name of the file after it is moved. If there is an existing file in the destination folder that already has the specified desiredNewName, the specified NameCollisionOption determines how Windows responds to the conflict.</param>
        /// <param name="option">An enum value that determines how Windows responds if the desiredNewName is the same as the name of an existing file in the destination folder.</param>
        Task MoveAsync(IStorageFolder destinationFolder, string desiredNewName, NameCollisionOption option);

        /// <summary>Moves the current file to the location of the specified file and replaces the specified file in that location.</summary>
        /// <returns>No object or value is returned by this method.</returns>
        /// <param name="fileToReplace">The file to replace.</param>
        Task MoveAndReplaceAsync(IStorageFile fileToReplace);
    }
}