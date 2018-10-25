namespace XPlat.Device.Geolocation
{
    /// <summary>Describes a geographic point.</summary>
    public class Geopoint : IGeopoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Geopoint"/> class.
        /// </summary>
        /// <param name="position">
        /// The position.
        /// </param>
        public Geopoint(BasicGeoposition position)
        {
            this.Position = position;
        }

#if WINDOWS_UWP
        /// <summary>Initializes a new instance of the <see cref="Geopoint"/> class.</summary>
        public Geopoint(Windows.Devices.Geolocation.Geopoint geopoint)
        {
            this.Position = new BasicGeoposition(geopoint.Position);
        }

        /// <summary>Initializes a new instance of the <see cref="Geopoint"/> class.</summary>
        public Geopoint(Windows.Devices.Geolocation.BasicGeoposition geoposition)
        {
            this.Position = new BasicGeoposition(geoposition);
        }
#elif __ANDROID__
        /// <summary>Initializes a new instance of the <see cref="Geopoint"/> class.</summary>
        public Geopoint(Android.Locations.Location location)
        {
            this.Position = new BasicGeoposition(location);
        }
#elif __IOS__
        /// <summary>Initializes a new instance of the <see cref="Geopoint"/> class.</summary>
        public Geopoint(CoreLocation.CLLocation location)
        {
            this.Position = new BasicGeoposition(location);
        }
#endif

        /// <summary>Gets or sets the position of a geographic point.</summary>
        public BasicGeoposition Position { get; set; }
    }
}