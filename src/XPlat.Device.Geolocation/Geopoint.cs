// XPlat Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XPlat.Device.Geolocation
{
    /// <summary>Describes a geographic point.</summary>
    public class Geopoint : IGeopoint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Geopoint"/> class.
        /// </summary>
        /// <param name="position">
        /// The position.
        /// </param>
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

        public static implicit operator Geopoint(Windows.Devices.Geolocation.Geopoint geopoint)
        {
            return new Geopoint(geopoint);
        }

        public static implicit operator Geopoint(Windows.Devices.Geolocation.BasicGeoposition geoposition)
        {
            return new Geopoint(geoposition);
        }

        public static implicit operator Windows.Devices.Geolocation.Geopoint(Geopoint geopoint)
        {
            return new Windows.Devices.Geolocation.Geopoint(geopoint.Position);
        }
#elif __ANDROID__
        /// <summary>Initializes a new instance of the <see cref="Geopoint"/> class.</summary>
        public Geopoint(Android.Locations.Location location)
        {
            this.Position = new BasicGeoposition(location);
        }
#elif __IOS__
        /// <summary>Initializes a new instance of the <see cref="Geopoint"/> class.</summary>
        public Geopoint(CoreLocation.CLLocation location)
        {
            this.Position = new BasicGeoposition(location);
        }

        public static implicit operator Geopoint(CoreLocation.CLLocation location)
        {
            return new Geopoint(location);
        }

        public static implicit operator CoreLocation.CLLocation(Geopoint geopoint)
        {
            return new CoreLocation.CLLocation(geopoint.Position.Latitude, geopoint.Position.Longitude);
        }
#endif

        /// <summary>Gets or sets the position of a geographic point.</summary>
        public BasicGeoposition Position { get; set; }
    }
}