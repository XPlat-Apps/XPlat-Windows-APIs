namespace XamarinApiToolkit
{
    using Windows.Storage;

    using XamarinApiToolkit.Storage;

    public static partial class Extensions
    {
        public static NameCollisionOption ToNameCollisionOption(this FileStoreNameCollisionOption option)
        {
            switch (option)
            {
                case FileStoreNameCollisionOption.ReplaceExisting:
                    return NameCollisionOption.ReplaceExisting;
                case FileStoreNameCollisionOption.GenerateUniqueName:
                    return NameCollisionOption.GenerateUniqueName;
                default:
                    return NameCollisionOption.FailIfExists;
            }
        }

        public static FileAccessMode ToFileAccessMode(this FileAccessOption accessMode)
        {
            switch (accessMode)
            {
                case FileAccessOption.ReadWrite:
                    return FileAccessMode.ReadWrite;
                default:
                    return FileAccessMode.Read;
            }
        }
    }
}