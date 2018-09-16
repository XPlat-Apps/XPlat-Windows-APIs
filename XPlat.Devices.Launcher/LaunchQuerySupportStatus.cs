namespace XPlat.Devices
{
    /// <summary>Specifies whether an app is available that supports activation.</summary>
    public enum LaunchQuerySupportStatus
    {
        /// <summary>An app that handles the activation is available and may be activated.</summary>
        Available,

        /// <summary>No app is installed to handle the activation.</summary>
        AppNotInstalled,

        /// <summary>An app that handles the activation is installed but not available because it is being updated by the store or it was installed on a removable device that is not available.</summary>
        AppUnavailable,

        /// <summary>The app does not handle the activation.</summary>
        NotSupported,

        /// <summary>An unknown error was encountered while determining whether an app supports the activation.</summary>
        Unknown,
    }
}