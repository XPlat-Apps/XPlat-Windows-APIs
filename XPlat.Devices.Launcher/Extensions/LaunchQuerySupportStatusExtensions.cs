namespace XPlat.Device.Extensions
{
    using System;

    /// <summary>
    /// Defines a collection of extensions for the LaunchQuerySupportStatus enum.
    /// </summary>
    public static class LaunchQuerySupportStatusExtensions
    {
#if WINDOWS_UWP
        /// <summary>
        /// Converts the Windows LaunchQuerySupportStatus enum to the internal XPlat equivalent.
        /// </summary>
        /// <param name="status">The Windows LaunchQuerySupportStatus to convert.</param>
        /// <returns>Returns the equivalent XPlat LaunchQuerySupportStatus value.</returns>
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

        /// <summary>
        /// Converts the XPlat LaunchQuerySupportStatus enum to the Windows equivalent.
        /// </summary>
        /// <param name="status">The XPlat LaunchQuerySupportStatus to convert.</param>
        /// <returns>Returns the equivalent Windows LaunchQuerySupportStatus value.</returns>
        public static LaunchQuerySupportStatus ToWindowsLaunchQuerySupportStatus(this LaunchQuerySupportStatus status)
        {
            switch (status)
            {
                case LaunchQuerySupportStatus.Available:
                    return LaunchQuerySupportStatus.Available;
                case LaunchQuerySupportStatus.AppNotInstalled:
                    return LaunchQuerySupportStatus.AppNotInstalled;
                case LaunchQuerySupportStatus.AppUnavailable:
                    return LaunchQuerySupportStatus.AppUnavailable;
                case LaunchQuerySupportStatus.NotSupported:
                    return LaunchQuerySupportStatus.NotSupported;
                case LaunchQuerySupportStatus.Unknown:
                    return LaunchQuerySupportStatus.Unknown;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
#endif
    }
}