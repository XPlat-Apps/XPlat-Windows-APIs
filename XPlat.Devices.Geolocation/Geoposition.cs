// <copyright file="Geoposition.cs" company="James Croft">
// Copyright (c) James Croft. All rights reserved.
// </copyright>

namespace XPlat.Devices.Geolocation
{
    /// <summary>Represents a location that may contain latitude and longitude data.</summary>
    public class Geoposition : IGeoposition
    {
        /// <summary>Gets or sets the latitude and longitude associated with a geographic location.</summary>
        public Geocoordinate Coordinate { get; set; }
    }
}