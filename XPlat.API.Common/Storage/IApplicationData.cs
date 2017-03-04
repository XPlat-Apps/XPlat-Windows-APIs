namespace XPlat.API.Storage
{
    /// <summary>
    /// Defines the interface for application data.
    /// </summary>
    public interface IApplicationData
    {
        /// <summary>
        /// Gets the root folder for the application in the local data store.
        /// </summary>
        IStorageFolder LocalFolder { get; }

        /// <summary>
        /// Gets the settings container for the application in the local data store.
        /// </summary>
        IAppSettingsContainer LocalSettings { get; }

        /// <summary>
        /// Gets the root folder for the application in the roaming data store.
        /// </summary>
        IStorageFolder RoamingFolder { get; }

        /// <summary>
        /// Gets the root folder for the application in the temporary data store.
        /// </summary>
        IStorageFolder TemporaryFolder { get; }
    }
}