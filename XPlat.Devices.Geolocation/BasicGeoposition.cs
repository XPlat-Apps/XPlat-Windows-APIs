namespace XPlat.Device.Geolocation
{
    /// <summary>The basic information to describe a geographic position.</summary>
    public struct BasicGeoposition
    {
        /// <summary>The latitude of the geographic position. The valid range of latitude values is from -90.0 to 90.0 degrees.</summary>
        public double Latitude;

        /// <summary>The longitude of the geographic position. The valid range of longitude values is from -180.0 to 180.0 degrees.</summary>
        public double Longitude;

        /// <summary>The altitude of the geographic position in meters.</summary>
        public double Altitude;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicGeoposition"/> struct.
        /// </summary>
        /// <param name="latitude">
        /// The latitude of the geographic position.
        /// </param>
        /// <param name="longitude">
        /// The longitude of the geographic position.
        /// </param>
        /// <param name="altitude">
        /// The altitude of the geographic position.
        /// </param>
        public BasicGeoposition(double latitude, double longitude, double altitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Altitude = altitude;
        }

#if WINDOWS_UWP
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicGeoposition"/> struct.
        /// </summary>
        /// <param name="geoposition">
        /// The Windows BasicGeoposition which will be used to populate the field values.
        /// </param>
        public BasicGeoposition(Windows.Devices.Geolocation.BasicGeoposition geoposition)
        {
            this.Latitude = geoposition.Latitude;
            this.Longitude = geoposition.Longitude;
            this.Altitude = geoposition.Altitude;
        }
#elif __ANDROID__
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicGeoposition"/> struct.
        /// </summary>
        /// <param name="location">
        /// The Android Location which will be used to populate the field values.
        /// </param>
        public BasicGeoposition(Android.Locations.Location location)
        {
            this.Latitude = location.Latitude;
            this.Longitude = location.Longitude;
            this.Altitude = location.HasAltitude ? location.Altitude : 0;
        }
#elif __IOS__
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicGeoposition"/> struct.
        /// </summary>
        /// <param name="location">
        /// The iOS CLLocation which will be used to populate the field values.
        /// </param>
        public BasicGeoposition(CoreLocation.CLLocation location)
        {
            this.Latitude = location.HorizontalAccuracy > -1 ? location.Coordinate.Latitude : 0;
            this.Longitude = location.HorizontalAccuracy > -1 ? location.Coordinate.Longitude : 0;
            this.Altitude = location.VerticalAccuracy > -1 ? location.Altitude : 0;
        }
#endif
    }
}