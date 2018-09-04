// <copyright file="IApplicationDataContainer.cs" company="James Croft">
// Copyright (c) James Croft. All rights reserved.
// </copyright>

namespace XPlat.Storage
{
    using XPlat.Foundation.Collections;

    /// <summary>
    /// Represents a container for app settings.
    /// </summary>
    public interface IApplicationDataContainer
    {
        /// <summary>
        /// Gets the type (local or roaming) of the app data store that is associated with the current settings container.
        /// </summary>
        ApplicationDataLocality Locality { get; }

        /// <summary>
        /// Gets the name of the current settings container.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets an object that represents the settings in this settings container.
        /// </summary>
        IPropertySet Values { get; }

        /// <summary>
        /// Creates or opens the specified settings container in the current settings container.
        /// </summary>
        /// <returns>
        /// The settings container.
        /// </returns>
        /// <param name="name">
        /// The name of the container.
        /// </param>
        /// <param name="disposition">
        /// One of the enumeration values.
        /// </param>
        IApplicationDataContainer CreateContainer(string name, ApplicationDataCreateDisposition disposition);
    }
}