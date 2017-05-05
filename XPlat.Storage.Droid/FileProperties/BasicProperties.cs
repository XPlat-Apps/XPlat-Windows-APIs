namespace XPlat.Storage.FileProperties
{
    using System;
    using System.IO;
    using System.Linq;

    public class BasicProperties : IBasicProperties
    {
        private readonly string filePath;

        private readonly bool isFolder;

        internal BasicProperties(string filePath) : this(filePath, false)
        {
        }

        internal BasicProperties(string filePath, bool isFolder)
        {
            this.filePath = filePath;
            this.isFolder = isFolder;
        }

        public DateTime DateModified => this.isFolder ? Directory.GetLastWriteTime(this.filePath) : File.GetCreationTime(this.filePath);

        public ulong Size
        {
            get
            {
                if (this.isFolder)
                {
                    var files = Directory.GetFiles(this.filePath, "*.*");
                    var length = files.Select(file => new FileInfo(file)).Select(fileInfo => fileInfo.Length).Sum();
                    return (ulong)length;
                }
                
                var info = new FileInfo(this.filePath);
                return (ulong)info.Length;
            }
        }
    }
}