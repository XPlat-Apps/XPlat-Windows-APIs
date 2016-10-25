namespace XamarinApiToolkit.Storage
{
    using System;
    using System.Threading;

    /// <summary>
    /// Defines the application data layer.
    /// </summary>
    public sealed class AppData : IAppData
    {
        private static readonly Lazy<AppData> CurrentAppData = new Lazy<AppData>(
                                                                   () => new AppData(),
                                                                   LazyThreadSafetyMode.PublicationOnly);

        private readonly Lazy<IAppSettingsContainer> settings = new Lazy<IAppSettingsContainer>(
                                                                    CreateSettings,
                                                                    LazyThreadSafetyMode.PublicationOnly);

        private readonly Lazy<IAppFolder> localFolder = new Lazy<IAppFolder>(
                                                            CreateLocalFolder,
                                                            LazyThreadSafetyMode.PublicationOnly);



        private readonly Lazy<IAppFolder> roamingFolder = new Lazy<IAppFolder>(
                                                              CreateRoamingFolder,
                                                              LazyThreadSafetyMode.PublicationOnly);

        private readonly Lazy<IAppFolder> temporaryFolder = new Lazy<IAppFolder>(
                                                                CreateTemporaryFolder,
                                                                LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Gets the current instance of the <see cref="AppData"/>.
        /// </summary>
        public static AppData Current
        {
            get
            {
                return CurrentAppData.Value;
            }
        }

        /// <summary>
        /// Gets the root folder for the application in the local data store.
        /// </summary>
        public IAppFolder LocalFolder
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
        public IAppFolder RoamingFolder
        {
            get
            {
                return this.roamingFolder.Value;
            }
        }

        /// <summary>
        /// Gets the root folder for the application in the temporary data store.
        /// </summary>
        public IAppFolder TemporaryFolder
        {
            get
            {
                return this.temporaryFolder.Value;
            }
        }

        private static IAppSettingsContainer CreateSettings()
        {
            // ToDo
            return null;
        }

        private static IAppFolder CreateLocalFolder()
        {
#if PORTABLE
            return null;
#elif WINDOWS_UWP
            return new AppFolder(null, Windows.Storage.ApplicationData.Current.LocalFolder);
#else
            return null;
#endif
        }

        private static IAppFolder CreateRoamingFolder()
        {
#if WINDOWS_UWP
            return new AppFolder(null, Windows.Storage.ApplicationData.Current.RoamingFolder);
#else
            return null;
#endif
        }

        private static IAppFolder CreateTemporaryFolder()
        {
#if PORTABLE
            return null;
#elif WINDOWS_UWP
            return new AppFolder(null, Windows.Storage.ApplicationData.Current.TemporaryFolder);
#else
            return null;
#endif
        }
    }
}