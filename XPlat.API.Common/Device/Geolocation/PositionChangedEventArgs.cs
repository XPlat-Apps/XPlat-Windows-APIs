namespace XPlat.API.Device.Geolocation
{
    /// <summary>
    /// Defines an event argument for providing data for the PositionChanged event.
    /// </summary>
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

        /// <summary>
        /// Gets the location data associated with the PositionChanged event.
        /// </summary>
        public Geoposition Position { get; }
    }
}