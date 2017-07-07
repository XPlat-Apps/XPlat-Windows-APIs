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
        private readonly object obj = new object();

        private ApplicationDataLocality _locality;
        private ISharedPreferences sharedPreferences;

        internal ApplicationDataContainerSettings(ApplicationDataLocality locality)
        {
            this._locality = locality;
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
            lock (this.obj)
            {
                using (this.sharedPreferences)
                {
                    using (var editor = this.sharedPreferences.Edit())
                    {
                        editor.Clear();
                        editor.Commit();
                    }
                }
            }
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            return this.ContainsKey(item.Key) && this[item.Key] == item.Value;
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
            bool containsKey;

            lock (this.obj)
            {
                using (this.sharedPreferences)
                {
                    containsKey = this.sharedPreferences.Contains(key);
                }
            }

            return containsKey;
        }

        public void Add(string key, object value)
        {
            lock (this.obj)
            {
                var type = value.GetType();
                var code = Type.GetTypeCode(type);

                using (this.sharedPreferences)
                {
                    using (var editor = this.sharedPreferences.Edit())
                    {
                        switch (code)
                        {
                            case TypeCode.Boolean:
                                editor.PutBoolean(key, ParseHelper.SafeParseBool(value));
                                break;
                            case TypeCode.Int32:
                                editor.PutInt(key, ParseHelper.SafeParseInt(value));
                                break;
                            case TypeCode.Int64:
                                editor.PutLong(key, ParseHelper.SafeParseInt64(value));
                                break;
                            case TypeCode.Single:
                                editor.PutFloat(key, ParseHelper.SafeParseFloat(value));
                                break;
                            case TypeCode.Double:
                                editor.PutString(key, ParseHelper.SafeParseString(value));
                                break;
                            case TypeCode.Decimal:
                                editor.PutString(key, ParseHelper.SafeParseString(value));
                                break;
                            case TypeCode.DateTime:
                                editor.PutLong(key, ParseHelper.SafeParseDateTime(value).ToUniversalTime().Ticks);
                                break;
                            case TypeCode.String:
                                editor.PutString(key, ParseHelper.SafeParseString(value));
                                break;
                            default:
                                if (value is Guid)
                                {
                                    editor.PutString(key, ParseHelper.SafeParseGuid(value).ToString());
                                }
                                else
                                {
                                    editor.PutString(key, JsonConvert.SerializeObject(value));
                                }
                                break;
                        }

                        editor.Commit();
                    }
                }
            }
        }

        public bool Remove(string key)
        {
            if (!this.ContainsKey(key)) return false;

            lock (this.obj)
            {
                using (this.sharedPreferences)
                {
                    using (var editor = this.sharedPreferences.Edit())
                    {
                        editor.Remove(key);
                        editor.Commit();
                    }
                }
            }

            return true;
        }

        public bool TryGetValue(string key, out object value)
        {
            lock (this.obj)
            {
                using (this.sharedPreferences)
                {
                    var values = this.sharedPreferences.GetStringSet(key, new List<string> { "null", string.Empty });
                    var type = string.Empty;
                    var val = string.Empty;
                    foreach (var v in values)
                    {
                        if (string.IsNullOrEmpty(type))
                        {
                            type = v;
                        }
                        else
                        {
                            val = v;
                            break;
                        }
                    }

                    switch (type)
                    {
                         // ToDo   
                    }

                    value = val;
                    return true;
                }
            }
        }

        public object this[string key]
        {
            get
            {
                object value;

                lock (this.obj)
                {
                    using (this.sharedPreferences)
                    {
                        var values = this.sharedPreferences.GetStringSet(key, new List<string> { "null", string.Empty });
                        var type = string.Empty;
                        var val = string.Empty;
                        foreach (var v in values)
                        {
                            if (string.IsNullOrEmpty(type))
                            {
                                type = v;
                            }
                            else
                            {
                                val = v;
                                break;
                            }
                        }

                        switch (type)
                        {
                            // ToDo   
                        }

                        value = val;
                        return value;
                    }
                }
            }
            set
            {
                this.AddOrUpdate(key, value);
            }
        }

        private void AddOrUpdate(string key, object value)
        {
            lock (this.obj)
            {
                var type = value.GetType();
                var code = Type.GetTypeCode(type);

                using (this.sharedPreferences)
                {
                    using (var editor = this.sharedPreferences.Edit())
                    {
                        switch (code)
                        {
                            case TypeCode.Boolean:
                                editor.PutBoolean(key, ParseHelper.SafeParseBool(value));
                                break;
                            case TypeCode.Int32:
                                editor.PutInt(key, ParseHelper.SafeParseInt(value));
                                break;
                            case TypeCode.Int64:
                                editor.PutLong(key, ParseHelper.SafeParseInt64(value));
                                break;
                            case TypeCode.Single:
                                editor.PutFloat(key, ParseHelper.SafeParseFloat(value));
                                break;
                            case TypeCode.Double:
                                editor.PutString(key, ParseHelper.SafeParseString(value));
                                break;
                            case TypeCode.Decimal:
                                editor.PutString(key, ParseHelper.SafeParseString(value));
                                break;
                            case TypeCode.DateTime:
                                editor.PutLong(key, ParseHelper.SafeParseDateTime(value).ToUniversalTime().Ticks);
                                break;
                            case TypeCode.String:
                                editor.PutString(key, ParseHelper.SafeParseString(value));
                                break;
                            default:
                                if (value is Guid)
                                {
                                    editor.PutString(key, ParseHelper.SafeParseGuid(value).ToString());
                                }
                                else
                                {
                                    editor.PutString(key, JsonConvert.SerializeObject(value));
                                }
                                break;
                        }

                        editor.Commit();
                    }
                }
            }
        }

        public ICollection<string> Keys => GetSettingKeys();

        private ICollection<string> GetSettingKeys()
        {
            ICollection<string> genericKeys = new Collection<string>();

            lock (this.obj)
            {
                using (this.sharedPreferences)
                {
                    foreach (KeyValuePair<string, object> entry in this.sharedPreferences.All)
                    {
                        genericKeys.Add(entry.Key);
                    }
                }
            }

            return genericKeys;
        }

        public ICollection<object> Values { get; }

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
            throw new NotImplementedException();
        }
    }
}