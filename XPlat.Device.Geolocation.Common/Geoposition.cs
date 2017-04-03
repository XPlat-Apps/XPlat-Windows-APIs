namespace XPlat.Device.Geolocation
{
    /// <summary>
    /// Defines a model for a location that may contain latitude and longitude data.
    /// </summary>
    public class Geoposition
    {
        /// <summary>
        /// Gets or sets the latitude and longitude associated with a geographic location.
        /// </summary>
        public Geocoordinate Coordinate { get; set; }
    }
}