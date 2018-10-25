// <copyright file="IGeolocator.cs" company="James Croft">
// Copyright (c) James Croft. All rights reserved.
// </copyright>

namespace XPlat.Device.Geolocation
{
    using System;
    using System.Threading.Tasks;
    using XPlat.Foundation;

    /// <summary>Provides access to the current geographic location.</summary>
    public interface IGeolocator
    {
        /// <summary>Raised when the location is updated.</summary>
        event TypedEventHandler<IGeolocator, PositionChangedEventArgs> PositionChanged;

        /// <summary>Raised when the ability of the Geolocator to provide updated location changes.</summary>
        event TypedEventHandler<IGeolocator, StatusChangedEventArgs> StatusChanged;

        /// <summary>Gets or sets the accuracy level at which the Geolocator provides location updates.</summary>
        PositionAccuracy DesiredAccuracy { get; set; }

        /// <summary>Gets or sets the distance of movement, in meters, relative to the coordinate from the last PositionChanged event, that is required for the Geolocator to raise a PositionChanged event.</summary>
        double MovementThreshold { get; set; }

        /// <summary>Gets or sets the requested minimum time interval between location updates, in milliseconds. If your application requires updates infrequently, set this value so that location services can conserve power by calculating location only when needed.</summary>
        uint ReportInterval { get; set; }

        /// <summary>Gets the status that indicates the ability of the Geolocator to provide location updates.</summary>
        PositionStatus LocationStatus { get; }

        /// <summary>Gets the last known position recorded by the Geolocator.</summary>
        Geoposition LastKnownPosition { get; }

        /// <summary>Gets or sets the desired accuracy in meters for data returned from the location service.</summary>
        uint DesiredAccuracyInMeters { get; set; }

        /// <summary>Requests permission to access location data.</summary>
        /// <returns>A GeolocationAccessStatus that indicates if permission to location data has been granted.</returns>
        Task<GeolocationAccessStatus> RequestAccessAsync();

        /// <summary>Starts an asynchronous operation to retrieve the current location of the device.</summary>
        /// <returns>An asynchronous operation that, upon completion, returns a Geoposition marking the found location.</returns>
        Task<Geoposition> GetGeopositionAsync();

        /// <summary>
        /// Starts an asynchronous operation to retrieve the current location of the device.
        /// </summary>
        /// <param name="maximumAge">
        /// The maximum acceptable age of cached location data.
        /// </param>
        /// <param name="timeout">
        /// The timeout.
        /// </param>
        /// <returns>
        /// An asynchronous operation that, upon completion, returns a Geoposition marking the found location.
        /// </returns>
        Task<Geoposition> GetGeopositionAsync(TimeSpan maximumAge, TimeSpan timeout);
    }
}