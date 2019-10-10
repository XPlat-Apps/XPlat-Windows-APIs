#if __ANDROID__
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
    using XPlat.Helpers;
    using XPlat.Storage.Helpers;

    /// <summary>Represents a UI element that lets the user choose and open files.</summary>
    public class FileOpenPicker : IFileOpenPicker
    {
        private static Context AppContext => Android.App.Application.Context;

        private TaskCompletionSource<IStorageFile> currentSingleTcs;

        private TaskCompletionSource<IReadOnlyList<IStorageFile>> currentMultiTcs;

        private int requestCode;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileOpenPicker"/> class.
        /// </summary>
        public FileOpenPicker()
        {
            this.FileTypeFilter = new List<string>();
        }

        /// <summary>Gets the collection of file types that the file open picker displays.</summary>
        public IList<string> FileTypeFilter { get; }

        /// <summary>Shows the file picker so that the user can pick one file.</summary>
        /// <returns>When the call to this method completes successfully, it returns a StorageFile object that represents the file that the user picked.</returns>
        public Task<IStorageFile> PickSingleFileAsync()
        {
            int newRequestCode = RequestCodeHelper.GenerateRequestCode();

            TaskCompletionSource<IStorageFile> newTcs = new TaskCompletionSource<IStorageFile>(newRequestCode);
            if (Interlocked.CompareExchange(ref this.currentSingleTcs, newTcs, null) != null)
            {
                throw new InvalidOperationException("Cannot activate multiple requests for files.");
            }

            this.requestCode = newRequestCode;

            this.AppContext.StartActivity(this.GenerateIntent(this.requestCode, false));

            TypedEventHandler<Activity, FileOpenPickerFilesReceived> handler = null;
            handler = (sender, args) =>
            {
                TaskCompletionSource<IStorageFile> tcs = Interlocked.Exchange(ref this.currentSingleTcs, null);

                FileOpenPickerActivity.FilesReceived -= handler;

                if (args.RequestCode != this.requestCode)
                {
                    return;
                }

                tcs.SetResult(args.Cancel ? null : args.Files?.FirstOrDefault());
            };

            FileOpenPickerActivity.FilesReceived += handler;

            return newTcs.Task;
        }

        /// <summary>Shows the file picker so that the user can pick multiple files. (Windows Store app)</summary>
        /// <returns>When the call to this method completes successfully, it returns a filePickerSelectedFilesArray object that contains all the files that were picked by the user. Picked files in this array are represented by storageFile objects.</returns>
        public Task<IReadOnlyList<IStorageFile>> PickMultipleFilesAsync()
        {
            int newRequestCode = RequestCodeHelper.GenerateRequestCode();

            TaskCompletionSource<IReadOnlyList<IStorageFile>> multiTcs =
                new TaskCompletionSource<IReadOnlyList<IStorageFile>>(newRequestCode);
            if (Interlocked.CompareExchange(ref this.currentMultiTcs, multiTcs, null) != null)
            {
                throw new InvalidOperationException("Cannot activate multiple requests for files.");
            }

            this.requestCode = newRequestCode;

            this.AppContext.StartActivity(this.GenerateIntent(this.requestCode, true));

            TypedEventHandler<Activity, FileOpenPickerFilesReceived> handler = null;
            handler = (sender, args) =>
            {
                TaskCompletionSource<IReadOnlyList<IStorageFile>> tcs =
                    Interlocked.Exchange(ref this.currentMultiTcs, null);

                FileOpenPickerActivity.FilesReceived -= handler;

                if (args.RequestCode != this.requestCode)
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
            Intent fileOpenPickerIntent = new Intent(this.AppContext, typeof(FileOpenPickerActivity));
            fileOpenPickerIntent.PutExtra(FileOpenPickerActivity.IntentId, id);

            IEnumerable<string> mimeTypes = MimeTypeHelper.GetMimeTypes(this.FileTypeFilter);

            fileOpenPickerIntent.PutExtra(FileOpenPickerActivity.IntentType, "*/*");
            fileOpenPickerIntent.PutExtra(Intent.ExtraMimeTypes, mimeTypes.ToArray());
            fileOpenPickerIntent.PutExtra(FileOpenPickerActivity.IntentAction, Intent.ActionOpenDocument);

            if (allowMultiple)
            {
                fileOpenPickerIntent.PutExtra(Intent.ExtraAllowMultiple, true);
            }

            return fileOpenPickerIntent;
        }
    }
}
#endif