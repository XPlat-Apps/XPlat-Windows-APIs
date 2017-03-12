namespace XPlat.API.Storage
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

        private readonly Lazy<IAppSettingsContainer> settings = new Lazy<IAppSettingsContainer>(
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
        public static ApplicationData Current
        {
            get
            {
                return CurrentAppData.Value;
            }
        }

        /// <summary>
        /// Gets the root folder for the application in the local data store.
        /// </summary>
        public IStorageFolder LocalFolder
        {
            get
            {
                return this.localFolder.Value;
            }
        }

        /// <summary>
        /// Gets the settings container for the application in the local data store.
        /// </summary>
        public IAppSettingsContainer LocalSettings
        {
            get
            {
                return this.settings.Value;
            }
        }

        /// <summary>
        /// Gets the root folder for the application in the roaming data store.
        /// </summary>
        public IStorageFolder RoamingFolder
        {
            get
            {
                return this.roamingFolder.Value;
            }
        }

        /// <summary>
        /// Gets the root folder for the application in the temporary data store.
        /// </summary>
        public IStorageFolder TemporaryFolder
        {
            get
            {
                return this.temporaryFolder.Value;
            }
        }

        private static IAppSettingsContainer CreateSettings()
        {
#if WINDOWS_UWP || ANDROID || IOS
            return new AppSettingsContainer();
#else
            return null;
#endif
        }

        private static IStorageFolder CreateLocalFolder()
        {
#if WINDOWS_UWP
            return new StorageFolder(null, Windows.Storage.ApplicationData.Current.LocalFolder);
#elif ANDROID
            return new StorageFolder(null, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
#elif IOS
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return new StorageFolder(null, System.IO.Path.Combine(documentsPath, "..", "Library"));
#else
            return null;
#endif
        }

        private static IStorageFolder CreateRoamingFolder()
        {
#if WINDOWS_UWP
            return new StorageFolder(null, Windows.Storage.ApplicationData.Current.RoamingFolder);
#else
            return null;
#endif
        }

        private static IStorageFolder CreateTemporaryFolder()
        {
#if WINDOWS_UWP
            return new StorageFolder(null, Windows.Storage.ApplicationData.Current.TemporaryFolder);
#elif ANDROID
            var localFolder = new StorageFolder(null, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            var tempFolderTask = localFolder.CreateFolderAsync("Temp", CreationCollisionOption.OpenIfExists);

            Task.WaitAll(tempFolderTask);

            return tempFolderTask.Result;
#elif IOS
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var localFolder = new StorageFolder(null, System.IO.Path.Combine(documentsPath, "..", "Library"));

            var tempFolderTask = localFolder.CreateFolderAsync("Temp", CreationCollisionOption.OpenIfExists);

            Task.WaitAll(tempFolderTask);

            return tempFolderTask.Result;
#else
            return null;
#endif
        }
    }
}