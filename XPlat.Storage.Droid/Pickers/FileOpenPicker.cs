namespace XPlat.Storage.Pickers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Android.App;
    using Android.Content;

    using XPlat.Foundation;

    /// <summary>
    /// Defines a service for a UI file open picker.
    /// </summary>
    public class FileOpenPicker : IFileOpenPicker
    {
        private readonly Context context;

        private int requestId;

        private TaskCompletionSource<IStorageFile> currentSingleTcs;

        private TaskCompletionSource<IReadOnlyList<IStorageFile>> currentMultiTcs;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileOpenPicker"/> class.
        /// </summary>
        /// <param name="context">
        /// The Android context.
        /// </param>
        public FileOpenPicker(Context context)
        {
            this.context = context;
            this.FileTypeFilter = new List<string>();
        }

        /// <inheritdoc />
        public IList<string> FileTypeFilter { get; }

        /// <inheritdoc />
        public Task<IStorageFile> PickSingleFileAsync()
        {
            var id = this.GenerateRequestId();

            var newTcs = new TaskCompletionSource<IStorageFile>(id);
            if (Interlocked.CompareExchange(ref this.currentSingleTcs, newTcs, null) != null)
            {
                throw new InvalidOperationException("Cannot activate multiple requests for files.");
            }

            this.context.StartActivity(this.GenerateIntent(id, false));

            TypedEventHandler<Activity, FileOpenPickerFilesReceived> handler = null;
            handler = (sender, args) =>
                {
                    var tcs = Interlocked.Exchange(ref this.currentSingleTcs, null);

                    FileOpenPickerActivity.FilesReceived -= handler;

                    if (args.RequestId != id)
                    {
                        return;
                    }

                    tcs.SetResult(args.Cancel ? null : args.Files?.FirstOrDefault());
                };

            FileOpenPickerActivity.FilesReceived += handler;

            return newTcs.Task;
        }

        /// <inheritdoc />
        public Task<IReadOnlyList<IStorageFile>> PickMultipleFilesAsync()
        {
            var id = this.GenerateRequestId();

            var multiTcs = new TaskCompletionSource<IReadOnlyList<IStorageFile>>(id);
            if (Interlocked.CompareExchange(ref this.currentMultiTcs, multiTcs, null) != null)
            {
                throw new InvalidOperationException("Cannot activate multiple requests for files.");
            }

            this.context.StartActivity(this.GenerateIntent(id, true));

            TypedEventHandler<Activity, FileOpenPickerFilesReceived> handler = null;
            handler = (sender, args) =>
                {
                    var tcs = Interlocked.Exchange(ref this.currentMultiTcs, null);

                    FileOpenPickerActivity.FilesReceived -= handler;

                    if (args.RequestId != id)
                    {
                        return;
                    }

                    if (args.Cancel)
                    {
                        tcs.SetResult(null);
                    }
                    else
                    {
                        tcs.SetResult(
                            args.Files == null ? new List<IStorageFile>() : new List<IStorageFile>(args.Files));
                    }
                };

            FileOpenPickerActivity.FilesReceived += handler;

            return multiTcs.Task;
        }

        private Intent GenerateIntent(int id, bool allowMultiple)
        {
            var fileOpenPickerIntent = new Intent(this.context, typeof(FileOpenPickerActivity));
            fileOpenPickerIntent.PutExtra(FileOpenPickerActivity.IntentId, id);

            var mimeTypes = string.Join("|", MimeTypeHelper.GetMimeTypes(this.FileTypeFilter));
            if (string.IsNullOrWhiteSpace(mimeTypes))
            {
                throw new InvalidOperationException("Cannot request file picker without any file type filters.");
            }

            fileOpenPickerIntent.PutExtra(FileOpenPickerActivity.IntentType, mimeTypes);
            fileOpenPickerIntent.PutExtra(FileOpenPickerActivity.IntentAction, Intent.ActionGetContent);

            if (allowMultiple)
            {
                fileOpenPickerIntent.PutExtra(Intent.ExtraAllowMultiple, true);
            }

            return fileOpenPickerIntent;
        }

        private int GenerateRequestId()
        {
            // Gets a code that will be used for the intent.
            if (this.requestId == int.MaxValue)
            {
                this.requestId = 0;
            }

            return this.requestId++;
        }
    }
}