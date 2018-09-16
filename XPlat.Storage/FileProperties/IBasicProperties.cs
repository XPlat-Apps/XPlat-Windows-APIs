namespace XPlat.Storage.FileProperties
{
    using System;

    /// <summary>Provides access to the basic properties, like the size of the item or the date the item was last modified, of the item (like a file or folder).</summary>
    public interface IBasicProperties
    {
        /// <summary>Gets the timestamp of the last time the file was modified.</summary>
        DateTime DateModified { get; }

        /// <summary>Gets the size of the file.</summary>
        ulong Size { get; }
    }
}