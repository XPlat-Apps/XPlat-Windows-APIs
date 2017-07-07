namespace XPlat.Storage
{
    using System;

    using XPlat.Foundation.Collections;

    public class ApplicationDataContainer : IApplicationDataContainer
    {
        private readonly ApplicationDataContainerSettings settings;

        internal ApplicationDataContainer(ApplicationDataLocality locality, string name)
        {
            this.Locality = locality;
            this.Name = name;
            this.settings = new ApplicationDataContainerSettings(locality, name);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public ApplicationDataLocality Locality { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public IPropertySet Values => this.settings;
    }
}