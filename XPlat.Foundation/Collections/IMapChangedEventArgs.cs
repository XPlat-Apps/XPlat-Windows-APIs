namespace XPlat.Foundation.Collections
{
    /// <summary>Provides data for the changed event of a map collection.</summary>
    /// <typeparam name="K">The type of the keys in the map.</typeparam>
    public interface IMapChangedEventArgs<K>
    {
        /// <summary>Gets the type of change that occurred in the map.</summary>
        /// <returns>The type of change in the map.</returns>
        CollectionChange CollectionChange { get; }

        /// <summary>Gets the key of the item that changed.</summary>
        /// <returns>The key of the item that changed.</returns>
        K Key { get; }
    }
}