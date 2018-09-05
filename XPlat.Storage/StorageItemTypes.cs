namespace XPlat.Storage
{
    using System;

    /// <summary>Describes whether an item that implements the IStorageItem interface is a file or a folder.</summary>
    [Flags]
    public enum StorageItemTypes
    {
        /// <summary>A storage item that is neither a file nor a folder.</summary>
        None = 0,

        /// <summary>A file that is represented as a StorageFile instance.</summary>
        File = 1,

        /// <summary>A folder that is represented as a StorageFolder instance.</summary>
        Folder = 2,
    }
}