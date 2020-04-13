namespace XPlat.Device
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

        /// <summary>
        /// Starts the default app associated with the URI scheme name for the specified URI.
        /// </summary>
        /// <param name="uri">
        /// The URI.
        /// </param>
        /// <returns>
        /// Returns true if the default app for the URI scheme was launched; false otherwise.
        /// </returns>
        Task<bool> LaunchUriAsync(Uri uri);

        /// <summary>
        /// Asynchronously query whether an app can be activated for the specified URI.
        /// </summary>
        /// <param name="uri">
        /// The URI for which to query support.
        /// </param>
        /// <returns>
        /// A value that indicates whether an application is available to launch the URI.
        /// </returns>
        Task<LaunchQuerySupportStatus> QueryUriSupportAsync(Uri uri);

        /// <summary>Starts the default app associated with the specified file.</summary>
        /// <returns>The launch operation.</returns>
        /// <param name="file">The file.</param>
        Task<bool> LaunchFileAsync(IStorageFile file);
    }
}