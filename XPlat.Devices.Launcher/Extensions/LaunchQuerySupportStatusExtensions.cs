namespace XPlat.Device.Extensions
{
    using System;

    public static class LaunchQuerySupportStatusExtensions
    {
#if WINDOWS_UWP
        public static LaunchQuerySupportStatus ToInternalLaunchQuerySupportStatus(
            this Windows.System.LaunchQuerySupportStatus status)
        {
            switch (status)
            {
                case Windows.System.LaunchQuerySupportStatus.Available:
                    return LaunchQuerySupportStatus.Available;
                case Windows.System.LaunchQuerySupportStatus.AppNotInstalled:
                    return LaunchQuerySupportStatus.AppNotInstalled;
                case Windows.System.LaunchQuerySupportStatus.AppUnavailable:
                    return LaunchQuerySupportStatus.AppUnavailable;
                case Windows.System.LaunchQuerySupportStatus.NotSupported:
                    return LaunchQuerySupportStatus.NotSupported;
                case Windows.System.LaunchQuerySupportStatus.Unknown:
                    return LaunchQuerySupportStatus.Unknown;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
#endif
    }
}