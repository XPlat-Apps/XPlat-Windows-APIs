namespace XPlat.Storage
{
    using System;

    using XPlat.Foundation.Collections;

    public class ApplicationDataContainer : IApplicationDataContainer
    {
        public IApplicationDataContainer CreateContainer(string name, ApplicationDataCreateDisposition disposition)
        {
            throw new NotImplementedException();
        }

        public ApplicationDataLocality Locality { get; }

        public string Name { get; }

        public IPropertySet Values { get; }
    }
}