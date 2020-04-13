namespace XPlat.Device.Geolocation
{
    /// <summary>Provides data for the PositionChanged event.</summary>
    public class PositionChangedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PositionChangedEventArgs"/> class.
        /// </summary>
        /// <param name="position">
        /// The location data associated with the PositionChanged event.
        /// </param>
        public PositionChangedEventArgs(Geoposition position)
        {
            this.Position = position;
        }

#if WINDOWS_UWP
        /// <summary>
        /// Initializes a new instance of the <see cref="PositionChangedEventArgs"/> class.
        /// </summary>
        public PositionChangedEventArgs(Windows.Devices.Geolocation.PositionChangedEventArgs eventArgs)
        {
            this.Position = new Geoposition(eventArgs.Position);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PositionChangedEventArgs"/> class.
        /// </summary>
        public PositionChangedEventArgs(Windows.Devices.Geolocation.Geoposition position)
        {
            this.Position = new Geoposition(position);
        }
#elif __ANDROID__
        /// <summary>
        /// Initializes a new instance of the <see cref="PositionChangedEventArgs"/> class.
        /// </summary>
        public PositionChangedEventArgs(Android.Locations.Location location)
        {
            this.Position = new Geoposition(location);
        }
#elif __IOS__
        /// <summary>
        /// Initializes a new instance of the <see cref="PositionChangedEventArgs"/> class.
        /// </summary>
        public PositionChangedEventArgs(CoreLocation.CLLocation location)
        {
            this.Position = new Geoposition(location);
        }
#endif

        /// <summary>
        /// Gets the location data associated with the PositionChanged event.
        /// </summary>
        public Geoposition Position { get; }
    }
}