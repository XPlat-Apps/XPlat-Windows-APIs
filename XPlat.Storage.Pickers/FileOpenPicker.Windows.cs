#if WINDOWS_UWP
namespace XPlat.Storage.Pickers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Windows.Storage.Pickers;

    /// <summary>Represents a UI element that lets the user choose and open files.</summary>
    public class FileOpenPicker : IFileOpenPicker
    {
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

        /// <summary>Shows the file picker so that the user can pick multiple files. (Windows Store app)</summary>
        /// <returns>When the call to this method completes successfully, it returns a filePickerSelectedFilesArray object that contains all the files that were picked by the user. Picked files in this array are represented by storageFile objects.</returns>
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
#endif