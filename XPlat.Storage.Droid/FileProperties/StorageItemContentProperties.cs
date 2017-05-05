namespace XPlat.Storage.FileProperties
{
    using System.Threading.Tasks;

    public class StorageItemContentProperties : IStorageItemContentProperties
    {
        private readonly string filePath;

        internal StorageItemContentProperties(string filePath)
        {
            this.filePath = filePath;
        }

        public Task<IMusicProperties> GetMusicPropertiesAsync()
        {
            IMusicProperties properties = new MusicProperties(this.filePath);
            return Task.FromResult(properties);
        }

        public Task<IVideoProperties> GetVideoPropertiesAsync()
        {
            IVideoProperties properties = new VideoProperties(this.filePath);
            return Task.FromResult(properties);
        }

        public Task<IImageProperties> GetImagePropertiesAsync()
        {
            IImageProperties properties = new ImageProperties(this.filePath);
            return Task.FromResult(properties);
        }
    }
}