namespace XPlat.Storage
{
    using System;

    [Flags]
    public enum FileAttributes
    {
        Normal = 0,

        ReadOnly = 1,

        Directory = 16,

        Archive = 32,

        Temporary = 256,

        LocallyIncomplete = 512
    }
}