namespace XPlat.Storage
{
    using System;

    /// <summary>Describes the attributes of a file or folder.</summary>
    [Flags]
    public enum FileAttributes
    {
        /// <summary>The item is normal. That is, the item doesn't have any of the other values in the enumeration.</summary>
        Normal = 0,

        /// <summary>The item is read-only.</summary>
        ReadOnly = 1,

        /// <summary>The item is a directory.</summary>
        Directory = 16,

        /// <summary>The item is archived.</summary>
        Archive = 32,

        /// <summary>The item is a temporary file.</summary>
        Temporary = 256,

        /// <summary>The item is locally incomplete. Windows only.</summary>
        LocallyIncomplete = 512,
    }
}