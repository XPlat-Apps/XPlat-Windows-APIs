namespace XPlat.Storage.FileProperties
{
    using System;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Provides access to the basic properties, like the size of the item or the date the item was last modified, of the item (like a file or folder).
    /// </summary>
    public class BasicProperties : IBasicProperties
    {
        private readonly string filePath;

        private readonly bool isFolder;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicProperties"/> class.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        internal BasicProperties(string filePath)
            : this(filePath, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicProperties"/> class.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <param name="isFolder">
        /// Indicates whether the properties are for a folder.
        /// </param>
        internal BasicProperties(string filePath, bool isFolder)
        {
            this.filePath = filePath;
            this.isFolder = isFolder;
        }

        /// <inheritdoc />
        public DateTime DateModified => this.isFolder
                                            ? Directory.GetLastWriteTime(this.filePath)
                                            : File.GetCreationTime(this.filePath);

        /// <inheritdoc />
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