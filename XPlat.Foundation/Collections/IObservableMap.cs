namespace XPlat.Foundation.Collections
{
    using System.Collections.Generic;

    /// <summary>
    /// Notifies listeners of dynamic changes to a map, such as when items are added or removed.
    /// </summary>
    /// <typeparam name="TKey">
    /// The type of the keys in the map.
    /// </typeparam>
    /// <typeparam name="TValue">
    /// The type of the values in the map.
    /// </typeparam>
    public interface IObservableMap<TKey, TValue> : IDictionary<TKey, TValue>
    {
        /// <summary>
        /// Occurs when the map changes.
        /// </summary>
        event MapChangedEventHandler<TKey, TValue> MapChanged;
    }
}