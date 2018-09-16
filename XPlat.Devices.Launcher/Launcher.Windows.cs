#if WINDOWS_UWP
namespace XPlat.Devices
{
    using System;
    using System.Threading.Tasks;
    using Windows.System;
    using XPlat.Devices.Extensions;
    using XPlat.Storage;

    /// <summary>Starts the default app associated with the specified file or URI.</summary>
    public class Launcher : ILauncher
    {
        /// <summary>Launches a file explorer and displays the contents of the specified folder.</summary>
        /// <returns>The result of the operation.</returns>
        /// <param name="folder">The folder to display in a file explorer.</param>
        public async Task<bool> LaunchFolderAsync(IStorageFolder folder)
        {
            Windows.Storage.StorageFolder windowsFolder = await Windows.Storage.StorageFolder.GetFolderFromPathAsync(folder.Path);

            return await Windows.System.Launcher.LaunchFolderAsync(windowsFolder);
        }

        public async Task<bool> LaunchUriAsync(Uri uri)
        {
            return await Windows.System.Launcher.LaunchUriAsync(uri);
        }

        public async Task<LaunchQuerySupportStatus> QueryUriSupportAsync(Uri uri)
        {
            Windows.System.LaunchQuerySupportStatus status = await Windows.System.Launcher.QueryUriSupportAsync(uri, LaunchQuerySupportType.Uri);
            return status.ToInternalLaunchQuerySupportStatus();
        }

        /// <summary>Starts the default app associated with the specified file.</summary>
        /// <returns>The launch operation.</returns>
        /// <param name="file">The file.</param>
        public async Task<bool> LaunchFileAsync(IStorageFile file)
        {
            Windows.Storage.StorageFile windowsFile = await Windows.Storage.StorageFile.GetFileFromPathAsync(file.Path);
            return await Windows.System.Launcher.LaunchFileAsync(windowsFile);
        }
    }
}
#endif