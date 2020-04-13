namespace XPlat.Storage.Extensions
{
    using System;

    public static class NameCollisionOptionExtensions
    {
#if WINDOWS_UWP
        public static NameCollisionOption ToInternalNameCollisionOption(this Windows.Storage.NameCollisionOption option)
        {
            switch (option)
            {
                case Windows.Storage.NameCollisionOption.GenerateUniqueName:
                    return NameCollisionOption.GenerateUniqueName;
                case Windows.Storage.NameCollisionOption.ReplaceExisting:
                    return NameCollisionOption.ReplaceExisting;
                case Windows.Storage.NameCollisionOption.FailIfExists:
                    return NameCollisionOption.FailIfExists;
                default:
                    throw new ArgumentOutOfRangeException(nameof(option), option, null);
            }
        }

        public static Windows.Storage.NameCollisionOption ToWindowsNameCollisionOption(this NameCollisionOption option)
        {
            switch (option)
            {
                case NameCollisionOption.GenerateUniqueName:
                    return Windows.Storage.NameCollisionOption.GenerateUniqueName;
                case NameCollisionOption.ReplaceExisting:
                    return Windows.Storage.NameCollisionOption.ReplaceExisting;
                case NameCollisionOption.FailIfExists:
                    return Windows.Storage.NameCollisionOption.FailIfExists;
                default:
                    throw new ArgumentOutOfRangeException(nameof(option), option, null);
            }
        }
#endif
    }
}