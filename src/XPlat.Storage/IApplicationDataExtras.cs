namespace XPlat.Storage
{
    using System.Threading.Tasks;

    /// <summary>Provides additional extra methods for accessing data from the application data store.</summary>
    public interface IApplicationDataExtras
    {
        /// <summary>
        /// Retrieves a <see cref="IStorageFile"/> by the given <paramref name="path"/>.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <returns>The <see cref="IStorageFile"/>.</returns>
        Task<IStorageFile> GetFileFromPathAsync(string path);
    }
}