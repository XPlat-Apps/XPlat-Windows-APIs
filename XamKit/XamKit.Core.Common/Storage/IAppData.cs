namespace XamKit.Core.Common.Storage
{
    /// <summary>
    /// Defines the interface for application data.
    /// </summary>
    public interface IAppData
    {
        /// <summary>
        /// Gets the application settings container in the local app data store.
        /// </summary>
        IAppSettings LocalSettings { get; }

        /// <summary>
        /// Gets the root folder in the local app data store.
        /// </summary>
        IAppFolder LocalFolder { get; }

        /// <summary>
        /// Gets the root folder in the roaming app data store.
        /// </summary>
        IAppFolder RoamingFolder { get; }
    }
}