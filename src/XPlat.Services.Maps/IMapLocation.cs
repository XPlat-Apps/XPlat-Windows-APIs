namespace XPlat.Services.Maps
{
    using XPlat.Device.Geolocation;

    /// <summary>Represents a geographic location.</summary>
    public interface IMapLocation
    {
        /// <summary>Gets the coordinates of a geographic location.</summary>
        Geopoint Point { get; }

        /// <summary>Gets the display name of a geographic location.</summary>
        string DisplayName { get; }

        /// <summary>Gets the description of a geographic location.</summary>
        string Description { get; }

        /// <summary>Gets the address of a geographic location.</summary>
        MapAddress Address { get; }
    }
}
