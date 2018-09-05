namespace XPlat.Storage
{
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>Provides additional extra methods for manipulating data for storage files.</summary>
    public interface IStorageFileExtras
    {
        /// <summary>
        /// Opens a stream over the current file for reading file contents.
        /// </summary>
        /// <returns>
        /// When this method completes, it returns the stream.
        /// </returns>
        Task<Stream> OpenReadAsync();

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
    }
}