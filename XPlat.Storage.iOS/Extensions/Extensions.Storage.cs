namespace XPlat.Storage
{
    public static partial class Extensions
    {
        public static FileAttributes AsFileAttributes(this System.IO.FileAttributes attributes)
        {
            var result = FileAttributes.Normal;

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
    }
}