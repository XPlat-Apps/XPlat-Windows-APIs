namespace XPlat.Storage.FileProperties
{
    using System.Threading.Tasks;

    public interface IStorageItemContentProperties
    {
        Task<IMusicProperties> GetMusicPropertiesAsync();

        Task<IVideoProperties> GetVideoPropertiesAsync();

        Task<IImageProperties> GetImagePropertiesAsync();
    }
}