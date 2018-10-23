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
        public PositionChangedEventArgs(Windows.Devices.Geolocation.PositionChangedEventArgs eventArgs)
        {
            this.Position = new Geoposition(eventArgs.Position);
        }

        public PositionChangedEventArgs(Windows.Devices.Geolocation.Geoposition position)
        {
            this.Position = new Geoposition(position);
        }
#elif __ANDROID__
        public PositionChangedEventArgs(Android.Locations.Location location)
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