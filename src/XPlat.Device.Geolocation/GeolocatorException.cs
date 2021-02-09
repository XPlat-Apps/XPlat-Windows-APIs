// XPlat Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XPlat.Device.Geolocation
{
    using System;

    /// <summary>
    /// Defines an exception for an error in the <see cref="IGeolocator"/>.
    /// </summary>
    public class GeolocatorException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GeolocatorException"/> class.
        /// </summary>
        public GeolocatorException()
            : this(string.Empty)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="GeolocatorException"/> class with a specified error message.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public GeolocatorException(string message)
            : this(message, null)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="GeolocatorException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public GeolocatorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}