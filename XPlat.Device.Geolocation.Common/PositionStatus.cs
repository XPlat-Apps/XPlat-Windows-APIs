namespace XPlat.Device.Geolocation
{
    /// <summary>
    /// Defines enumeration values indicating the ability of the Geolocator object to provide location data.
    /// </summary>
    public enum PositionStatus
    {
        Ready,

        Initializing,

        NoData,

        Disabled,

        NotInitialized,

        NotAvailable,
    }
}