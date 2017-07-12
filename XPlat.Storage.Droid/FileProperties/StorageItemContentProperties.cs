namespace XPlat.Storage.FileProperties
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides access to the content-related properties of an item (like a file or folder).
    /// </summary>
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

        /// <inheritdoc />
        public Task<IMusicProperties> GetMusicPropertiesAsync()
        {
            if (this.Item == null || !this.Item.Exists)
            {
                throw new InvalidOperationException("Cannot access the properties for an item that does not exist.");
            }

            IMusicProperties properties = new MusicProperties(this.Item.Path);
            return Task.FromResult(properties);
        }

        /// <inheritdoc />
        public Task<IVideoProperties> GetVideoPropertiesAsync()
        {
            if (this.Item == null || !this.Item.Exists)
            {
                throw new InvalidOperationException("Cannot access the properties for an item that does not exist.");
            }

            IVideoProperties properties = new VideoProperties(this.Item.Path);
            return Task.FromResult(properties);
        }

        /// <inheritdoc />
        public Task<IImageProperties> GetImagePropertiesAsync()
        {
            if (this.Item == null || !this.Item.Exists)
            {
                throw new InvalidOperationException("Cannot access the properties for an item that does not exist.");
            }

            IImageProperties properties = new ImageProperties(this.Item.Path);
            return Task.FromResult(properties);
        }

        public async Task<IDictionary<string, object>> RetrievePropertiesAsync(IEnumerable<string> propertiesToRetrieve)
        {
            if (this.Item == null || !this.Item.Exists)
            {
                throw new InvalidOperationException("Cannot access the properties for an item that does not exist.");
            }

            Dictionary<string, object> props = new Dictionary<string, object>();

            if (!props.ContainsKey("System.FileName") && !string.IsNullOrWhiteSpace(this.Item.Name))
            {
                props.Add("System.FileName", this.Item.Name);
            }

            if (this.Item.IsOfType(StorageItemTypes.File))
            {
                var item = this.Item as IStorageFile;
                if (item != null && !props.ContainsKey("System.FileExtension") && !string.IsNullOrWhiteSpace(item.FileType))
                {
                    props.Add("System.FileExtension", item.FileType);
                }
            }

            try
            {
                var basicProps = await this.Item.GetBasicPropertiesAsync();

                if (!props.ContainsKey("System.Size"))
                {
                    props.Add("System.Size", basicProps.Size);
                }
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                var mediaProps = await this.GetAllMediaPropertiesAsync();

                foreach (var prop in mediaProps)
                {
                    if (!props.ContainsKey(prop.Key) && prop.Value != null)
                    {
                        props.Add(prop.Key, prop.Value);
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }

            return props;
        }
    }
}