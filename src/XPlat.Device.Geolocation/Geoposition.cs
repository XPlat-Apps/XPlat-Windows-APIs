namespace XPlat.Device.Geolocation
{
    /// <summary>Represents a location that may contain latitude and longitude data.</summary>
    public class Geoposition : IGeoposition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Geoposition"/> class.
        /// </summary>
        public Geoposition()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Geoposition"/> class.
        /// </summary>
        /// <param name="coordinate">
        /// The coordinate.
        /// </param>
        public Geoposition(Geocoordinate coordinate)
        {
            this.Coordinate = coordinate;
        }

#if WINDOWS_UWP
        /// <summary>
        /// Initializes a new instance of the <see cref="Geoposition"/> class.
        /// </summary>
        public Geoposition(Windows.Devices.Geolocation.Geocoordinate coordinate)
        {
            this.Coordinate = new Geocoordinate(coordinate);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Geoposition"/> class.
        /// </summary>
        public Geoposition(Windows.Devices.Geolocation.Geoposition position)
        {
            this.Coordinate = new Geocoordinate(position.Coordinate);
        }

        public static implicit operator Geoposition(Windows.Devices.Geolocation.Geocoordinate geocoordinate)
        {
            return new Geoposition(geocoordinate);
        }

        public static implicit operator Geoposition(Windows.Devices.Geolocation.Geoposition geoposition)
        {
            return new Geoposition(geoposition);
        }
#elif __ANDROID__
        /// <summary>
        /// Initializes a new instance of the <see cref="Geoposition"/> class.
        /// </summary>
        public Geoposition(Android.Locations.Location location)
        {
            this.Coordinate = new Geocoordinate(location);
        }
#elif __IOS__
        /// <summary>
        /// Initializes a new instance of the <see cref="Geoposition"/> class.
        /// </summary>
        public Geoposition(CoreLocation.CLLocation location)
        {
            this.Coordinate = new Geocoordinate(location);
        }
#endif

        /// <summary>Gets or sets the latitude and longitude associated with a geographic location.</summary>
        public Geocoordinate Coordinate { get; set; }
    }
}