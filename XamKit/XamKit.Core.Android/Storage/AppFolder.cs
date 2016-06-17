namespace XamKit.Core.Storage
{
    using System;
    using System.Threading.Tasks;

    using XamKit.Core.Common.Storage;

    /// <summary>
    /// Defines the app's root folder for an Android application.
    /// </summary>
    public class AppFolder : IAppFolder
    {
        public AppFolder(string path)
        {
            this.Path = path;
        }

        public string Name { get; }

        public string Path { get; }

        public Task<IAppFile> CreateFileAsync(
            string fileName,
            FileStoreCreationOption creationOption = FileStoreCreationOption.ThrowExceptionIfExists)
        {
            throw new NotImplementedException();
        }

        public Task<IAppFolder> CreateFolderAsync(
            string folderName,
            FileStoreCreationOption creationOption = FileStoreCreationOption.ThrowExceptionIfExists)
        {
            throw new NotImplementedException();
        }

        public Task<IAppFile> GetFileAsync(string fileName, bool createIfNotExisting = false)
        {
            throw new NotImplementedException();
        }

        public Task<IAppFolder> GetFolderAsync(string folderName, bool createIfNotExisting = false)
        {
            throw new NotImplementedException();
        }
    }
}