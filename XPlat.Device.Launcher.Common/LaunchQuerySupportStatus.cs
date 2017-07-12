namespace XPlat.Device
{
    /// <summary>Specifies whether an app is available that supports activation.</summary>
    public enum LaunchQuerySupportStatus
    {
        Available,

        AppNotInstalled,

        AppUnavailable,

        NotSupported,

        Unknown,
    }
}