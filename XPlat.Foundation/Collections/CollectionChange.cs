// <copyright file="CollectionChange.cs" company="James Croft">
// Copyright (c) James Croft. All rights reserved.
// </copyright>

namespace XPlat.Foundation.Collections
{
    /// <summary>
    /// Describes the action that causes a change to a collection.
    /// </summary>
    public enum CollectionChange
    {
        Reset,

        ItemInserted,

        ItemRemoved,

        ItemChanged,
    }
}