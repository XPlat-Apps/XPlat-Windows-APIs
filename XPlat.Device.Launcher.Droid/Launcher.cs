namespace XPlat.Device
{
    using System;
    using System.Threading.Tasks;

    using Android.Content;

    using XPlat.Storage;

    public static class Launcher
    {
        public static Task<bool> LaunchUriAsync(Context context, Uri uri)
        {
            return Task.Run(
                () =>
                    {
                        bool result = false;

                        try
                        {
                            Intent intent = new Intent(Intent.ActionView);
                            intent.SetData(Android.Net.Uri.Parse(uri.ToString()));
                            context.StartActivity(intent);
                            result = true;
                        }
                        catch (Exception ex)
                        {
#if DEBUG
                            System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif
                        }

                        return result;
                    });
        }

        public static Task<LaunchQuerySupportStatus> QueryUriSupportAsync(Context context, Uri uri)
        {
            return Task.Run(
                () =>
                    {
                        LaunchQuerySupportStatus result = LaunchQuerySupportStatus.Unknown;

                        try
                        {
                            Intent intent = new Intent(Intent.ActionRun);
                            intent.SetData(Android.Net.Uri.Parse(uri.ToString()));
                            result = intent.ResolveActivity(context.PackageManager) != null
                                         ? LaunchQuerySupportStatus.Available
                                         : LaunchQuerySupportStatus.AppNotInstalled;
                        }
                        catch (ActivityNotFoundException anfe)
                        {
                            result = LaunchQuerySupportStatus.NotSupported;
#if DEBUG
                            System.Diagnostics.Debug.WriteLine(anfe.ToString());
#endif
                        }
                        catch (Exception ex)
                        {
#if DEBUG
                            System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif
                        }

                        return result;
                    });
        }

        /// <summary>
        /// Starts the default app associated with the specified file.
        /// </summary>
        /// <param name="context">
        /// The application context.
        /// </param>
        /// <param name="file">
        /// The file.
        /// </param>
        /// <returns>
        /// The launch operation.
        /// </returns>
        public static Task<bool> LaunchFileAsync(Context context, IStorageFile file)
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

                                Intent intent = new Intent(Intent.ActionView);
                                intent.SetDataAndType(Android.Net.Uri.Parse(file.Path), fileContentType);
                                intent.SetFlags(ActivityFlags.ClearTop);
                                context.StartActivity(intent);
                                result = true;
                            }
                        }
                        catch (Exception ex)
                        {
#if DEBUG
                            System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif
                        }

                        return result;
                    });
        }

        public static Task<bool> LaunchFolderAsync(Context context, IStorageFolder folder)
        {
            return Task.Run(
                () =>
                    {
                        bool result = false;

                        try
                        {
                            if (folder != null && folder.Exists)
                            {
                                Intent intent = new Intent(Intent.ActionView);
                                intent.SetDataAndType(Android.Net.Uri.Parse(folder.Path), "*/*");
                                intent.SetFlags(ActivityFlags.ClearTop);
                                context.StartActivity(intent);
                                result = true;
                            }
                        }
                        catch (Exception ex)
                        {
#if DEBUG
                            System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif
                        }

                        return result;
                    });
        }
    }
}