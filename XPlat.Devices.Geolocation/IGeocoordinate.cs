namespace XPlat.Devices.Geolocation
{
    using System;

    /// <summary>Contains the information for identifying a geographic location.</summary>
    public interface IGeocoordinate
    {
        /// <summary>Gets or sets the latitude in degrees. The valid range of values is from -90.0 to 90.0.</summary>
        double Latitude { get; set; }

        /// <summary>Gets or sets the longitude in degrees. The valid range of values is from -180.0 to 180.0.</summary>
        double Longitude { get; set; }

        /// <summary>Gets or sets the altitude of the location, in meters.</summary>
        double Altitude { get; set; }

        /// <summary>Gets or sets the accuracy of the location in meters.</summary>
        double Accuracy { get; set; }

        /// <summary>Gets or sets the current heading in degrees relative to true north.</summary>
        double Heading { get; set; }

        /// <summary>Gets or sets the speed in meters per second.</summary>
        double Speed { get; set; }

        /// <summary>Gets or sets the system time at which the location was determined.</summary>
        DateTimeOffset Timestamp { get; set; }

        /// <summary>Gets or sets the location of the Geocoordinate.</summary>
        Geopoint Point { get; set; }
    }
}