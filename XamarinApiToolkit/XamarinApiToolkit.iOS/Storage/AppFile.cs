using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinApiToolkit.Storage
{
    using System.IO;
    using System.Threading.Tasks;

    public sealed class AppFile : IAppFile
    {
        public AppFile(IAppFolder parentFolder, string path)
        {
            this.Parent = parentFolder;
            this.Path = path;
        }

        public DateTime DateCreated { get; }

        public string Name { get; }

        public string Path { get; }

        public bool Exists { get; }

        public IAppFolder Parent { get; }

        public Task RenameAsync(string desiredNewName)
        {
            throw new NotImplementedException();
        }

        public Task RenameAsync(string desiredNewName, FileStoreNameCollisionOption option)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDictionary<string, object>> GetPropertiesAsync()
        {
            throw new NotImplementedException();
        }

        public bool IsOfType(FileStoreItemTypes type)
        {
            throw new NotImplementedException();
        }

        public string FileType { get; }

        public Task<Stream> OpenAsync(FileAccessOption accessMode)
        {
            throw new NotImplementedException();
        }

        public Task<IAppFile> CopyAsync(IAppFolder destinationFolder)
        {
            throw new NotImplementedException();
        }

        public Task<IAppFile> CopyAsync(IAppFolder destinationFolder, string desiredNewName)
        {
            throw new NotImplementedException();
        }

        public Task<IAppFile> CopyAsync(IAppFolder destinationFolder, string desiredNewName, FileStoreNameCollisionOption option)
        {
            throw new NotImplementedException();
        }

        public Task CopyAndReplaceAsync(IAppFile fileToReplace)
        {
            throw new NotImplementedException();
        }

        public Task MoveAsync(IAppFolder destinationFolder)
        {
            throw new NotImplementedException();
        }

        public Task MoveAsync(IAppFolder destinationFolder, string desiredNewName)
        {
            throw new NotImplementedException();
        }

        public Task MoveAsync(IAppFolder destinationFolder, string desiredNewName, FileStoreNameCollisionOption option)
        {
            throw new NotImplementedException();
        }

        public Task MoveAndReplaceAsync(IAppFile fileToReplace)
        {
            throw new NotImplementedException();
        }

        public Task WriteTextAsync(string text)
        {
            throw new NotImplementedException();
        }

        public Task<string> ReadTextAsync()
        {
            throw new NotImplementedException();
        }
    }
}
