namespace XPlat.Foundation.Collections
{
    public class StringMapChangedEventArgs : IMapChangedEventArgs<string>
    {
        public StringMapChangedEventArgs(string key, CollectionChange collectionChange)
        {
            this.Key = key;
            this.CollectionChange = collectionChange;
        }

        /// <summary>Gets the type of change that occurred in the map.</summary>
        public CollectionChange CollectionChange { get; }

        /// <summary>Gets the key of the item that changed.</summary>
        public string Key { get; }
    }
}