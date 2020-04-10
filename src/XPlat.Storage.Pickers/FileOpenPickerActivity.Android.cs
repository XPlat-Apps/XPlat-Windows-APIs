﻿#if __ANDROID__
namespace XPlat.Storage.Pickers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.Database;
    using Android.OS;
    using Android.Provider;

    using XPlat.Foundation;

    using Uri = Android.Net.Uri;

    [Activity(ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    internal class FileOpenPickerActivity : Activity
    {
        internal const string IntentId = "id";

        internal const string IntentAction = "action";

        internal const string IntentType = "type";

        internal const string IntentFilePath = "path";

        private int requestCode;

        private string action;

        private string type;

        private string filePath;

        private string[] extraMimes;

        internal static event TypedEventHandler<Activity, FileOpenPickerFilesReceived> FilesReceived;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Bundle bundle = savedInstanceState ?? this.Intent.Extras;

            bool isComplete = bundle.GetBoolean("isComplete", false);
            this.requestCode = bundle.GetInt(IntentId, 0);
            this.action = bundle.GetString(IntentAction);
            this.type = bundle.GetString(IntentType);
            this.extraMimes = bundle.GetStringArray(Intent.ExtraMimeTypes);
            bool allowMultiple = bundle.GetBoolean(Intent.ExtraAllowMultiple, false);

            Intent intent = null;
            try
            {
                intent = new Intent(this.action);
                intent.SetType(this.type);

                if (allowMultiple)
                {
                    intent.PutExtra(Intent.ExtraAllowMultiple, true);
                }

                if (this.extraMimes != null && this.extraMimes.Any())
                {
                    intent.PutExtra(Intent.ExtraMimeTypes, this.extraMimes);
                }

                if (!isComplete)
                {
                    Intent chooser = Intent.CreateChooser(intent, allowMultiple ? "Select files" : "Select a file");
                    this.StartActivityForResult(chooser, this.requestCode);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                FilesReceived?.Invoke(this, new FileOpenPickerFilesReceived(this.requestCode, null, true));
            }
            finally
            {
                intent?.Dispose();
            }
        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (resultCode == Result.Canceled)
            {
                Task<FileOpenPickerFilesReceived> result = Task.FromResult(new FileOpenPickerFilesReceived(requestCode, null, true));
                result.ContinueWith(
                    x =>
                    {
                        FilesReceived?.Invoke(this, x.Result);
                    });
                this.Finish();
            }
            else
            {
                FileOpenPickerFilesReceived args = await this.ExtractFileArgsAsync(this, requestCode, data?.Data);
                FilesReceived?.Invoke(this, args);
                this.Finish();
            }
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutBoolean("isComplete", true);
            outState.PutInt(IntentId, this.requestCode);
            outState.PutString(IntentType, this.type);
            outState.PutString(IntentAction, this.action);

            if (!string.IsNullOrWhiteSpace(this.filePath))
            {
                outState.PutString(IntentFilePath, this.filePath);
            }

            base.OnSaveInstanceState(outState);
        }

        internal async Task<FileOpenPickerFilesReceived> ExtractFileArgsAsync(Context context, int requestCode, Uri data)
        {
            if (data != null)
            {
                string path = await GetFilePathFromUriAsync(context, data);

                StorageFile file = new StorageFile(path);
                if (file.Exists)
                {
                    this.filePath = file.Path;
                    return new FileOpenPickerFilesReceived(requestCode, new List<IStorageFile> { file }, false);
                }
            }

            return new FileOpenPickerFilesReceived(requestCode, null, true);
        }

        private static Task<string> GetFilePathFromUriAsync(Context context, Uri data)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

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
                                string[] projection = new[] { MediaStore.MediaColumns.DisplayName };
                                cursor = context.ContentResolver.Query(data, projection, null, null, null);
                            }
                            catch (Exception)
                            {
                            }

                            if (cursor != null && cursor.MoveToFirst())
                            {
                                int index = cursor.GetColumnIndex(MediaStore.MediaColumns.DisplayName);
                                displayName = cursor.GetString(index);
                            }

                            if (!string.IsNullOrWhiteSpace(displayName))
                            {
                                try
                                {
                                    string filePath =
                                        $"{ApplicationData.Current.TemporaryFolder.Path}/{DateTime.UtcNow.Year}{DateTime.UtcNow.DayOfYear}_{displayName}";

                                    using (Stream input = context.ContentResolver.OpenInputStream(data))
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
#endif