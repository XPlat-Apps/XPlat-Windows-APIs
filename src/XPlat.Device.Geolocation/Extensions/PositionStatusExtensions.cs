// XPlat Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XPlat.Device.Geolocation.Extensions
{
    using System;

    /// <summary>
    /// Defines a collection of extensions for the PositionStatus enum.
    /// </summary>
    public static class PositionStatusExtensions
    {
#if WINDOWS_UWP
        /// <summary>
        /// Converts the Windows PositionStatus enum to the internal XPlat equivalent.
        /// </summary>
        /// <param name="status">The Windows PositionStatus to convert.</param>
        /// <returns>Returns the equivalent XPlat PositionStatus value.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown if the <paramref name="status"/> is not supported.</exception>
        public static PositionStatus ToInternalPositionStatus(this Windows.Devices.Geolocation.PositionStatus status)
        {
            return status switch
            {
                Windows.Devices.Geolocation.PositionStatus.Ready => PositionStatus.Ready,
                Windows.Devices.Geolocation.PositionStatus.Initializing => PositionStatus.Initializing,
                Windows.Devices.Geolocation.PositionStatus.NoData => PositionStatus.NoData,
                Windows.Devices.Geolocation.PositionStatus.Disabled => PositionStatus.Disabled,
                Windows.Devices.Geolocation.PositionStatus.NotInitialized => PositionStatus.NotInitialized,
                Windows.Devices.Geolocation.PositionStatus.NotAvailable => PositionStatus.NotAvailable,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }

        /// <summary>
        /// Converts the XPlat PositionStatus enum to the Windows equivalent.
        /// </summary>
        /// <param name="status">The XPlat PositionStatus to convert.</param>
        /// <returns>Returns the equivalent Windows PositionStatus value.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">Thrown if the <paramref name="status"/> is not supported.</exception>
        public static Windows.Devices.Geolocation.PositionStatus ToWindowsPositionStatus(this PositionStatus status)
        {
            return status switch
            {
                PositionStatus.Ready => Windows.Devices.Geolocation.PositionStatus.Ready,
                PositionStatus.Initializing => Windows.Devices.Geolocation.PositionStatus.Initializing,
                PositionStatus.NoData => Windows.Devices.Geolocation.PositionStatus.NoData,
                PositionStatus.Disabled => Windows.Devices.Geolocation.PositionStatus.Disabled,
                PositionStatus.NotInitialized => Windows.Devices.Geolocation.PositionStatus.NotInitialized,
                PositionStatus.NotAvailable => Windows.Devices.Geolocation.PositionStatus.NotAvailable,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
            };
        }
#endif
    }
}