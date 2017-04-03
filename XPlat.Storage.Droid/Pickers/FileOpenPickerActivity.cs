namespace XPlat.Storage.Pickers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    using Android.App;
    using Android.Content;
    using Android.Database;
    using Android.OS;
    using Android.Provider;

    using XPlat.Foundation;

    using Uri = Android.Net.Uri;

    [Activity]
    internal class FileOpenPickerActivity : Activity
    {
        internal const string IntentId = "id";

        internal const string IntentAction = "action";

        internal const string IntentType = "type";

        internal const string IntentFilePath = "path";

        private int requestId;

        private string action;

        private string type;

        private string filePath;

        internal static event TypedEventHandler<Activity, FileOpenPickerFilesReceived> FilesReceived;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var bundle = savedInstanceState ?? this.Intent.Extras;

            var isComplete = bundle.GetBoolean("isComplete", false);
            this.requestId = bundle.GetInt(IntentId, 0);
            this.action = bundle.GetString(IntentAction);
            this.type = bundle.GetString(IntentType);
            var allowMultiple = bundle.GetBoolean(Intent.ExtraAllowMultiple, false);

            Intent intent = null;
            try
            {
                intent = new Intent(this.action);
                intent.SetType(this.type);

                if (allowMultiple)
                {
                    intent.PutExtra(Intent.ExtraAllowMultiple, true);
                }

                if (!isComplete)
                {
                    this.StartActivityForResult(intent, this.requestId);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif

                FilesReceived?.Invoke(this, new FileOpenPickerFilesReceived(this.requestId, null, true));
            }
            finally
            {
                intent?.Dispose();
            }
        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            Task<FileOpenPickerFilesReceived> result;

            if (resultCode == Result.Canceled)
            {
                result = Task.FromResult(new FileOpenPickerFilesReceived(requestCode, null, true));
                result.ContinueWith(
                    x =>
                        {
                            FilesReceived?.Invoke(this, x.Result);
                        });
                this.Finish();
            }
            else
            {
                var args = await this.ExtractFileArgsAsync(this, requestCode, data?.Data);
                FilesReceived?.Invoke(this, args);
                this.Finish();
            }
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutBoolean("isComplete", true);
            outState.PutInt(IntentId, this.requestId);
            outState.PutString(IntentType, this.type);
            outState.PutString(IntentAction, this.action);

            if (!string.IsNullOrWhiteSpace(this.filePath))
            {
                outState.PutString(IntentFilePath, this.filePath);
            }

            base.OnSaveInstanceState(outState);
        }

        internal async Task<FileOpenPickerFilesReceived> ExtractFileArgsAsync(Context context, int requestId, Uri data)
        {
            if (data != null)
            {
                var path = await GetFilePathFromUriAsync(context, data);

                var file = new StorageFile(ApplicationData.Current.TemporaryFolder, path);
                if (file.Exists)
                {
                    this.filePath = file.Path;
                    return new FileOpenPickerFilesReceived(requestId, new List<IStorageFile> { file }, false);
                }
            }

            return new FileOpenPickerFilesReceived(requestId, null, true);
        }

        private static Task<string> GetFilePathFromUriAsync(Context context, Uri data)
        {
            var tcs = new TaskCompletionSource<string>();

            if (data != null)
            {
                if (data.Scheme.Equals("file", StringComparison.CurrentCultureIgnoreCase))
                {
                    tcs.SetResult(new System.Uri(data.ToString()).LocalPath);
                }
                else if (data.Scheme.Equals("content", StringComparison.CurrentCultureIgnoreCase))
                {
                    Task.Factory.StartNew(() =>
                    {
                        ICursor cursor = null;
                        try
                        {
                            string displayName = string.Empty;

                            try
                            {
                                var projection = new[] { MediaStore.MediaColumns.DisplayName };
                                cursor = context.ContentResolver.Query(data, projection, null, null, null);
                            }
                            catch (Exception)
                            {
                            }

                            if (cursor != null && cursor.MoveToFirst())
                            {
                                var index = cursor.GetColumnIndex(MediaStore.MediaColumns.DisplayName);
                                displayName = cursor.GetString(index);
                            }

                            if (!string.IsNullOrWhiteSpace(displayName))
                            {
                                try
                                {
                                    var filePath =
                                        $"{ApplicationData.Current.TemporaryFolder.Path}/{DateTime.UtcNow.Year}{DateTime.UtcNow.DayOfYear}_{displayName}";

                                    using (var input = context.ContentResolver.OpenInputStream(data))
                                    {
                                        using (Stream output = File.Create(filePath))
                                        {
                                            input.CopyTo(output);
                                        }
                                    }

                                    displayName = filePath;
                                }
                                catch (Exception)
                                {
                                }
                            }

                            tcs.SetResult(displayName);
                        }
                        finally
                        {
                            if (cursor != null)
                            {
                                cursor.Close();
                                cursor.Dispose();
                            }
                        }
                    }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default);
                }
            }
            else
            {
                tcs.SetResult(string.Empty);
            }

            return tcs.Task;
        }
    }
}