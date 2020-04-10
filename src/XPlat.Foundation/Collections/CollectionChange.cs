// <copyright file="CollectionChange.cs" company="James Croft">
// Copyright (c) James Croft. All rights reserved.
// </copyright>

namespace XPlat.Foundation.Collections
{
    /// <summary>Describes the action that causes a change to a collection.</summary>
    public enum CollectionChange
    {
        /// <summary>The collection is changed.</summary>
        Reset,

        /// <summary>An item is added to the collection.</summary>
        ItemInserted,

        /// <summary>An item is removed from the collection.</summary>
        ItemRemoved,

        /// <summary>An item is changed in the collection.</summary>
        ItemChanged,
    }
}