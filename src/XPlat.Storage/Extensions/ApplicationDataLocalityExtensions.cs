namespace XPlat.Storage.Extensions
{
    using System;

    public static class ApplicationDataLocalityExtensions
    {
#if WINDOWS_UWP

        public static ApplicationDataLocality ToInternalApplicationDataLocality(
            this Windows.Storage.ApplicationDataLocality locality)
        {
            switch (locality)
            {
                case Windows.Storage.ApplicationDataLocality.Local:
                    return ApplicationDataLocality.Local;
                case Windows.Storage.ApplicationDataLocality.Roaming:
                    return ApplicationDataLocality.Roaming;
                case Windows.Storage.ApplicationDataLocality.Temporary:
                    return ApplicationDataLocality.Temporary;
                case Windows.Storage.ApplicationDataLocality.LocalCache:
                    return ApplicationDataLocality.LocalCache;
                default:
                    throw new ArgumentOutOfRangeException(nameof(locality), locality, null);
            }
        }

        public static Windows.Storage.ApplicationDataLocality ToWindowsApplicationDataLocality(
            this ApplicationDataLocality locality)
        {
            switch (locality)
            {
                case ApplicationDataLocality.Local:
                    return Windows.Storage.ApplicationDataLocality.Local;
                case ApplicationDataLocality.Shared:
                case ApplicationDataLocality.Roaming:
                    return Windows.Storage.ApplicationDataLocality.Roaming;
                case ApplicationDataLocality.Temporary:
                    return Windows.Storage.ApplicationDataLocality.Temporary;
                case ApplicationDataLocality.LocalCache:
                    return Windows.Storage.ApplicationDataLocality.LocalCache;
                default:
                    throw new ArgumentOutOfRangeException(nameof(locality), locality, null);
            }
        }
#endif
    }
}