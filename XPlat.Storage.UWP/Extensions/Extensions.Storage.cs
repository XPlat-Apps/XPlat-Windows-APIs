namespace XPlat.Storage
{
    public static partial class Extensions
    {
        public static Windows.Storage.NameCollisionOption ToNameCollisionOption(this NameCollisionOption option)
        {
            switch (option)
            {
                case NameCollisionOption.ReplaceExisting:
                    return Windows.Storage.NameCollisionOption.ReplaceExisting;
                case NameCollisionOption.GenerateUniqueName:
                    return Windows.Storage.NameCollisionOption.GenerateUniqueName;
                default:
                    return Windows.Storage.NameCollisionOption.FailIfExists;
            }
        }

        public static Windows.Storage.FileAccessMode ToFileAccessMode(this FileAccessMode accessMode)
        {
            switch (accessMode)
            {
                case FileAccessMode.ReadWrite:
                    return Windows.Storage.FileAccessMode.ReadWrite;
                default:
                    return Windows.Storage.FileAccessMode.Read;
            }
        }

        public static Windows.Storage.CreationCollisionOption ToCreationCollisionOption(this CreationCollisionOption option)
        {
            switch (option)
            {
                case CreationCollisionOption.GenerateUniqueName:
                    return Windows.Storage.CreationCollisionOption.GenerateUniqueName;
                case CreationCollisionOption.ReplaceExisting:
                    return Windows.Storage.CreationCollisionOption.ReplaceExisting;
                case CreationCollisionOption.OpenIfExists:
                    return Windows.Storage.CreationCollisionOption.OpenIfExists;
                default:
                    return Windows.Storage.CreationCollisionOption.FailIfExists;
            }
        }
    }
}