// <copyright file="BasicGeoposition.cs" company="James Croft">
// Copyright (c) James Croft. All rights reserved.
// </copyright>

namespace XPlat.Devices.Geolocation
{
    /// <summary>The basic information to describe a geographic position.</summary>
    public struct BasicGeoposition
    {
        /// <summary>The latitude of the geographic position. The valid range of latitude values is from -90.0 to 90.0 degrees.</summary>
        public double Latitude;

        /// <summary>The longitude of the geographic position. The valid range of longitude values is from -180.0 to 180.0 degrees.</summary>
        public double Longitude;

        /// <summary>The altitude of the geographic position in meters.</summary>
        public double Altitude;

        public BasicGeoposition(double latitude, double longitude, double altitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Altitude = altitude;
        }

#if WINDOWS_UWP
        public BasicGeoposition(Windows.Devices.Geolocation.BasicGeoposition geoposition)
        {
            this.Latitude = geoposition.Latitude;
            this.Longitude = geoposition.Longitude;
            this.Altitude = geoposition.Altitude;
        }
#elif __ANDROID__
        public BasicGeoposition(Android.Locations.Location location)
        {
            this.Latitude = location.Latitude;
            this.Longitude = location.Longitude;
            this.Altitude = location.HasAltitude ? location.Altitude : 0;
        }
#elif __IOS__
        public BasicGeoposition(CoreLocation.CLLocation location)
        {
            this.Latitude = location.HorizontalAccuracy > -1 ? location.Coordinate.Latitude : 0;
            this.Longitude = location.HorizontalAccuracy > -1 ? location.Coordinate.Longitude : 0;
            this.Altitude = location.VerticalAccuracy > -1 ? location.Altitude : 0;
        }
#endif
    }
}