// <copyright file="Geopoint.cs" company="James Croft">
// Copyright (c) James Croft. All rights reserved.
// </copyright>

namespace XPlat.Devices.Geolocation
{
    /// <summary>Describes a geographic point.</summary>
    public class Geopoint : IGeopoint
    {
        /// <summary>Initializes a new instance of the <see cref="Geopoint"/> class.</summary>
        public Geopoint(BasicGeoposition position)
        {
            this.Position = position;
        }

#if WINDOWS_UWP
        /// <summary>Initializes a new instance of the <see cref="Geopoint"/> class.</summary>
        public Geopoint(Windows.Devices.Geolocation.Geopoint geopoint)
        {
            this.Position = new BasicGeoposition(geopoint.Position);
        }

        /// <summary>Initializes a new instance of the <see cref="Geopoint"/> class.</summary>
        public Geopoint(Windows.Devices.Geolocation.BasicGeoposition geoposition)
        {
            this.Position = new BasicGeoposition(geoposition);
        }
#endif

        /// <summary>Gets or sets the position of a geographic point.</summary>
        public BasicGeoposition Position { get; set; }
    }
}