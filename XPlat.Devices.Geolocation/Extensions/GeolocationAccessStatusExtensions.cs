// <copyright file="GeolocationAccessStatusExtensions.cs" company="James Croft">
// Copyright (c) James Croft. All rights reserved.
// </copyright>

namespace XPlat.Devices.Geolocation.Extensions
{
    using System;

    public static class GeolocationAccessStatusExtensions
    {
#if WINDOWS_UWP
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