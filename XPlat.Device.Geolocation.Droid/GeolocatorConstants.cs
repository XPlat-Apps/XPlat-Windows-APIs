namespace XPlat.Device.Geolocation
{
    using System;

    internal static class GeolocatorConstants
    {
        /// <summary>
        /// Gets the time used for converting the Android Location objects Time property to a DateTime.
        /// </summary>
        public static DateTime LocationTime => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }
}