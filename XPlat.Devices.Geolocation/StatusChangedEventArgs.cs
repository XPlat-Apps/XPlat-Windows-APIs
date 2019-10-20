namespace XPlat.Device.Geolocation
{
    /// <summary>Provides information for the StatusChanged event.</summary>
    public class StatusChangedEventArgs : IStatusChangedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusChangedEventArgs"/> class.
        /// </summary>
        /// <param name="status">
        /// The position status.
        /// </param>
        public StatusChangedEventArgs(PositionStatus status)
        {
            this.Status = status;
        }

#if WINDOWS_UWP
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusChangedEventArgs"/> class.
        /// </summary>
        public StatusChangedEventArgs(Windows.Devices.Geolocation.PositionStatus status)
        {
            this.Status =
                XPlat.Device.Geolocation.Extensions.PositionStatusExtensions.ToInternalPositionStatus(status);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusChangedEventArgs"/> class.
        /// </summary>
        public StatusChangedEventArgs(Windows.Devices.Geolocation.StatusChangedEventArgs eventArgs)
        {
            this.Status =
                XPlat.Device.Geolocation.Extensions.PositionStatusExtensions
                    .ToInternalPositionStatus(eventArgs.Status);
        }

        public static implicit operator StatusChangedEventArgs(Windows.Devices.Geolocation.StatusChangedEventArgs eventArgs)
        {
            return new StatusChangedEventArgs(eventArgs);
        }
#endif

        /// <summary>Gets the updated status of the Geolocator object.</summary>
        public PositionStatus Status { get; }
    }
}