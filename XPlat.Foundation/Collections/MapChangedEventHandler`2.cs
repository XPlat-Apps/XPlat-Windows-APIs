namespace XPlat.Foundation.Collections
{
    /// <summary>Represents the method that handles the changed event of an observable map.</summary>
    /// <typeparam name="K">The type of the keys in the map.</typeparam>
    /// <typeparam name="V">The type of the values in the map.</typeparam>
    /// <param name="sender">The observable map that changed.</param>
    /// <param name="event">The description of the change that occurred in the map.</param>
    public delegate void MapChangedEventHandler<K, V>(IObservableMap<K, V> sender, IMapChangedEventArgs<K> @event);
}