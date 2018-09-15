namespace XPlat.Storage.Extensions
{
    using System;

    public static class FileAccessModeExtensions
    {
#if WINDOWS_UWP
        public static FileAccessMode ToInternalFileAccessMode(this Windows.Storage.FileAccessMode mode)
        {
            switch (mode)
            {
                case Windows.Storage.FileAccessMode.Read:
                    return FileAccessMode.Read;
                case Windows.Storage.FileAccessMode.ReadWrite:
                    return FileAccessMode.ReadWrite;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        public static Windows.Storage.FileAccessMode ToWindowsFileAccessMode(this FileAccessMode mode)
        {
            switch (mode)
            {
                case FileAccessMode.Read:
                    return Windows.Storage.FileAccessMode.Read;
                case FileAccessMode.ReadWrite:
                    return Windows.Storage.FileAccessMode.ReadWrite;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
#endif
    }
}