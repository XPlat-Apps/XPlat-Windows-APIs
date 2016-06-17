namespace XamKit.Core.Storage
{
    using System.IO;
    using System.Threading.Tasks;

    using XamKit.Core.Common.Storage;

    public class AppFile : IAppFile
    {
        public AppFile(string path)
        {
        }

        public string Name { get; }

        public string Path { get; }

        public Task<Stream> OpenAsync(FileAccessOption option = FileAccessOption.ReadOnly)
        {
            throw new System.NotImplementedException();
        }

        public Task MoveAsync(
            IAppFolder destinationFolder,
            string newName = "",
            FileNameCreationOption option = FileNameCreationOption.ReplaceIfExists)
        {
            throw new System.NotImplementedException();
        }

        public Task RenameAsync(
            string fileName,
            FileNameCreationOption option = FileNameCreationOption.ThrowExceptionIfExists)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}