// <copyright file="GeolocationAccessStatus.cs" company="James Croft">
// Copyright (c) James Croft. All rights reserved.
// </copyright>

namespace XPlat.Devices.Geolocation
{
    /// <summary>Indicates if your app has permission to access location data.</summary>
    public enum GeolocationAccessStatus
    {
        /// <summary>Permission to access location was not specified.</summary>
        Unspecified,

        /// <summary>Permission to access location was granted.</summary>
        Allowed,

        /// <summary>Permission to access location was denied.</summary>
        Denied,
    }
}