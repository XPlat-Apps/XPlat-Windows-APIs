#if WINDOWS_UWP
namespace XPlat.Storage.FileProperties
{
    using System;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>Provides access to the content-related properties of an item (like a file or folder).</summary>
    public class StorageItemContentProperties : IStorageItemContentProperties
    {
        private readonly WeakReference itemReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="StorageItemContentProperties"/> class.
        /// </summary>
        /// <param name="itemReference">
        /// The IStorageItem reference.
        /// </param>
        internal StorageItemContentProperties(WeakReference itemReference)
        {
            this.itemReference = itemReference;
        }

        /// <summary>
        /// Gets the instance of the IStorageItem passed by the weak reference.
        /// </summary>
        internal IStorageItem Item => this.itemReference != null && this.itemReference.IsAlive
            ? this.itemReference.Target as IStorageItem
            : null;

        /// <summary>Retrieves the music properties of the item (like a file of folder).</summary>
        /// <returns>When this method completes successfully, it returns a musicProperties object.</returns>
        public async Task<IMusicProperties> GetMusicPropertiesAsync()
        {
            Windows.Storage.FileProperties.MusicProperties props;

            switch (this.Item)
            {
                case StorageFile file:
                    props = await file.Originator.Properties.GetMusicPropertiesAsync();
                    return new MusicProperties(props);
                case StorageFolder folder:
                    props = await folder.Originator.Properties.GetMusicPropertiesAsync();
                    return new MusicProperties(props);
                default:
                    return null;
            }
        }

        /// <summary>Retrieves the video properties of the item (like a file of folder).</summary>
        /// <returns>When this method completes successfully, it returns a videoProperties object.</returns>
        public async Task<IVideoProperties> GetVideoPropertiesAsync()
        {
            Windows.Storage.FileProperties.VideoProperties props;

            switch (this.Item)
            {
                case StorageFile file:
                    props = await file.Originator.Properties.GetVideoPropertiesAsync();
                    return new VideoProperties(props);
                case StorageFolder folder:
                    props = await folder.Originator.Properties.GetVideoPropertiesAsync();
                    return new VideoProperties(props);
                default:
                    return null;
            }
        }

        /// <summary>Retrieves the image properties of the item (like a file of folder).</summary>
        /// <returns>When this method completes successfully, it returns an imageProperties object.</returns>
        public async Task<IImageProperties> GetImagePropertiesAsync()
        {
            Windows.Storage.FileProperties.ImageProperties props;

            switch (this.Item)
            {
                case StorageFile file:
                    props = await file.Originator.Properties.GetImagePropertiesAsync();
                    return new ImageProperties(props);
                case StorageFolder folder:
                    props = await folder.Originator.Properties.GetImagePropertiesAsync();
                    return new ImageProperties(props);
                default:
                    return null;
            }
        }

        public async Task<IDictionary<string, object>> RetrievePropertiesAsync(IEnumerable<string> propertiesToRetrieve)
        {
            switch (this.Item)
            {
                case StorageFile file:
                    return await file.Originator.Properties.RetrievePropertiesAsync(propertiesToRetrieve);
                case StorageFolder folder:
                    return await folder.Originator.Properties.RetrievePropertiesAsync(propertiesToRetrieve);
                default:
                    return null;
            }
        }
    }
}
#endif