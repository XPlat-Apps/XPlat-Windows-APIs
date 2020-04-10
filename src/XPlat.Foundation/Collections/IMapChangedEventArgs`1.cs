﻿namespace XPlat.Foundation.Collections
{
    /// <summary>Provides data for the changed event of a map collection.</summary>
    /// <typeparam name="K">The type of the keys in the map.</typeparam>
    public interface IMapChangedEventArgs<K>
    {
        /// <summary>Gets the type of change that occurred in the map.</summary>
        CollectionChange CollectionChange { get; }

        /// <summary>Gets the key of the item that changed.</summary>
        K Key { get; }
    }
}