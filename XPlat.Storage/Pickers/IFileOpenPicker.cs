namespace XPlat.Storage.Pickers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines an interface for a UI file open picker.
    /// </summary>
    public interface IFileOpenPicker
    {
        /// <summary>
        /// Gets the collection of file types that the file open picker displays.
        /// </summary>
        IList<string> FileTypeFilter { get; }

        /// <summary>
        /// Shows the file picker so that the user can pick one file.
        /// </summary>
        /// <returns>
        /// When the call to this method completes successfully, it returns an IStorageFile object that represents the file that the user picked.
        /// </returns>
        Task<IStorageFile> PickSingleFileAsync();

        /// <summary>
        /// Shows the file picker so that the user can pick multiple files.
        /// </summary>
        /// <returns>
        /// When the call to this method completes successfully, it returns a list of IStorageFile objects that contains all the files that were picked by the user. 
        /// </returns>
        Task<IReadOnlyList<IStorageFile>> PickMultipleFilesAsync();
    }
}