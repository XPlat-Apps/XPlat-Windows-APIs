namespace XPlat.API
{
    using XPlat.API.Device.Geolocation;

    public partial class Extensions
    {
        internal static Geoposition ToLocalGeoposition(this Windows.Devices.Geolocation.Geoposition position)
        {
            return position == null ? null : new Geoposition { Coordinate = position.Coordinate.ToLocalGeocoordinate() };
        }

        internal static Geocoordinate ToLocalGeocoordinate(this Windows.Devices.Geolocation.Geocoordinate coordinate)
        {
            return coordinate == null
                       ? null
                       : new Geocoordinate
                             {
                                 Accuracy = coordinate.Accuracy,
                                 Altitude =
                                     coordinate.Point == null ? 0 : coordinate.Point.Position.Altitude,
                                 Heading = coordinate.Heading ?? 0,
                                 Latitude =
                                     coordinate.Point == null ? 0 : coordinate.Point.Position.Latitude,
                                 Longitude =
                                     coordinate.Point == null ? 0 : coordinate.Point.Position.Longitude,
                                 Speed = coordinate.Speed ?? 0,
                                 Timestamp = coordinate.Timestamp
                             };
        }

        internal static PositionStatus ToLocalPositionStatus(this Windows.Devices.Geolocation.PositionStatus status)
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
            }

            return PositionStatus.NotAvailable;
        }

        internal static GeolocationAccessStatus ToLocalGeolocationAccessStatus(
            this Windows.Devices.Geolocation.GeolocationAccessStatus status)
        {
            switch (status)
            {
                case Windows.Devices.Geolocation.GeolocationAccessStatus.Allowed:
                    return GeolocationAccessStatus.Allowed;
                case Windows.Devices.Geolocation.GeolocationAccessStatus.Denied:
                    return GeolocationAccessStatus.Denied;
            }

            return GeolocationAccessStatus.Unspecified;
        }
    }
}