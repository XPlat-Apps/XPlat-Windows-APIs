namespace XPlat.Storage.Extensions
{
    public static class FileAttributesExtensions
    {
        public static FileAttributes ToInternalFileAttributes(this System.IO.FileAttributes attributes)
        {
            FileAttributes result = FileAttributes.Normal;

            if (attributes.HasFlag(System.IO.FileAttributes.ReadOnly))
            {
                result |= FileAttributes.ReadOnly;
            }

            if (attributes.HasFlag(System.IO.FileAttributes.Directory))
            {
                result |= FileAttributes.Directory;
            }

            if (attributes.HasFlag(System.IO.FileAttributes.Archive))
            {
                result |= FileAttributes.Archive;
            }

            if (attributes.HasFlag(System.IO.FileAttributes.Temporary))
            {
                result |= FileAttributes.Temporary;
            }

            return result;
        }

        public static System.IO.FileAttributes ToSystemFileAttributes(this FileAttributes attributes)
        {
            System.IO.FileAttributes result = System.IO.FileAttributes.Normal;

            if (attributes.HasFlag(FileAttributes.ReadOnly))
            {
                result |= System.IO.FileAttributes.ReadOnly;
            }

            if (attributes.HasFlag(FileAttributes.Directory))
            {
                result |= System.IO.FileAttributes.Directory;
            }

            if (attributes.HasFlag(FileAttributes.Archive))
            {
                result |= System.IO.FileAttributes.Archive;
            }

            if (attributes.HasFlag(FileAttributes.Temporary))
            {
                result |= System.IO.FileAttributes.Temporary;
            }

            return result;
        }
    }
}