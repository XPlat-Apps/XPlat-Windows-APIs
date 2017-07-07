namespace XPlat.Storage
{
    using XPlat.Foundation.Collections;

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

        public static implicit operator Windows.Storage.ApplicationDataContainer(
            ApplicationDataContainer xPlatContainer)
        {
            return xPlatContainer.container;
        }

        /// <inheritdoc />
        public IApplicationDataContainer CreateContainer(string name, ApplicationDataCreateDisposition disposition)
        {
            return new ApplicationDataContainer(
                this.container.CreateContainer(name, disposition.ToApplicationDataCreateDisposition()));
        }

        /// <inheritdoc />
        public ApplicationDataLocality Locality => this.container.Locality.ToApplicationDataLocality();

        /// <inheritdoc />
        public string Name => this.container.Name;

        /// <inheritdoc />
        public IPropertySet Values => new ApplicationDataContainerSettings(this.container.Values);
    }
}