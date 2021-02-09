// XPlat Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XPlat.Device.Geolocation
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