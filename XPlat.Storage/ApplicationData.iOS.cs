#if __IOS__
namespace XPlat.Storage
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>Provides access to the application data store.</summary>
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

        /// <summary>Gets the current app data store associated with the app's app package.</summary>
        public static ApplicationData Current => CurrentAppData.Value;

        /// <summary>Gets the root folder in the local app data store.</summary>
        public IStorageFolder LocalFolder => this.localFolder.Value;

        /// <summary>Gets the application settings container in the local app data store.</summary>
        public IApplicationDataContainer LocalSettings => this.localSettings.Value;

        /// <summary>Gets the root folder in the roaming app data store.</summary>
        public IStorageFolder RoamingFolder => this.roamingFolder.Value;

        /// <summary>Gets the application settings container in the roaming app data store.</summary>
        public IApplicationDataContainer RoamingSettings => this.roamingSettings.Value;

        /// <summary>Gets the root folder in the temporary app data store.</summary>
        public IStorageFolder TemporaryFolder => this.temporaryFolder.Value;

        /// <summary>Removes all application data from the local, roaming, and temporary app data stores.</summary>
        /// <returns>An object that is used to manage the asynchronous clear operation.</returns>
        public Task ClearAsync()
        {
            return Task.Run(
                async () =>
                {
                    IStorageFolder local = this.LocalFolder;
                    if (local != null)
                    {
                        await local.ClearAsync();
                    }

                    IStorageFolder roaming = this.RoamingFolder;
                    if (roaming != null)
                    {
                        await roaming.ClearAsync();
                    }

                    IStorageFolder temporary = this.TemporaryFolder;
                    if (temporary != null)
                    {
                        await temporary.ClearAsync();
                    }

                    this.LocalSettings?.Values?.Clear();
                    this.RoamingSettings?.Values?.Clear();
                });
        }

        /// <summary>Removes all application data from the specified app data store.</summary>
        /// <returns>An object that is used to manage the asynchronous clear operation.</returns>
        /// <param name="locality">One of the enumeration values.</param>
        public Task ClearAsync(ApplicationDataLocality locality)
        {
            return Task.Run(
                async () =>
                {
                    switch (locality)
                    {
                        case ApplicationDataLocality.Local:
                            IStorageFolder local = this.LocalFolder;
                            if (local != null)
                            {
                                await local.ClearAsync();
                            }
                            this.LocalSettings?.Values?.Clear();
                            break;
                        case ApplicationDataLocality.Roaming:
                            IStorageFolder roaming = this.RoamingFolder;
                            if (roaming != null)
                            {
                                await roaming.ClearAsync();
                            }
                            this.RoamingSettings?.Values?.Clear();
                            break;
                        case ApplicationDataLocality.Temporary:
                            IStorageFolder temporary = this.TemporaryFolder;
                            if (temporary != null)
                            {
                                await temporary.ClearAsync();
                            }
                            break;
                    }
                });
        }

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
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            return new StorageFolder(System.IO.Path.Combine(documentsPath, "..", "Library"));
        }

        private static IStorageFolder CreateRoamingFolder()
        {
            return null;
        }

        private static IStorageFolder CreateTemporaryFolder()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            StorageFolder localFolder = new StorageFolder(System.IO.Path.Combine(documentsPath, "..", "Library"));

            Task<IStorageFolder> tempFolderTask = localFolder.CreateFolderAsync("Temp", CreationCollisionOption.OpenIfExists);

            Task.WaitAll(tempFolderTask);

            return tempFolderTask.Result;
        }
    }
}
#endif