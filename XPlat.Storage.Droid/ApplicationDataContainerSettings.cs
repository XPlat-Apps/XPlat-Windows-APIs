namespace XPlat.Storage
{
    using System.Collections;

    using XPlat.Foundation.Collections;

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Android.App;
    using Android.Content;
    using Android.Preferences;

    using Newtonsoft.Json;

    using XPlat.Helpers;

    public class ApplicationDataContainerSettings : Java.Lang.Object,
                                                    ISharedPreferencesOnSharedPreferenceChangeListener,
                                                    IPropertySet
    {
        private readonly ISharedPreferences sharedPreferences;

        private readonly JsonSerializerSettings jsonSettings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDataContainerSettings"/> class.
        /// </summary>
        internal ApplicationDataContainerSettings()
        {
            this.sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
        }

        public void OnSharedPreferenceChanged(ISharedPreferences sharedPreferences, string key)
        {
            this.mapChanged?.Invoke(this, new ApplicationDataContainerSettingsMapChangedEventArgs(key, CollectionChange.Reset));
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            throw new NotSupportedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotSupportedException();
        }

        public void Add(KeyValuePair<string, object> item)
        {
            this.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            ISharedPreferencesEditor editor = this.sharedPreferences.Edit();
            editor.Clear();
            editor.Commit();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            object storedValue;
            var expectedValue = item.Value;

            return this.TryGetValue(item.Key, out storedValue) && expectedValue == storedValue;
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            var index = arrayIndex;

            foreach (var key in this.Keys)
            {
                array[index++] = new KeyValuePair<string, object>(key, this[key]);
            }
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            return this.Remove(item.Key);
        }

        public int Count => this.sharedPreferences.All.Count;

        public bool IsReadOnly => false;

        public bool ContainsKey(string key)
        {
            object storedValue;
            bool success = this.TryGetValue(key, out storedValue);
            return success;
        }

        public void Add(string key, object value)
        {
            ISharedPreferencesEditor editor = this.sharedPreferences.Edit();

            var valueString = JsonConvert.SerializeObject(value, this.jsonSettings);
            editor.PutString(key, valueString);
            editor.Commit();
        }

        public bool Remove(string key)
        {
            ISharedPreferencesEditor editor = this.sharedPreferences.Edit();
            editor.Remove(key);
            return editor.Commit();
        }

        public bool TryGetValue(string key, out object value)
        {
            var valueString = this.sharedPreferences.GetString(key, string.Empty);
            if (!string.IsNullOrWhiteSpace(valueString))
            {
                value = JsonConvert.DeserializeObject(valueString, this.jsonSettings);
                return true;
            }

            value = null;
            return false;
        }

        public object this[string key]
        {
            get
            {
                var valueString = this.sharedPreferences.GetString(key, string.Empty);
                return !string.IsNullOrWhiteSpace(valueString) ? JsonConvert.DeserializeObject(valueString, this.jsonSettings) : null;
            }
            set
            {
                this.Add(key, value);
            }
        }

        public ICollection<string> Keys => this.GetSettingKeys();

        private ICollection<string> GetSettingKeys()
        {
            ICollection<string> keys = new Collection<string>();

            foreach (var preference in this.sharedPreferences.All)
            {
                keys.Add(preference.Key);
            }

            return keys;
        }

        public ICollection<object> Values => this.GetSettingValues();

        private ICollection<object> GetSettingValues()
        {
            Collection<object> values = new Collection<object>();

            foreach (var preference in this.sharedPreferences.All)
            {
                string valueString = preference.Value.ToString();
                var value = !string.IsNullOrWhiteSpace(valueString) ? JsonConvert.DeserializeObject(valueString, this.jsonSettings) : null;
                if (value != null)
                {
                    values.Add(value);
                }
            }

            return values;
        }

        private event MapChangedEventHandler<string, object> mapChanged;

        public event MapChangedEventHandler<string, object> MapChanged
        {
            add
            {
                if (this.mapChanged == null)
                {
                    this.sharedPreferences.RegisterOnSharedPreferenceChangeListener(this);
                }
                this.mapChanged += value;
            }

            remove
            {
                this.mapChanged -= value;

                if (this.mapChanged == null)
                {
                    this.sharedPreferences.UnregisterOnSharedPreferenceChangeListener(this);
                }
            }
        }

        public T Get<T>(string key)
            where T : class
        {
            object storedValue;
            this.TryGetValue(key, out storedValue);
            return storedValue as T;
        }
    }
}