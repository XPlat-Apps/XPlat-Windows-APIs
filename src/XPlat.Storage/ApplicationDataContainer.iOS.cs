#if __IOS__
namespace XPlat.Storage
{
    using System;

    using XPlat.Foundation.Collections;

    /// <summary>Represents a container for app settings.</summary>
    public class ApplicationDataContainer : IApplicationDataContainer
    {
        private readonly ApplicationDataContainerSettings settings;

        internal ApplicationDataContainer(ApplicationDataLocality locality, string name)
        {
            this.Locality = locality;
            this.Name = name;
            this.settings = new ApplicationDataContainerSettings(locality, name);
        }

        /// <summary>Gets the type (local or roaming) of the app data store that is associated with the current settings container.</summary>
        public ApplicationDataLocality Locality { get; }

        /// <summary>Gets the name of the current settings container.</summary>
        public string Name { get; }

        /// <summary>Gets an object that represents the settings in this settings container.</summary>
        public IPropertySet Values => this.settings;

        /// <summary>Creates or opens the specified settings container in the current settings container.</summary>
        /// <returns>The settings container.</returns>
        /// <param name="name">The name of the container.</param>
        /// <param name="disposition">One of the enumeration values.</param>
        public IApplicationDataContainer CreateContainer(string name, ApplicationDataCreateDisposition disposition)
        {
            if (disposition != ApplicationDataCreateDisposition.Existing)
            {
                throw new ArgumentException(
                    "iOS only supports ApplicationDataCreateDisposition.Existing.",
                    nameof(disposition));
            }

            return new ApplicationDataContainer(ApplicationDataLocality.Shared, name);
        }

        /// <summary>Deletes the specified settings container, its subcontainers, and all application settings in the hierarchy.</summary>
        /// <param name="name">The name of the settings container.</param>
        public void DeleteContainer(string name)
        {
            throw new NotSupportedException("DeleteContainer is not currently supported by iOS.");
        }
    }
}
#endif