namespace XPlat.Storage
{
    using System;

    using XPlat.Storage.FileProperties;

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

        public static Windows.Storage.ApplicationDataLocality ToApplicationDataLocality(
            this ApplicationDataLocality locality)
        {
            switch (locality)
            {
                case ApplicationDataLocality.Roaming:
                    return Windows.Storage.ApplicationDataLocality.Roaming;
                case ApplicationDataLocality.Temporary:
                    return Windows.Storage.ApplicationDataLocality.Temporary;
                case ApplicationDataLocality.LocalCache:
                    return Windows.Storage.ApplicationDataLocality.LocalCache;
                default:
                    return Windows.Storage.ApplicationDataLocality.Local;
            }
        }

        public static ApplicationDataLocality ToApplicationDataLocality(
            this Windows.Storage.ApplicationDataLocality locality)
        {
            switch (locality)
            {
                case Windows.Storage.ApplicationDataLocality.Roaming:
                    return ApplicationDataLocality.Roaming;
                case Windows.Storage.ApplicationDataLocality.Temporary:
                    return ApplicationDataLocality.Temporary;
                case Windows.Storage.ApplicationDataLocality.LocalCache:
                    return ApplicationDataLocality.LocalCache;
                default:
                    return ApplicationDataLocality.Local;
            }
        }

        public static Windows.Storage.ApplicationDataCreateDisposition ToApplicationDataCreateDisposition(
            this ApplicationDataCreateDisposition disposition)
        {
            switch (disposition)
            {
                case ApplicationDataCreateDisposition.Existing:
                    return Windows.Storage.ApplicationDataCreateDisposition.Existing;
                default:
                    return Windows.Storage.ApplicationDataCreateDisposition.Always;
            }
        }

        public static IBasicProperties ToBasicProperties(this Windows.Storage.FileProperties.BasicProperties properties)
        {
            return new BasicProperties(properties.DateModified.DateTime, properties.Size);
        }
    }
}