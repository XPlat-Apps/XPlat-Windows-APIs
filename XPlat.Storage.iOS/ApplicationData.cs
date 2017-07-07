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

        private readonly Lazy<IApplicationDataContainer> localSettings =
            new Lazy<IApplicationDataContainer>(CreateLocalSettings, LazyThreadSafetyMode.PublicationOnly);

        private readonly Lazy<IStorageFolder> localFolder =
            new Lazy<IStorageFolder>(CreateLocalFolder, LazyThreadSafetyMode.PublicationOnly);

        private readonly Lazy<IApplicationDataContainer> roamingSettings =
            new Lazy<IApplicationDataContainer>(CreateRoamingSettings, LazyThreadSafetyMode.PublicationOnly);

        private readonly Lazy<IStorageFolder> roamingFolder =
            new Lazy<IStorageFolder>(CreateRoamingFolder, LazyThreadSafetyMode.PublicationOnly);

        private readonly Lazy<IStorageFolder> temporaryFolder =
            new Lazy<IStorageFolder>(CreateTemporaryFolder, LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// Gets the current instance of the <see cref="ApplicationData"/>.
        /// </summary>
        public static ApplicationData Current => CurrentAppData.Value;

        /// <inheritdoc />
        public Task ClearAsync()
        {
            return Task.Run(
                async () =>
                    {
                        await this.LocalFolder?.ClearAsync();
                        await this.RoamingFolder?.ClearAsync();
                        await this.TemporaryFolder?.ClearAsync();
                        this.LocalSettings?.Values.Clear();
                        this.RoamingSettings?.Values.Clear();
                    });
        }

        /// <inheritdoc />
        public Task ClearAsync(ApplicationDataLocality locality)
        {
            return Task.Run(
                async () =>
                    {
                        switch (locality)
                        {
                            case ApplicationDataLocality.Local:
                                await this.LocalFolder?.ClearAsync();
                                this.LocalSettings?.Values.Clear();
                                break;
                            case ApplicationDataLocality.Roaming:
                                await this.RoamingFolder?.ClearAsync();
                                this.RoamingSettings?.Values.Clear();
                                break;
                            case ApplicationDataLocality.Temporary:
                                await this.TemporaryFolder?.ClearAsync();
                                break;
                        }
                    });
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
            return new ApplicationDataContainer(ApplicationDataLocality.Roaming, string.Empty);
        }

        private static IApplicationDataContainer CreateLocalSettings()
        {
            return new ApplicationDataContainer(ApplicationDataLocality.Local, string.Empty);
        }

        private static IStorageFolder CreateLocalFolder()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return new StorageFolder(System.IO.Path.Combine(documentsPath, "..", "Library"));
        }

        private static IStorageFolder CreateRoamingFolder()
        {
            return null;
        }

        private static IStorageFolder CreateTemporaryFolder()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var localFolder = new StorageFolder(System.IO.Path.Combine(documentsPath, "..", "Library"));

            var tempFolderTask = localFolder.CreateFolderAsync("Temp", CreationCollisionOption.OpenIfExists);

            Task.WaitAll(tempFolderTask);

            return tempFolderTask.Result;
        }
    }
}