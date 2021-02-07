#if __ANDROID__
namespace XPlat.Device
{
    using System;
    using System.Threading.Tasks;

    using Android.Content;
    using AndroidX.Core.Content;
    using XPlat.Storage;

    /// <summary>Starts the default app associated with the specified file or URI.</summary>
    public class Launcher : ILauncher
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Launcher"/> class using the default <see cref="Android.App.Application.Context"/>.
        /// </summary>
        public Launcher()
            : this(Android.App.Application.Context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Launcher"/> class.
        /// </summary>
        /// <param name="context">
        /// The Android context.
        /// </param>
        public Launcher(Context context)
        {
            this.Context = context;
        }

        /// <summary>Gets or sets the Android context to be used for launching an Activity with.</summary>
        public Context Context { get; set; }

        /// <summary>Launches a file explorer and displays the contents of the specified folder.</summary>
        /// <returns>The result of the operation.</returns>
        /// <param name="folder">The folder to display in a file explorer.</param>
        public Task<bool> LaunchFolderAsync(IStorageFolder folder)
        {
            return Task.Run(
                () =>
                    {
                        bool result = false;

                        try
                        {
                            if (folder != null && folder.Exists)
                            {
                                var intent = new Intent(Intent.ActionView);
                                intent.SetDataAndType(Android.Net.Uri.Parse(folder.Path), "*/*");
                                intent.SetFlags(ActivityFlags.ClearTop);
                                this.Context.StartActivity(intent);
                                result = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.ToString());
                        }

                        return result;
                    });
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
            return Task.Run(
                () =>
                    {
                        bool result = false;

                        try
                        {
                            var intent = new Intent(Intent.ActionView);
                            intent.SetData(Android.Net.Uri.Parse(uri.ToString()));
                            this.Context.StartActivity(intent);
                            result = true;
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.ToString());
                        }

                        return result;
                    });
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
            return Task.Run(
                () =>
                    {
                        LaunchQuerySupportStatus result = LaunchQuerySupportStatus.Unknown;

                        try
                        {
                            var intent = new Intent(Intent.ActionRun);
                            intent.SetData(Android.Net.Uri.Parse(uri.ToString()));
                            result = intent.ResolveActivity(this.Context.PackageManager) != null
                                         ? LaunchQuerySupportStatus.Available
                                         : LaunchQuerySupportStatus.AppNotInstalled;
                        }
                        catch (ActivityNotFoundException anfe)
                        {
                            result = LaunchQuerySupportStatus.NotSupported;
                            System.Diagnostics.Debug.WriteLine(anfe.ToString());
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.ToString());
                        }

                        return result;
                    });
        }

        /// <summary>Starts the default app associated with the specified file.</summary>
        /// <returns>The launch operation.</returns>
        /// <param name="file">The file.</param>
        public Task<bool> LaunchFileAsync(IStorageFile file)
        {
            return Task.Run(
                () =>
                    {
                        bool result = false;

                        try
                        {
                            if (file != null && file.Exists)
                            {
                                string fileContentType = string.IsNullOrWhiteSpace(file.ContentType)
                                                             ? "*/*"
                                                             : file.ContentType;

                                var javaFile = new Java.IO.File(file.Path);
                                javaFile.SetReadable(true);

                                Android.Net.Uri uri = FileProvider.GetUriForFile(
                                    this.Context,
                                    $"{this.Context.PackageName}",
                                    javaFile);

                                Intent intent = new Intent(Intent.ActionView)
                                    .AddFlags(
                                        ActivityFlags.GrantReadUriPermission | ActivityFlags.ClearTop
                                                                             | ActivityFlags.NewTask).SetDataAndType(
                                        uri,
                                        fileContentType);

                                if (intent.ResolveActivity(this.Context.PackageManager) != null)
                                {
                                    this.Context.StartActivity(intent);
                                    result = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.ToString());
                        }

                        return result;
                    });
        }
    }
}
#endif