namespace XPlat.API.Device.Geolocation
{
    public class StatusChangedEventArgs
    {
        public StatusChangedEventArgs(PositionStatus status)
        {
            this.Status = status;
        }

        /// <summary>
        /// Gets the updated status of the Geolocator object.
        /// </summary>
        public PositionStatus Status { get; }
    }
}