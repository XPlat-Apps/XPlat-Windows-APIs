namespace XPlat.API.Device.Geolocation
{
    using System;
    using System.Threading.Tasks;

    using XPlat.API.Foundation;

    /// <summary>
    /// Defines an interface to access the current geographic location.
    /// </summary>
    public interface IGeolocator
    {
        /// <summary>
        /// Raised when the location is updated.
        /// </summary>
        event TypedEventHandler<IGeolocator, PositionChangedEventArgs> PositionChanged;

        /// <summary>
        /// Raised when the ability of the Geolocator to provide updated location changes.
        /// </summary>
        event TypedEventHandler<IGeolocator, StatusChangedEventArgs> StatusChanged;

        /// <summary>
        /// Gets or sets the requested minimum time interval between location updates, in milliseconds.
        /// </summary>
        uint ReportInterval { get; set; }

        /// <summary>
        /// Gets and sets the distance of movement, in meters, relative to the coordinate from the last PositionChanged event, that is required for the Geolocator to raise a PositionChanged event.
        /// </summary>
        double MovementThreshold { get; set; }

        /// <summary>
        /// Gets or sets the accuracy level at which the Geolocator provides location updates.
        /// </summary>
        PositionAccuracy DesiredAccuracy { get; set; }

        /// <summary>
        /// Gets the status that indicates the ability of the Geolocator to provide location updates.
        /// </summary>
        PositionStatus LocationStatus { get; }

        /// <summary>
        /// Gets or sets the desired accuracy in meters for data returned from the location service.
        /// </summary>
        uint DesiredAccuracyInMeters { get; set; }

        /// <summary>
        /// Requests permission to access location data.
        /// </summary>
        /// <returns>
        /// An object that is used to manage the asynchronous operation.
        /// </returns>
        Task<GeolocationAccessStatus> RequestAccessAsync();

        /// <summary>
        /// Retrieves the current location of the device.
        /// </summary>
        /// <returns>
        /// An asynchronous operation that, upon completion, returns a Geoposition marking the found location.
        /// </returns>
        Task<Geoposition> GetGeopositionAsync();

        /// <summary>
        /// Retrieves the current location of the device.
        /// </summary>
        /// <param name="timeout">
        /// The duration for a timeout.
        /// </param>
        /// <returns>
        /// An asynchronous operation that, upon completion, returns a Geoposition marking the found location.
        /// </returns>
        Task<Geoposition> GetGeopositionAsync(TimeSpan timeout);
    }
}