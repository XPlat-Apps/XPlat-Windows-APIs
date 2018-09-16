namespace XPlat.Devices
{
    using System;
    using System.Threading.Tasks;
    using XPlat.Storage;

    /// <summary>Starts the default app associated with the specified file or URI.</summary>
    public interface ILauncher
    {
        /// <summary>Launches a file explorer and displays the contents of the specified folder.</summary>
        /// <returns>The result of the operation.</returns>
        /// <param name="folder">The folder to display in a file explorer.</param>
        Task<bool> LaunchFolderAsync(IStorageFolder folder);

        Task<bool> LaunchUriAsync(Uri uri);

        Task<LaunchQuerySupportStatus> QueryUriSupportAsync(Uri uri);

        /// <summary>Starts the default app associated with the specified file.</summary>
        /// <returns>The launch operation.</returns>
        /// <param name="file">The file.</param>
        Task<bool> LaunchFileAsync(IStorageFile file);
    }
}