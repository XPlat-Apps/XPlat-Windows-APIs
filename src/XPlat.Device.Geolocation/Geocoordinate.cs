namespace XPlat.Device.Geolocation
{
    using System;

    /// <summary>Contains the information for identifying a geographic location.</summary>
    public class Geocoordinate : IGeocoordinate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Geocoordinate"/> class.
        /// </summary>
        public Geocoordinate()
        {
        }

#if WINDOWS_UWP
        /// <summary>
        /// Initializes a new instance of the <see cref="Geocoordinate"/> class.
        /// </summary>
        /// <param name="coordinate">
        /// The Windows Geocoordinate which will be used to populate the field values.
        /// </param>
        public Geocoordinate(Windows.Devices.Geolocation.Geocoordinate coordinate)
        {
            this.Latitude = coordinate.Latitude;
            this.Longitude = coordinate.Longitude;
            this.Altitude = coordinate.Altitude ?? 0;
            this.Accuracy = coordinate.Accuracy;
            this.Heading = coordinate.Heading ?? 0;
            this.Speed = coordinate.Speed ?? 0;
            this.Timestamp = coordinate.Timestamp;
            this.Point = new Geopoint(coordinate.Point);
        }

        public static implicit operator Geocoordinate(Windows.Devices.Geolocation.Geocoordinate geocoordinate)
        {
            return new Geocoordinate(geocoordinate);
        }
#elif __ANDROID__
        /// <summary>
        /// Initializes a new instance of the <see cref="Geocoordinate"/> class.
        /// </summary>
        /// <param name="location">
        /// The Android Location which will be used to populate the field values.
        /// </param>
        public Geocoordinate(Android.Locations.Location location)
        {
            this.Latitude = location.Latitude;
            this.Longitude = location.Longitude;
            this.Altitude = location.HasAltitude ? location.Altitude : 0;
            this.Accuracy = location.HasAccuracy ? location.Accuracy : 0;
            this.Heading = location.HasBearing ? location.Bearing : 0;
            this.Speed = location.HasSpeed ? location.Speed : 0;
            this.Timestamp = new DateTimeOffset(Geolocator.AndroidLocationTime.AddMilliseconds(location.Time));
            this.Point = new Geopoint(location);
        }
#elif __IOS__
        /// <summary>
        /// Initializes a new instance of the <see cref="Geocoordinate"/> class.
        /// </summary>
        /// <param name="location">
        /// The iOS CLLocation which will be used to populate the field values.
        /// </param>
        public Geocoordinate(CoreLocation.CLLocation location)
        {
            if (location.HorizontalAccuracy > -1)
            {
                this.Accuracy = location.HorizontalAccuracy;
                this.Latitude = location.Coordinate.Latitude;
                this.Longitude = location.Coordinate.Longitude;
            }

            if (location.VerticalAccuracy > -1)
            {
                this.Altitude = location.Altitude;
            }

            if (location.Speed > -1)
            {
                this.Speed = location.Speed;
            }

            this.Timestamp = new DateTimeOffset((DateTime)location.Timestamp);
            this.Point = new Geopoint(location);
        }
#endif

        /// <summary>Gets or sets the latitude in degrees. The valid range of values is from -90.0 to 90.0.</summary>
        public double Latitude { get; set; }

        /// <summary>Gets or sets the longitude in degrees. The valid range of values is from -180.0 to 180.0.</summary>
        public double Longitude { get; set; }

        /// <summary>Gets or sets the altitude of the location, in meters.</summary>
        public double Altitude { get; set; }

        /// <summary>Gets or sets the accuracy of the location in meters.</summary>
        public double Accuracy { get; set; }

        /// <summary>Gets or sets the current heading in degrees relative to true north.</summary>
        public double Heading { get; set; }

        /// <summary>Gets or sets the speed in meters per second.</summary>
        public double Speed { get; set; }

        /// <summary>Gets or sets the system time at which the location was determined.</summary>
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>Gets or sets the location of the Geocoordinate.</summary>
        public Geopoint Point { get; set; }
    }
}