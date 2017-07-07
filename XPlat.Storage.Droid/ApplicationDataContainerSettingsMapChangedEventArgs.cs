namespace XPlat.Storage
{
    using XPlat.Foundation.Collections;

    internal sealed class ApplicationDataContainerSettingsMapChangedEventArgs : IMapChangedEventArgs<string>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDataContainerSettingsMapChangedEventArgs"/> class.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="change">
        /// The change.
        /// </param>
        internal ApplicationDataContainerSettingsMapChangedEventArgs(string key, CollectionChange change)
        {
            this.Key = key;
            this.CollectionChange = change;
        }

        /// <inheritdoc />
        public CollectionChange CollectionChange { get; }

        /// <inheritdoc />
        public string Key { get; }
    }
}