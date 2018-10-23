namespace XPlat.Device.Geolocation
{
    /// <summary>Describes a geographic point.</summary>
    public interface IGeopoint
    {
        /// <summary>Gets or sets the position of a geographic point.</summary>
        BasicGeoposition Position { get; set; }
    }
}