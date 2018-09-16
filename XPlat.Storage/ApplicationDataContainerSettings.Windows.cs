#if WINDOWS_UWP
namespace XPlat.Storage
{
    using System.Collections;
    using System.Collections.Generic;

    using XPlat.Foundation.Collections;

    /// <summary>
    /// Provides access to the settings in a settings container. The ApplicationDataContainer.Values property returns an object that can be cast to this type.
    /// </summary>
    public sealed class ApplicationDataContainerSettings : IPropertySet
    {
        private readonly Windows.Storage.ApplicationDataContainerSettings containerSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDataContainerSettings"/> class.
        /// </summary>
        /// <param name="propertySet">
        /// The property set.
        /// </param>
        internal ApplicationDataContainerSettings(Windows.Foundation.Collections.IPropertySet propertySet)
        {
            this.containerSettings = (Windows.Storage.ApplicationDataContainerSettings)propertySet;
        }

        public static implicit operator ApplicationDataContainerSettings(Windows.Storage.ApplicationDataContainerSettings settings)
        {
            return new ApplicationDataContainerSettings(settings);
        }

        public static implicit operator Windows.Storage.ApplicationDataContainerSettings(
            ApplicationDataContainerSettings settings)
        {
            return settings.containerSettings;
        }

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<string, object>>)this.containerSettings).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.containerSettings).GetEnumerator();
        }

        /// <inheritdoc />
        public void Add(KeyValuePair<string, object> item)
        {
            this.containerSettings.Add(item);
        }

        /// <inheritdoc />
        public void Clear()
        {
            this.containerSettings.Clear();
        }

        /// <inheritdoc />
        public bool Contains(KeyValuePair<string, object> item)
        {
            return this.containerSettings.Contains(item);
        }

        /// <inheritdoc />
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            this.containerSettings.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc />
        public bool Remove(KeyValuePair<string, object> item)
        {
            return this.containerSettings.Remove(item);
        }

        /// <inheritdoc />
        public int Count => this.containerSettings.Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public void Add(string key, object value)
        {
            this.containerSettings.Add(key, value);
        }

        /// <inheritdoc />
        public bool ContainsKey(string key)
        {
            return this.containerSettings.ContainsKey(key);
        }

        /// <inheritdoc />
        public bool Remove(string key)
        {
            return this.containerSettings.Remove(key);
        }

        /// <inheritdoc />
        public bool TryGetValue(string key, out object value)
        {
            return this.containerSettings.TryGetValue(key, out value);
        }

        /// <inheritdoc />
        public object this[string key]
        {
            get
            {
                return this.containerSettings[key];
            }
            set
            {
                this.containerSettings[key] = value;
            }
        }

        /// <inheritdoc />
        public ICollection<string> Keys => this.containerSettings.Keys;

        /// <inheritdoc />
        public ICollection<object> Values => this.containerSettings.Values;

        /// <inheritdoc />
        public event MapChangedEventHandler<string, object> MapChanged;

        public T Get<T>(string key)
            where T : class
        {
            object value;

            if (this.TryGetValue(key, out value))
            {
                return value as T;
            }

            return null;
        }
    }
}
#endif