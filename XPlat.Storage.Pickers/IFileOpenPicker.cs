namespace XPlat.Storage.Pickers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>Represents a UI element that lets the user choose and open files.</summary>
    public interface IFileOpenPicker
    {
        /// <summary>Gets the collection of file types that the file open picker displays.</summary>
        IList<string> FileTypeFilter { get; }

        /// <summary>Shows the file picker so that the user can pick one file.</summary>
        /// <returns>When the call to this method completes successfully, it returns a StorageFile object that represents the file that the user picked.</returns>
        Task<IStorageFile> PickSingleFileAsync();

        /// <summary>Shows the file picker so that the user can pick multiple files. (Windows Store app)</summary>
        /// <returns>When the call to this method completes successfully, it returns a filePickerSelectedFilesArray object that contains all the files that were picked by the user. Picked files in this array are represented by storageFile objects.</returns>
        Task<IReadOnlyList<IStorageFile>> PickMultipleFilesAsync();
    }
}