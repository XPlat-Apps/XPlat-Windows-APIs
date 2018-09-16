#if __IOS__
namespace XPlat.Storage.FileProperties
{
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>Provides access to the basic properties, like the size of the item or the date the item was last modified, of the item (like a file or folder).</summary>
    public class BasicProperties : IBasicProperties
    {
        private readonly string filePath;

        private readonly bool isFolder;

        internal BasicProperties(string filePath)
            : this(filePath, false)
        {
        }

        internal BasicProperties(string filePath, bool isFolder)
        {
            this.filePath = filePath;
            this.isFolder = isFolder;
        }

        /// <summary>Gets the timestamp of the last time the file was modified.</summary>
        public DateTime DateModified =>
            this.isFolder ? Directory.GetLastWriteTime(this.filePath) : File.GetCreationTime(this.filePath);

        /// <summary>Gets the size of the file.</summary>
        public ulong Size
        {
            get
            {
                if (this.isFolder)
                {
                    string[] files = Directory.GetFiles(this.filePath, "*.*");
                    long length = files.Select(file => new FileInfo(file)).Select(fileInfo => fileInfo.Length).Sum();
                    return (ulong) length;
                }

                FileInfo info = new FileInfo(this.filePath);
                return (ulong) info.Length;
            }
        }
    }
}
#endif