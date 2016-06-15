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

        private readonly Lazy<IAppFileStore> rootFolder = new Lazy<IAppFileStore>(
          CreateAppRootFolder,
          LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Gets the application settings.
        /// </summary>
        public IAppSettings Settings
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

        public IAppFileStore RootFolder
        {
            get
            {
                var f = this.rootFolder.Value;
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

        private static IAppFileStore CreateAppRootFolder()
        {
#if PORTABLE
            return null;
#else
            return new AppRootFolder();
#endif
        }
    }
}