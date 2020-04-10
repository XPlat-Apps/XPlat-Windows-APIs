namespace XPlat.Storage.Extensions
{
    using System;

    public static class CreationCollisionOptionExtensions
    {
#if WINDOWS_UWP
        public static CreationCollisionOption ToInternalCreationCollisionOption(
            this Windows.Storage.CreationCollisionOption option)
        {
            switch (option)
            {
                case Windows.Storage.CreationCollisionOption.GenerateUniqueName:
                    return CreationCollisionOption.GenerateUniqueName;
                case Windows.Storage.CreationCollisionOption.ReplaceExisting:
                    return CreationCollisionOption.ReplaceExisting;
                case Windows.Storage.CreationCollisionOption.FailIfExists:
                    return CreationCollisionOption.FailIfExists;
                case Windows.Storage.CreationCollisionOption.OpenIfExists:
                    return CreationCollisionOption.OpenIfExists;
                default:
                    throw new ArgumentOutOfRangeException(nameof(option), option, null);
            }
        }

        public static Windows.Storage.CreationCollisionOption ToWindowsCreationCollisionOption(
            this CreationCollisionOption option)
        {
            switch (option)
            {
                case CreationCollisionOption.GenerateUniqueName:
                    return Windows.Storage.CreationCollisionOption.GenerateUniqueName;
                case CreationCollisionOption.ReplaceExisting:
                    return Windows.Storage.CreationCollisionOption.ReplaceExisting;
                case CreationCollisionOption.FailIfExists:
                    return Windows.Storage.CreationCollisionOption.FailIfExists;
                case CreationCollisionOption.OpenIfExists:
                    return Windows.Storage.CreationCollisionOption.OpenIfExists;
                default:
                    throw new ArgumentOutOfRangeException(nameof(option), option, null);
            }
        }
#endif
    }
}