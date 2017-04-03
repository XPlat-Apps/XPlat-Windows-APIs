namespace XPlat.Device.Geolocation
{
    using System;

    using CoreLocation;

    public static partial class Extensions
    {
        internal static Geoposition ToLocalGeoposition(this CLLocation location)
        {
            return location == null ? null : new Geoposition { Coordinate = location.ToLocalGeocoordinate() };
        }

        internal static Geocoordinate ToLocalGeocoordinate(this CLLocation location)
        {
            if (location == null)
            {
                return null;
            }

            var geocoordinate = new Geocoordinate();

            if (location.HorizontalAccuracy > -1)
            {
                geocoordinate.Accuracy = location.HorizontalAccuracy;
                geocoordinate.Latitude = location.Coordinate.Latitude;
                geocoordinate.Longitude = location.Coordinate.Longitude;
            }

            if (location.VerticalAccuracy > -1)
            {
                geocoordinate.Altitude = location.Altitude;
            }

            if (location.Speed > -1)
            {
                geocoordinate.Speed = location.Speed;
            }

            geocoordinate.Timestamp = new DateTimeOffset((DateTime)location.Timestamp);

            return geocoordinate;
        }
    }
}