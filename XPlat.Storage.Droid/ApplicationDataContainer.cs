namespace XPlat.Storage
{
    using System;

    using XPlat.Foundation.Collections;

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

        /// <inheritdoc />
        /// <remarks>
        /// CreateContainer is not supported by Android.
        /// </remarks>
        public IApplicationDataContainer CreateContainer(string name, ApplicationDataCreateDisposition disposition)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public ApplicationDataLocality Locality { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public IPropertySet Values => this.settings;
    }
}