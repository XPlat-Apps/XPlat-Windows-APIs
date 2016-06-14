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
                    throw new NotImplementedException("The library you're calling AppData from is not support.");
                }
                return s;
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
    }
}