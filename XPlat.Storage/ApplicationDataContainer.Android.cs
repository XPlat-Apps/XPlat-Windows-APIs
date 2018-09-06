#if __ANDROID__
namespace XPlat.Storage
{
    using System;

    using XPlat.Foundation.Collections;

    /// <summary>Represents a container for app settings.</summary>
    public class ApplicationDataContainer : IApplicationDataContainer
    {
        private readonly ApplicationDataContainerSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDataContainer"/> class.
        /// </summary>
        /// <param name="locality">
        /// The locality.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        internal ApplicationDataContainer(ApplicationDataLocality locality, string name)
        {
            this.Locality = locality;
            this.Name = name;
            this.settings = new ApplicationDataContainerSettings();
        }

        /// <summary>Creates or opens the specified settings container in the current settings container.</summary>
        public IApplicationDataContainer CreateContainer(string name, ApplicationDataCreateDisposition disposition)
        {
            throw new NotSupportedException("CreateContainer is not currently supported by Android.");
        }

        /// <summary>Deletes the specified settings container, its subcontainers, and all application settings in the hierarchy.</summary>
        /// <param name="name">The name of the settings container.</param>
        public void DeleteContainer(string name)
        {
            throw new NotSupportedException("DeleteContainer is not currently supported by Android.");
        }

        /// <summary>Gets the type (local or roaming) of the app data store that is associated with the current settings container.</summary>
        public ApplicationDataLocality Locality { get; }

        /// <summary>Gets the name of the current settings container.</summary>
        public string Name { get; }

        /// <summary>Gets an object that represents the settings in this settings container.</summary>
        public IPropertySet Values => this.settings;
    }
}
#endif