using System;

namespace XPlat.Devices.Geolocation
{
    /// <summary>
    /// Defines a model for identifying a geographic location.
    /// </summary>
    public class Geocoordinate
    {
        /// <summary>
        /// Gets or sets the accuracy of the geographic position in meters.
        /// </summary>
        public double Accuracy { get; set; }

        /// <summary>
        /// Gets or sets the altitude of the geographic position in meters.
        /// </summary>
        public double Altitude { get; set; }

        /// <summary>
        /// Gets or sets the current heading in degrees relative to true north.
        /// </summary>
        public double Heading { get; set; }

        /// <summary>
        /// Gets or sets the latitude of the geographic position. The valid range of latitude values is from -90.0 to 90.0 degrees.
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the geographic position. The valid range of latitude values is from -180.0 to 180.0 degrees.
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the speed in meters per second.
        /// </summary>
        public double Speed { get; set; }

        /// <summary>
        /// Gets or sets the system time at which the location was determined.
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }
    }
}