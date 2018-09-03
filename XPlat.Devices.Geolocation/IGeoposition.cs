// <copyright file="IGeoposition.cs" company="James Croft">
// Copyright (c) James Croft. All rights reserved.
// </copyright>

namespace XPlat.Devices.Geolocation
{
    public interface IGeoposition
    {
        /// <summary>Gets or sets the latitude and longitude associated with a geographic location.</summary>
        Geocoordinate Coordinate { get; set; }
    }
}