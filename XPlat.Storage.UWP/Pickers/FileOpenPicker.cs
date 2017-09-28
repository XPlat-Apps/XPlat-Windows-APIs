namespace XPlat.Storage.Pickers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Windows.Storage.Pickers;

    /// <summary>
    /// Defines a service for a UI file open picker.
    /// </summary>
    public class FileOpenPicker : IFileOpenPicker
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileOpenPicker"/> class.
        /// </summary>
        public FileOpenPicker()
        {
            this.FileTypeFilter = new List<string>();
        }

        /// <inheritdoc />
        public IList<string> FileTypeFilter { get; }

        /// <inheritdoc />
        public async Task<IStorageFile> PickSingleFileAsync()
        {
            Windows.Storage.Pickers.FileOpenPicker picker = new Windows.Storage.Pickers.FileOpenPicker { ViewMode = PickerViewMode.List };

            foreach (string fileType in this.FileTypeFilter)
            {
                picker.FileTypeFilter.Add(fileType);
            }

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();
            return file == null ? null : new StorageFile(file);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<IStorageFile>> PickMultipleFilesAsync()
        {
            Windows.Storage.Pickers.FileOpenPicker picker = new Windows.Storage.Pickers.FileOpenPicker { ViewMode = PickerViewMode.List };

            foreach (string fileType in this.FileTypeFilter)
            {
                picker.FileTypeFilter.Add(fileType);
            }

            List<IStorageFile> result = new List<IStorageFile>();

            IReadOnlyList<Windows.Storage.StorageFile> files = await picker.PickMultipleFilesAsync();
            if (files == null)
            {
                return result;
            }

            result.AddRange(from file in files where file != null select new StorageFile(file));

            return result;
        }
    }
}