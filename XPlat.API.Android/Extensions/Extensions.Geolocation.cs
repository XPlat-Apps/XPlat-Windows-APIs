namespace XPlat.API
{
    using System;

    using Android.Locations;

    using XPlat.API.Device.Geolocation;

    public static partial class Extensions
    {
        internal static TimeSpan TimeSpanFromMilliseconds(this long time)
        {
            return new TimeSpan(TimeSpan.TicksPerMillisecond * time);
        }

        internal static DateTimeOffset GetTimestamp(this Location location)
        {
            return new DateTimeOffset(GeolocatorConstants.LocationTime.AddMilliseconds(location.Time));
        }

        internal static Geoposition ToLocalGeoposition(this Location location)
        {
            return location == null ? null : new Geoposition { Coordinate = location.ToLocalGeocoordinate() };
        }

        internal static Geocoordinate ToLocalGeocoordinate(this Location location)
        {
            return location == null
                       ? null
                       : new Geocoordinate
                             {
                                 Accuracy = location.HasAccuracy ? location.Accuracy : 0,
                                 Altitude = location.HasAltitude ? location.Altitude : 0,
                                 Heading = location.HasBearing ? location.Bearing : 0,
                                 Speed = location.HasSpeed ? location.Speed : 0,
                                 Latitude = location.Latitude,
                                 Longitude = location.Longitude,
                                 Timestamp = location.GetTimestamp()
                             };
        }
    }
}