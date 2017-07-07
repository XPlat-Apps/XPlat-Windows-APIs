namespace XPlat.Storage
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the application data layer.
    /// </summary>
    public sealed class ApplicationData : IApplicationData
    {
        private static readonly Lazy<ApplicationData> CurrentAppData =
            new Lazy<ApplicationData>(() => new ApplicationData(), LazyThreadSafetyMode.PublicationOnly);

        private readonly Lazy<IApplicationDataContainer> localSettings = new Lazy<IApplicationDataContainer>(
            CreateLocalSettings,
            LazyThreadSafetyMode.PublicationOnly);

        private readonly Lazy<IStorageFolder> localFolder = new Lazy<IStorageFolder>(
            CreateLocalFolder,
            LazyThreadSafetyMode.PublicationOnly);

        private readonly Lazy<IApplicationDataContainer> roamingSettings = new Lazy<IApplicationDataContainer>(
            CreateRoamingSettings,
            LazyThreadSafetyMode.PublicationOnly);

        private readonly Lazy<IStorageFolder> roamingFolder = new Lazy<IStorageFolder>(
            CreateRoamingFolder,
            LazyThreadSafetyMode.PublicationOnly);

        private readonly Lazy<IStorageFolder> temporaryFolder = new Lazy<IStorageFolder>(
            CreateTemporaryFolder,
            LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Gets the current instance of the <see cref="ApplicationData"/>.
        /// </summary>
        public static ApplicationData Current => CurrentAppData.Value;

        /// <inheritdoc />
        public Task ClearAsync()
        {
            return Windows.Storage.ApplicationData.Current.ClearAsync().AsTask();
        }

        /// <inheritdoc />
        public Task ClearAsync(ApplicationDataLocality locality)
        {
            return Windows.Storage.ApplicationData.Current.ClearAsync(locality.ToApplicationDataLocality()).AsTask();
        }

        /// <summary>
        /// Gets the root folder for the application in the local data store.
        /// </summary>
        public IStorageFolder LocalFolder => this.localFolder.Value;

        /// <inheritdoc />
        public IApplicationDataContainer LocalSettings => this.localSettings.Value;

        /// <summary>
        /// Gets the root folder for the application in the roaming data store.
        /// </summary>
        public IStorageFolder RoamingFolder => this.roamingFolder.Value;

        /// <inheritdoc />
        public IApplicationDataContainer RoamingSettings => this.roamingSettings.Value;

        /// <summary>
        /// Gets the root folder for the application in the temporary data store.
        /// </summary>
        public IStorageFolder TemporaryFolder => this.temporaryFolder.Value;

        private static IApplicationDataContainer CreateRoamingSettings()
        {
            return new ApplicationDataContainer(Windows.Storage.ApplicationData.Current.RoamingSettings);
        }

        private static IApplicationDataContainer CreateLocalSettings()
        {
            return new ApplicationDataContainer(Windows.Storage.ApplicationData.Current.LocalSettings);
        }

        private static IStorageFolder CreateLocalFolder()
        {
            return new StorageFolder(Windows.Storage.ApplicationData.Current.LocalFolder);
        }

        private static IStorageFolder CreateRoamingFolder()
        {
            return new StorageFolder(Windows.Storage.ApplicationData.Current.RoamingFolder);
        }

        private static IStorageFolder CreateTemporaryFolder()
        {
            return new StorageFolder(Windows.Storage.ApplicationData.Current.TemporaryFolder);
        }
    }
}