namespace XamarinApiToolkit.Storage
{
    public sealed class AppData : IAppData
    {
        public IAppFolder LocalFolder { get; }

        public IAppSettingsContainer LocalSettings { get; }

        public IAppFolder RoamingFolder { get; }

        public IAppFolder TemporaryFolder { get; }

        public static AppData Current { get; }
    }
}