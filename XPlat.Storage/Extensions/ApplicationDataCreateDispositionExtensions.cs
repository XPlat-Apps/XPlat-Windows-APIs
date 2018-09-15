namespace XPlat.Storage.Extensions
{
    using System;

    public static class ApplicationDataCreateDispositionExtensions
    {
#if WINDOWS_UWP
        public static ApplicationDataCreateDisposition ToInternalApplicationDataCreateDisposition(
            this Windows.Storage.ApplicationDataCreateDisposition disposition)
        {
            switch (disposition)
            {
                case Windows.Storage.ApplicationDataCreateDisposition.Always:
                    return ApplicationDataCreateDisposition.Always;
                case Windows.Storage.ApplicationDataCreateDisposition.Existing:
                    return ApplicationDataCreateDisposition.Existing;
                default:
                    throw new ArgumentOutOfRangeException(nameof(disposition), disposition, null);
            }
        }

        public static Windows.Storage.ApplicationDataCreateDisposition ToWindowsApplicationDataCreateDisposition(
            this ApplicationDataCreateDisposition disposition)
        {
            switch (disposition)
            {
                case ApplicationDataCreateDisposition.Always:
                    return Windows.Storage.ApplicationDataCreateDisposition.Always;
                case ApplicationDataCreateDisposition.Existing:
                    return Windows.Storage.ApplicationDataCreateDisposition.Existing;
                default:
                    throw new ArgumentOutOfRangeException(nameof(disposition), disposition, null);
            }
        }
#endif
    }
}