namespace XPlat.Device.Geolocation
{
    /// <summary>Represents a location that may contain latitude and longitude data.</summary>
    public class Geoposition : IGeoposition
    {
        public Geoposition()
        {
        }

        public Geoposition(Geocoordinate coordinate)
        {
            this.Coordinate = coordinate;
        }

#if WINDOWS_UWP
        public Geoposition(Windows.Devices.Geolocation.Geocoordinate coordinate)
        {
            this.Coordinate = new Geocoordinate(coordinate);
        }

        public Geoposition(Windows.Devices.Geolocation.Geoposition position)
        {
            this.Coordinate = new Geocoordinate(position.Coordinate);
        }
#elif __ANDROID__
        public Geoposition(Android.Locations.Location location)
        {
            this.Coordinate = new Geocoordinate(location);
        }
#elif __IOS__
        public Geoposition(CoreLocation.CLLocation location)
        {
            this.Coordinate = new Geocoordinate(location);
        }
#endif

        /// <summary>Gets or sets the latitude and longitude associated with a geographic location.</summary>
        public Geocoordinate Coordinate { get; set; }
    }
}