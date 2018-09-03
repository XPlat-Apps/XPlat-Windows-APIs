// <copyright file="IGeopoint.cs" company="James Croft">
// Copyright (c) James Croft. All rights reserved.
// </copyright>

namespace XPlat.Devices.Geolocation
{
    public interface IGeopoint
    {
        /// <summary>Gets or sets the position of a geographic point.</summary>
        BasicGeoposition Position { get; set; }
    }
}