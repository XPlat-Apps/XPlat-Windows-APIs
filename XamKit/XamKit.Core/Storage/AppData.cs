namespace XamKit.Core.Storage
{
    using System;
    using System.Threading;

    using XamKit.Core.Common.Storage;

    /// <summary>
    /// Defines the app data model.
    /// </summary>
    public class AppData : IAppData
    {
        private readonly Lazy<IAppSettings> settings = new Lazy<IAppSettings>(
            CreateAppSettings,
            LazyThreadSafetyMode.PublicationOnly);

        private readonly Lazy<IAppFolder> localFolder = new Lazy<IAppFolder>(
            CreateAppLocalFolder,
            LazyThreadSafetyMode.PublicationOnly);

        private readonly Lazy<IAppFolder> roamingFolder = new Lazy<IAppFolder>(
            CreateAppRoamingFolder,
            LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Gets the application settings.
        /// </summary>
        public IAppSettings LocalSettings
        {
            get
            {
                var s = this.settings.Value;
                if (s == null)
                {
                    throw new NotImplementedException("The library you're calling AppData from is not supported.");
                }
                return s;
            }
        }

        public IAppFolder LocalFolder
        {
            get
            {
                var f = this.localFolder.Value;
                if (f == null)
                {
                    throw new NotImplementedException("The library you're calling AppData from is not supported.");
                }
                return f;
            }
        }

        public IAppFolder RoamingFolder
        {
            get
            {
                var f = this.roamingFolder.Value;
                if (f == null)
                {
                    throw new NotImplementedException("The library you're calling AppData from is not supported.");
                }
                return f;
            }
        }

        private static IAppSettings CreateAppSettings()
        {
#if PORTABLE
            return null;
#else
            return new AppSettings();
#endif
        }

        private static IAppFolder CreateAppLocalFolder()
        {
#if PORTABLE
            return null;
#elif WINDOWS_UWP
            return new AppFolder(Windows.Storage.ApplicationData.Current.LocalFolder);
#else
            return new AppFolder();
#endif
        }

        private static IAppFolder CreateAppRoamingFolder()
        {
#if PORTABLE
            return null;
#elif WINDOWS_UWP
            return new AppFolder(Windows.Storage.ApplicationData.Current.RoamingFolder);
#else
            return new AppFolder();
#endif
        }
    }
}