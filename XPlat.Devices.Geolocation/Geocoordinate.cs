// <copyright file="Geocoordinate.cs" company="James Croft">
// Copyright (c) James Croft. All rights reserved.
// </copyright>

namespace XPlat.Devices.Geolocation
{
    using System;

    /// <summary>Contains the information for identifying a geographic location.</summary>
    public class Geocoordinate : IGeocoordinate
    {
        public Geocoordinate()
        {
        }

#if WINDOWS_UWP
        public Geocoordinate(Windows.Devices.Geolocation.Geocoordinate coordinate)
        {
            this.Latitude = coordinate.Latitude;
            this.Longitude = coordinate.Longitude;
            this.Altitude = coordinate.Altitude ?? 0;
            this.Accuracy = coordinate.Accuracy;
            this.Heading = coordinate.Heading ?? 0;
            this.Speed = coordinate.Speed ?? 0;
            this.Timestamp = coordinate.Timestamp;
            this.Point = new Geopoint(coordinate.Point);
        }
#endif

        /// <summary>Gets or sets the latitude in degrees. The valid range of values is from -90.0 to 90.0.</summary>
        public double Latitude { get; set; }

        /// <summary>Gets or sets the longitude in degrees. The valid range of values is from -180.0 to 180.0.</summary>
        public double Longitude { get; set; }

        /// <summary>Gets or sets the altitude of the location, in meters.</summary>
        public double Altitude { get; set; }

        /// <summary>Gets or sets the accuracy of the location in meters.</summary>
        public double Accuracy { get; set; }

        /// <summary>Gets or sets the current heading in degrees relative to true north.</summary>
        public double Heading { get; set; }

        /// <summary>Gets or sets the speed in meters per second.</summary>
        public double Speed { get; set; }

        /// <summary>Gets or sets the system time at which the location was determined.</summary>
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>Gets or sets the location of the Geocoordinate.</summary>
        public Geopoint Point { get; set; }
    }
}