// <copyright file="PositionStatusExtensions.cs" company="James Croft">
// Copyright (c) James Croft. All rights reserved.
// </copyright>

namespace XPlat.Devices.Geolocation.Extensions
{
    using System;

    public static class PositionStatusExtensions
    {
#if WINDOWS_UWP
        public static PositionStatus ToInternalPositionStatus(this Windows.Devices.Geolocation.PositionStatus status)
        {
            switch (status)
            {
                case Windows.Devices.Geolocation.PositionStatus.Ready:
                    return PositionStatus.Ready;
                case Windows.Devices.Geolocation.PositionStatus.Initializing:
                    return PositionStatus.Initializing;
                case Windows.Devices.Geolocation.PositionStatus.NoData:
                    return PositionStatus.NoData;
                case Windows.Devices.Geolocation.PositionStatus.Disabled:
                    return PositionStatus.Disabled;
                case Windows.Devices.Geolocation.PositionStatus.NotInitialized:
                    return PositionStatus.NotInitialized;
                case Windows.Devices.Geolocation.PositionStatus.NotAvailable:
                    return PositionStatus.NotAvailable;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

        public static Windows.Devices.Geolocation.PositionStatus ToWindowsPositionStatus(this PositionStatus status)
        {
            switch (status)
            {
                case PositionStatus.Ready:
                    return Windows.Devices.Geolocation.PositionStatus.Ready;
                case PositionStatus.Initializing:
                    return Windows.Devices.Geolocation.PositionStatus.Initializing;
                case PositionStatus.NoData:
                    return Windows.Devices.Geolocation.PositionStatus.NoData;
                case PositionStatus.Disabled:
                    return Windows.Devices.Geolocation.PositionStatus.Disabled;
                case PositionStatus.NotInitialized:
                    return Windows.Devices.Geolocation.PositionStatus.NotInitialized;
                case PositionStatus.NotAvailable:
                    return Windows.Devices.Geolocation.PositionStatus.NotAvailable;
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
#endif
    }
}