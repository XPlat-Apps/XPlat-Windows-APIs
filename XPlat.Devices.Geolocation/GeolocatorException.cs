using System;

namespace XPlat.Devices.Geolocation
{
    public class GeolocatorException : Exception
    {
        public GeolocatorException()
            : this(string.Empty)
        {
        }

        public GeolocatorException(string message)
            : this(message, null)
        {
        }

        public GeolocatorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}