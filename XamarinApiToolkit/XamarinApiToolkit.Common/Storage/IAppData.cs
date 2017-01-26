namespace XamarinApiToolkit.Storage
{
    /// <summary>
    /// Defines the interface for application data.
    /// </summary>
    public interface IAppData
    {
        /// <summary>
        /// Gets the root folder for the application in the local data store.
        /// </summary>
        IAppFolder LocalFolder { get; }

        /// <summary>
        /// Gets the settings container for the application in the local data store.
        /// </summary>
        IAppSettingsContainer LocalSettings { get; }

        /// <summary>
        /// Gets the root folder for the application in the roaming data store.
        /// </summary>
        IAppFolder RoamingFolder { get; }

        /// <summary>
        /// Gets the root folder for the application in the temporary data store.
        /// </summary>
        IAppFolder TemporaryFolder { get; }
    }
}