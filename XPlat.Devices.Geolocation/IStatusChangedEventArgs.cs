namespace XPlat.Devices.Geolocation
{
    /// <summary>Provides information for the StatusChanged event.</summary>
    public interface IStatusChangedEventArgs
    {
        /// <summary>Gets the updated status of the Geolocator object.</summary>
        PositionStatus Status { get; }
    }
}