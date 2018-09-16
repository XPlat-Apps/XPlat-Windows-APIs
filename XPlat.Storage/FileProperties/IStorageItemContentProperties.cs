namespace XPlat.Storage.FileProperties
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>Provides access to the content-related properties of an item (like a file or folder).</summary>
    public interface IStorageItemContentProperties
    {
        /// <summary>Retrieves the music properties of the item (like a file of folder).</summary>
        /// <returns>When this method completes successfully, it returns a musicProperties object.</returns>
        Task<IMusicProperties> GetMusicPropertiesAsync();

        /// <summary>Retrieves the video properties of the item (like a file of folder).</summary>
        /// <returns>When this method completes successfully, it returns a videoProperties object.</returns>
        Task<IVideoProperties> GetVideoPropertiesAsync();

        /// <summary>Retrieves the image properties of the item (like a file of folder).</summary>
        /// <returns>When this method completes successfully, it returns an imageProperties object.</returns>
        Task<IImageProperties> GetImagePropertiesAsync();

        Task<IDictionary<string, object>> RetrievePropertiesAsync(IEnumerable<string> propertiesToRetrieve);
    }
}