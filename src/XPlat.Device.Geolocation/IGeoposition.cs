namespace XPlat.Device.Geolocation
{
    /// <summary>Represents a location that may contain latitude and longitude data.</summary>
    public interface IGeoposition
    {
        /// <summary>Gets or sets the latitude and longitude associated with a geographic location.</summary>
        Geocoordinate Coordinate { get; set; }
    }
}