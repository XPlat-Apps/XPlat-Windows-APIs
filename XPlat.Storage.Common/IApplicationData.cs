namespace XPlat.Storage
{
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the interface for application data.
    /// </summary>
    public interface IApplicationData
    {
        /// <summary>
        /// Removes all application data from the local, roaming, and temporary app data stores.
        /// </summary>
        /// <returns>
        /// An object that is used to manage the asynchronous clear operation.
        /// </returns>
        Task ClearAsync();

        /// <summary>
        /// Removes all application data from the specified app data store.
        /// </summary>
        /// <returns>
        /// An object that is used to manage the asynchronous clear operation.
        /// </returns>
        /// <param name="locality">
        /// One of the enumeration values.
        /// </param>
        Task ClearAsync(ApplicationDataLocality locality);

        /// <summary>
        /// Gets the root folder for the application in the local data store.
        /// </summary>
        IStorageFolder LocalFolder { get; }

        /// <summary>
        /// Gets the settings container for the application in the local data store.
        /// </summary>
        IApplicationDataContainer LocalSettings { get; }

        /// <summary>
        /// Gets the root folder for the application in the roaming data store.
        /// </summary>
        IStorageFolder RoamingFolder { get; }

        /// <summary>
        /// Gets the application settings container in the roaming app data store.
        /// </summary>
        IApplicationDataContainer RoamingSettings { get; }

        /// <summary>
        /// Gets the root folder for the application in the temporary data store.
        /// </summary>
        IStorageFolder TemporaryFolder { get; }
    }
}