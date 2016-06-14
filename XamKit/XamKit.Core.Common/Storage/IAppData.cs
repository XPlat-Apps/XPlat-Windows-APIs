namespace XamKit.Core.Common.Storage
{
    /// <summary>
    /// Defines the interface for application data.
    /// </summary>
    public interface IAppData
    {
        /// <summary>
        /// Gets the application settings.
        /// </summary>
        IAppSettings Settings { get; }
    }
}