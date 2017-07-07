namespace XPlat.Foundation.Collections
{
    using System.Collections.Generic;

    /// <summary>
    /// Notifies listeners of dynamic changes to a map, such as when items are added or removed.
    /// </summary>
    /// <typeparam name="K">
    /// The type of the keys in the map.
    /// </typeparam>
    /// <typeparam name="V">
    /// The type of the values in the map.
    /// </typeparam>
    public interface IObservableMap<K, V> : IDictionary<K, V>
    {
        /// <summary>
        /// Occurs when the map changes.
        /// </summary>
        event MapChangedEventHandler<K, V> MapChanged;
    }
}