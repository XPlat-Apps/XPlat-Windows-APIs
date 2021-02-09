#if WINDOWS_UWP
namespace XPlat.Storage
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using XPlat.Storage.Extensions;

    /// <summary>Provides access to the application data store.</summary>
    public sealed class ApplicationData : IApplicationData, IApplicationDataExtras
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
            return Windows.Storage.ApplicationData.Current.ClearAsync().AsTask();
        }

        /// <summary>Removes all application data from the specified app data store.</summary>
        /// <returns>An object that is used to manage the asynchronous clear operation.</returns>
        /// <param name="locality">One of the enumeration values.</param>
        public Task ClearAsync(ApplicationDataLocality locality)
        {
            return Windows.Storage.ApplicationData.Current.ClearAsync(locality.ToWindowsApplicationDataLocality()).AsTask();
        }

        /// <summary>
        /// Retrieves a <see cref="IStorageFile"/> by the given <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>The <see cref="IStorageFile"/>.</returns>
        public async Task<IStorageFile> GetFileFromPathAsync(string path)
        {
            StorageFile file = await Windows.Storage.StorageFile.GetFileFromPathAsync(path);
            return file;
        }

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
#endif