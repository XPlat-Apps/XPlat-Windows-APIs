#if __IOS__
namespace XPlat.Device
{
    using System;
    using System.Threading.Tasks;

    using UIKit;

    using XPlat.Storage;

    /// <summary>Starts the default app associated with the specified file or URI.</summary>
    public class Launcher : ILauncher
    {
        /// <summary>Launches a file explorer and displays the contents of the specified folder.</summary>
        /// <returns>The result of the operation.</returns>
        /// <param name="folder">The folder to display in a file explorer.</param>
        public Task<bool> LaunchFolderAsync(IStorageFolder folder)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Starts the default app associated with the URI scheme name for the specified URI.
        /// </summary>
        /// <param name="uri">
        /// The URI.
        /// </param>
        /// <returns>
        /// Returns true if the default app for the URI scheme was launched; false otherwise.
        /// </returns>
        public Task<bool> LaunchUriAsync(Uri uri)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Asynchronously query whether an app can be activated for the specified URI.
        /// </summary>
        /// <param name="uri">
        /// The URI for which to query support.
        /// </param>
        /// <returns>
        /// A value that indicates whether an application is available to launch the URI.
        /// </returns>
        public Task<LaunchQuerySupportStatus> QueryUriSupportAsync(Uri uri)
        {
            LaunchQuerySupportStatus result = LaunchQuerySupportStatus.Unknown;

            try
            {
                bool canOpenUrl = UIApplication.SharedApplication.CanOpenUrl(uri);
                result = canOpenUrl ? LaunchQuerySupportStatus.Available : LaunchQuerySupportStatus.AppNotInstalled;
            }
            catch (Exception ex)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }

            return Task.FromResult(result);
        }

        /// <summary>Starts the default app associated with the specified file.</summary>
        /// <returns>The launch operation.</returns>
        /// <param name="file">The file.</param>
        public Task<bool> LaunchFileAsync(IStorageFile file)
        {
            throw new NotImplementedException();
        }
    }
}
#endif