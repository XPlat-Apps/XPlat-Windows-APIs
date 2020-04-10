namespace XPlat.Services.Maps
{
    using XPlat.Device.Geolocation;

    /// <summary>Represents a geographic location.</summary>
    public class MapLocation : IMapLocation
    {
#if WINDOWS_UWP
        /// <summary>
        /// Initializes a new instance of the <see cref="MapLocation"/> class.
        /// </summary>
        public MapLocation(Windows.Services.Maps.MapLocation mapLocation)
        {
            this.Point = mapLocation.Point;
            this.DisplayName = mapLocation.DisplayName;
            this.Description = mapLocation.Description;
            this.Address = mapLocation.Address;
        }

        public static implicit operator MapLocation(Windows.Services.Maps.MapLocation mapAddress)
        {
            return new MapLocation(mapAddress);
        }
#elif __ANDROID__
        /// <summary>
        /// Initializes a new instance of the <see cref="MapLocation"/> class.
        /// </summary>
        public MapLocation(Android.Locations.Address mapLocation)
        {
            this.Point = new Geopoint(new BasicGeoposition(mapLocation.Latitude, mapLocation.Longitude, 0));
            this.DisplayName = mapLocation.FeatureName;
            this.Address = mapLocation;
        }

        public static implicit operator MapLocation(Android.Locations.Address mapAddress)
        {
            return new MapLocation(mapAddress);
        }
#endif

        /// <summary>Gets the coordinates of a geographic location.</summary>
        public Geopoint Point { get; internal set; }

        /// <summary>Gets the display name of a geographic location.</summary>
        public string DisplayName { get; internal set; }

        /// <summary>Gets the description of a geographic location.</summary>
        public string Description { get; internal set; }

        /// <summary>Gets the address of a geographic location.</summary>
        public MapAddress Address { get; internal set; }
    }
}
