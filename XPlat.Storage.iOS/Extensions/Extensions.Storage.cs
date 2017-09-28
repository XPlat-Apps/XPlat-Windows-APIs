namespace XPlat.Storage
{
    using System.Threading.Tasks;

    public static partial class Extensions
    {
        public static FileAttributes AsFileAttributes(this System.IO.FileAttributes attributes)
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

        public static Task ClearAsync(this IStorageFolder folder)
        {
            return Task.Run(
                async () =>
                    {
                        foreach (IStorageFolder subfolder in await folder.GetFoldersAsync())
                        {
                            await subfolder.DeleteAsync();
                        }

                        foreach (IStorageFile file in await folder.GetFilesAsync())
                        {
                            await file.DeleteAsync();
                        }
                    });
        }
    }
}