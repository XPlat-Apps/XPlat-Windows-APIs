namespace XPlat.Device.Geolocation.Extensions
{
    using System;

    /// <summary>
    /// Defines a collection of extensions for the GeolocationAccessStatus enum.
    /// </summary>
    public static class GeolocationAccessStatusExtensions
    {
#if WINDOWS_UWP
        /// <summary>
        /// Converts the Windows GeolocationAccessStatus enum to the internal XPlat equivalent.
        /// </summary>
        /// <param name="status">The Windows GeolocationAccessStatus to convert.</param>
        /// <returns>Returns the equivalent XPlat GeolocationAccessStatus value.</returns>
        public static GeolocationAccessStatus ToInternalGeolocationAccessStatus(
            this Windows.Devices.Geolocation.GeolocationAccessStatus status)
        {
            switch (status)
            {
                case Windows.Devices.Geolocation.GeolocationAccessStatus.Unspecified:
                    return GeolocationAccessStatus.Unspecified;
                case Windows.Devices.Geolocation.GeolocationAccessStatus.Allowed:
                    return GeolocationAccessStatus.Allowed;
                case Windows.Devices.Geolocation.GeolocationAccessStatus.Denied:
                    return GeolocationAccessStatus.Denied;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

        /// <summary>
        /// Converts the XPlat GeolocationAccessStatus enum to the Windows equivalent.
        /// </summary>
        /// <param name="status">The XPlat GeolocationAccessStatus to convert.</param>
        /// <returns>Returns the equivalent Windows GeolocationAccessStatus value.</returns>
        public static Windows.Devices.Geolocation.GeolocationAccessStatus ToWindowsGeolocationAccessStatus(
            this GeolocationAccessStatus status)
        {
            switch (status)
            {
                case GeolocationAccessStatus.Unspecified:
                    return Windows.Devices.Geolocation.GeolocationAccessStatus.Unspecified;
                case GeolocationAccessStatus.Allowed:
                    return Windows.Devices.Geolocation.GeolocationAccessStatus.Allowed;
                case GeolocationAccessStatus.Denied:
                    return Windows.Devices.Geolocation.GeolocationAccessStatus.Denied;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
#endif
    }
}