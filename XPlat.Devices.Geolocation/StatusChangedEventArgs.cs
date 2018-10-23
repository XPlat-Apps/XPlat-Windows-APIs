namespace XPlat.Device.Geolocation
{
    /// <summary>Provides information for the StatusChanged event.</summary>
    public class StatusChangedEventArgs : IStatusChangedEventArgs
    {
        public StatusChangedEventArgs(PositionStatus status)
        {
            this.Status = status;
        }

#if WINDOWS_UWP
        public StatusChangedEventArgs(Windows.Devices.Geolocation.PositionStatus status)
        {
            this.Status =
                XPlat.Device.Geolocation.Extensions.PositionStatusExtensions.ToInternalPositionStatus(status);
        }

        public StatusChangedEventArgs(Windows.Devices.Geolocation.StatusChangedEventArgs eventArgs)
        {
            this.Status =
                XPlat.Device.Geolocation.Extensions.PositionStatusExtensions
                    .ToInternalPositionStatus(eventArgs.Status);
        }
#endif

        /// <summary>Gets the updated status of the Geolocator object.</summary>
        public PositionStatus Status { get; }
    }
}