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

        private readonly Lazy<IApplicationSettingsContainer> settings = new Lazy<IApplicationSettingsContainer>(
            CreateSettings,
            LazyThreadSafetyMode.PublicationOnly);

        private readonly Lazy<IStorageFolder> localFolder = new Lazy<IStorageFolder>(
            CreateLocalFolder,
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

        public Task ClearAsync()
        {
            throw new NotImplementedException();
        }

        public Task ClearAsync(ApplicationDataLocality locality)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the root folder for the application in the local data store.
        /// </summary>
        public IStorageFolder LocalFolder => this.localFolder.Value;

        IApplicationDataContainer IApplicationData.LocalSettings
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the settings container for the application in the local data store.
        /// </summary>
        public IApplicationSettingsContainer LocalSettings => this.settings.Value;

        /// <summary>
        /// Gets the root folder for the application in the roaming data store.
        /// </summary>
        public IStorageFolder RoamingFolder => this.roamingFolder.Value;

        public IApplicationDataContainer RoamingSettings { get; }

        /// <summary>
        /// Gets the root folder for the application in the temporary data store.
        /// </summary>
        public IStorageFolder TemporaryFolder => this.temporaryFolder.Value;

        private static IApplicationSettingsContainer CreateSettings()
        {
            return new AppSettingsContainer();
        }

        private static IStorageFolder CreateLocalFolder()
        {
            return new StorageFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        }

        private static IStorageFolder CreateRoamingFolder()
        {
            return null;
        }

        private static IStorageFolder CreateTemporaryFolder()
        {
            var localFolder = new StorageFolder(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            var tempFolderTask = localFolder.CreateFolderAsync("Temp", CreationCollisionOption.OpenIfExists);

            Task.WaitAll(tempFolderTask);

            return tempFolderTask.Result;
        }
    }
}