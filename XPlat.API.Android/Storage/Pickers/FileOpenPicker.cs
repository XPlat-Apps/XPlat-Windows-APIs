namespace XPlat.API.Storage.Pickers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Android.Content;

    /// <summary>
    /// Defines a service for a UI file open picker.
    /// </summary>
    public class FileOpenPicker : IFileOpenPicker
    {
        private Context context;

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
            throw new System.NotImplementedException();
        }

        /// <inheritdoc />
        public Task<IReadOnlyList<IStorageFile>> PickMultipleFilesAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}