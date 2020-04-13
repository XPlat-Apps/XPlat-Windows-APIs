#if WINDOWS_UWP
namespace XPlat.Storage
{
    using XPlat.Foundation.Collections;
    using XPlat.Storage.Extensions;

    public class ApplicationDataContainer : IApplicationDataContainer
    {
        private readonly Windows.Storage.ApplicationDataContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDataContainer"/> class.
        /// </summary>
        /// <param name="container">
        /// The Windows ApplicationDataContainer.
        /// </param>
        internal ApplicationDataContainer(Windows.Storage.ApplicationDataContainer container)
        {
            this.container = container;
        }

        /// <summary>Gets the type (local or roaming) of the app data store that is associated with the current settings container.</summary>
        public ApplicationDataLocality Locality => this.container.Locality.ToInternalApplicationDataLocality();

        /// <summary>Gets the name of the current settings container.</summary>
        public string Name => this.container.Name;

        /// <summary>Gets an object that represents the settings in this settings container.</summary>
        public IPropertySet Values => new ApplicationDataContainerSettings(this.container.Values);

        public static implicit operator ApplicationDataContainer(Windows.Storage.ApplicationDataContainer container)
        {
            return new ApplicationDataContainer(container);
        }

        public static implicit operator Windows.Storage.ApplicationDataContainer(
            ApplicationDataContainer container)
        {
            return container.container;
        }

        /// <summary>Creates or opens the specified settings container in the current settings container.</summary>
        /// <returns>The settings container.</returns>
        /// <param name="name">The name of the container.</param>
        /// <param name="disposition">One of the enumeration values.</param>
        public IApplicationDataContainer CreateContainer(string name, ApplicationDataCreateDisposition disposition)
        {
            return new ApplicationDataContainer(
                this.container.CreateContainer(name, disposition.ToWindowsApplicationDataCreateDisposition()));
        }

        /// <summary>Deletes the specified settings container, its subcontainers, and all application settings in the hierarchy.</summary>
        /// <param name="name">The name of the settings container.</param>
        public void DeleteContainer(string name)
        {
            this.container.DeleteContainer(name);
        }
    }
}
#endif