namespace XPlat.API.Storage
{
    using System.IO;

    using File = TagLib.File;

    internal class StorageFileTagLibAbstraction : File.IFileAbstraction
    {
        public StorageFileTagLibAbstraction(string file)
        {
            this.Name = file;
        }

        public string Name { get; }

        public Stream ReadStream => System.IO.File.Open(this.Name, FileMode.Open, FileAccess.ReadWrite);

        public Stream WriteStream => System.IO.File.Open(this.Name, FileMode.Open, FileAccess.ReadWrite);

        public void CloseStream(Stream stream)

        {
            stream.Close();
        }
    }
}