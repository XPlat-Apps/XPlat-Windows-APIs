#if WINDOWS_UWP
namespace XPlat.Storage.FileProperties
{
    using System;

    /// <summary>Provides access to the basic properties, like the size of the item or the date the item was last modified, of the item (like a file or folder).</summary>
    public class BasicProperties : IBasicProperties
    {
        public BasicProperties(DateTime dateModified, ulong size)
        {
            this.DateModified = dateModified;
            this.Size = size;
        }

        public BasicProperties(Windows.Storage.FileProperties.BasicProperties properties)
        {
            this.DateModified = properties.DateModified.DateTime;
            this.Size = properties.Size;
        }

        /// <summary>Gets the timestamp of the last time the file was modified.</summary>
        public DateTime DateModified { get; }

        /// <summary>Gets the size of the file.</summary>
        public ulong Size { get; }

        public static implicit operator BasicProperties(Windows.Storage.FileProperties.BasicProperties properties)
        {
            return new BasicProperties(properties);
        }
    }
}
#endif