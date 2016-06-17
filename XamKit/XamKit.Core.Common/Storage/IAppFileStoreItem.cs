namespace XamKit.Core.Common.Storage
{
    public interface IAppFileStoreItem
    {
        /// <summary>
        /// Gets the name of the current file store item.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the full path of the current file store item.
        /// </summary>
        string Path { get; }
    }
}