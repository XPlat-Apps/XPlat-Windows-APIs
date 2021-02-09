// XPlat Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown if the <paramref name="status"/> is not supported.</exception>
        public static GeolocationAccessStatus ToInternalGeolocationAccessStatus(
            this Windows.Devices.Geolocation.GeolocationAccessStatus status)
        {
            return status switch
            {
                Windows.Devices.Geolocation.GeolocationAccessStatus.Unspecified => GeolocationAccessStatus.Unspecified,
                Windows.Devices.Geolocation.GeolocationAccessStatus.Allowed => GeolocationAccessStatus.Allowed,
                Windows.Devices.Geolocation.GeolocationAccessStatus.Denied => GeolocationAccessStatus.Denied,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }

        /// <summary>
        /// Converts the XPlat GeolocationAccessStatus enum to the Windows equivalent.
        /// </summary>
        /// <param name="status">The XPlat GeolocationAccessStatus to convert.</param>
        /// <returns>Returns the equivalent Windows GeolocationAccessStatus value.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown if the <paramref name="status"/> is not supported.</exception>
        public static Windows.Devices.Geolocation.GeolocationAccessStatus ToWindowsGeolocationAccessStatus(
            this GeolocationAccessStatus status)
        {
            return status switch
            {
                GeolocationAccessStatus.Unspecified => Windows.Devices.Geolocation.GeolocationAccessStatus.Unspecified,
                GeolocationAccessStatus.Allowed => Windows.Devices.Geolocation.GeolocationAccessStatus.Allowed,
                GeolocationAccessStatus.Denied => Windows.Devices.Geolocation.GeolocationAccessStatus.Denied,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
#endif
    }
}