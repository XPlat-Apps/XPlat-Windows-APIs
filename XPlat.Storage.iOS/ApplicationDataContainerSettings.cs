namespace XPlat.Storage
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using global::Foundation;

    using Newtonsoft.Json;

    using XPlat.Foundation.Collections;
    using XPlat.Helpers;

    public class ApplicationDataContainerSettings : IPropertySet
    {
        private readonly object obj = new object();

        private readonly NSUserDefaults standardUserDefaults;

        private readonly NSUbiquitousKeyValueStore defaultKeyValueStore;

        private NSObject observer;

        private readonly ApplicationDataLocality locality;

        private event MapChangedEventHandler<string, object> mapChanged;

        internal ApplicationDataContainerSettings(ApplicationDataLocality locality, string name)
        {
            this.locality = locality;
            switch (this.locality)
            {
                case ApplicationDataLocality.Roaming:
                    this.defaultKeyValueStore = NSUbiquitousKeyValueStore.DefaultStore;
                    break;
                case ApplicationDataLocality.Shared:
                    this.standardUserDefaults = new NSUserDefaults(name, NSUserDefaultsType.SuiteName);
                    if (this.standardUserDefaults == null)
                    {
                        throw new InvalidOperationException($"Could not get shared settings for {name}.");
                    }
                    break;
                default:
                    this.standardUserDefaults = NSUserDefaults.StandardUserDefaults;
                    break;
            }
        }

        private bool IsRoaming => this.locality == ApplicationDataLocality.Roaming;

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            throw new NotSupportedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public void Add(KeyValuePair<string, object> item)
        {
            this.Add(item.Key, item.Value);
        }

        /// <inheritdoc />
        public void Clear()
        {
            if (this.IsRoaming)
            {
                lock (this.obj)
                {
                    this.defaultKeyValueStore.Init();
                }
            }
            else
            {
                lock (this.obj)
                {
                    this.standardUserDefaults.Init();
                }
            }
        }

        /// <inheritdoc />
        public bool Contains(KeyValuePair<string, object> item)
        {
            return this.ContainsKey(item.Key) && this[item.Key] == item.Value;
        }

        /// <inheritdoc />
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public bool Remove(KeyValuePair<string, object> item)
        {
            return this.Remove(item.Key);
        }

        /// <inheritdoc />
        public int Count => this.IsRoaming
                                ? this.defaultKeyValueStore.ToDictionary()
                                    .ToDictionary<KeyValuePair<NSObject, NSObject>, string, object>(
                                        item => item.Key.ToString(),
                                        item => item.Value).Count
                                : this.standardUserDefaults.ToDictionary()
                                    .ToDictionary<KeyValuePair<NSObject, NSObject>, string, object>(
                                        item => item.Key.ToString(),
                                        item => item.Value).Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public bool ContainsKey(string key)
        {
            bool containsKey;

            if (this.IsRoaming)
            {
                lock (this.obj)
                {
                    containsKey = this.defaultKeyValueStore.ValueForKey(new NSString(key)) != null;
                }
            }
            else
            {
                lock (this.obj)
                {
                    containsKey = this.standardUserDefaults.ValueForKey(new NSString(key)) != null;
                }
            }

            return containsKey;
        }

        /// <inheritdoc />
        public void Add(string key, object value)
        {
            this[key] = value;
        }

        /// <inheritdoc />
        public bool Remove(string key)
        {
            if (!this.ContainsKey(key)) return false;

            if (this.IsRoaming)
            {
                lock (this.obj)
                {
                    try
                    {
                        this.defaultKeyValueStore.Remove(key);
                        return true;
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }
            else
            {
                lock (this.obj)
                {
                    try
                    {
                        this.standardUserDefaults.RemoveObject(key);
                        this.standardUserDefaults.Synchronize();
                        return true;
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }

            return false;
        }

        /// <inheritdoc />
        public bool TryGetValue(string key, out object value)
        {
            NSObject nsObj = null;

            if (!this.ContainsKey(key))
            {
                value = null;
            }
            else
            {
                if (this.IsRoaming)
                {
                    lock (this.obj)
                    {
                        nsObj = this.defaultKeyValueStore.ValueForKey(new NSString(key));
                    }
                }
                else
                {
                    lock (this.obj)
                    {
                        nsObj = this.standardUserDefaults.ValueForKey(new NSString(key));
                    }
                }

                value = nsObj.ToObject();
            }

            return nsObj != null;
        }

        /// <inheritdoc />
        public object this[string key]
        {
            get
            {
                object value = null;

                if (this.ContainsKey(key))
                {
                    NSObject nsObj;
                    if (this.IsRoaming)
                    {
                        lock (this.obj)
                        {
                            nsObj = this.defaultKeyValueStore.ValueForKey(new NSString(key));
                        }
                    }
                    else
                    {
                        lock (this.obj)
                        {
                            nsObj = this.standardUserDefaults.ValueForKey(new NSString(key));
                        }
                    }

                    value = nsObj.ToObject();
                }

                return value;
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

                switch (code)
                {
                    case TypeCode.Boolean:
                        if (this.IsRoaming)
                        {
                            this.defaultKeyValueStore.SetBool(key, ParseHelper.SafeParseBool(value));
                        }
                        else
                        {
                            this.standardUserDefaults.SetBool(ParseHelper.SafeParseBool(value), key);
                        }
                        break;
                    case TypeCode.Int32:
                        if (this.IsRoaming)
                        {
                            this.defaultKeyValueStore.SetString(key, ParseHelper.SafeParseString(value));
                        }
                        else
                        {
                            this.standardUserDefaults.SetInt(ParseHelper.SafeParseInt(value), key);
                        }
                        break;
                    case TypeCode.Single:
                        if (this.IsRoaming)
                        {
                            this.defaultKeyValueStore.SetString(key, ParseHelper.SafeParseString(value));
                        }
                        else
                        {
                            this.standardUserDefaults.SetFloat(ParseHelper.SafeParseFloat(value), key);
                        }
                        break;
                    case TypeCode.Double:
                        if (this.IsRoaming)
                        {
                            this.defaultKeyValueStore.SetDouble(key, ParseHelper.SafeParseDouble(value));
                        }
                        else
                        {
                            this.standardUserDefaults.SetDouble(ParseHelper.SafeParseDouble(value), key);
                        }
                        break;
                    case TypeCode.Int64:
                    case TypeCode.Decimal:
                    case TypeCode.DateTime:
                    case TypeCode.String:
                        if (this.IsRoaming)
                        {
                            this.defaultKeyValueStore.SetString(key, ParseHelper.SafeParseString(value));
                        }
                        else
                        {
                            this.standardUserDefaults.SetString(ParseHelper.SafeParseString(value), key);
                        }
                        break;
                    default:
                        if (value is Guid)
                        {
                            if (this.IsRoaming)
                            {
                                this.defaultKeyValueStore.SetString(key, ParseHelper.SafeParseString(value));
                            }
                            else
                            {
                                this.standardUserDefaults.SetString(ParseHelper.SafeParseString(value), key);
                            }
                        }
                        else
                        {
                            if (this.IsRoaming)
                            {
                                this.defaultKeyValueStore.SetString(key, JsonConvert.SerializeObject(value));
                            }
                            else
                            {
                                this.standardUserDefaults.SetString(JsonConvert.SerializeObject(value), key);
                            }
                        }
                        break;
                }

                if (!this.IsRoaming)
                {
                    try
                    {
                        this.standardUserDefaults.Synchronize();
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }
        }

        /// <inheritdoc />
        public ICollection<string> Keys => this.defaultKeyValueStore.ToDictionary()
            .ToDictionary<KeyValuePair<NSObject, NSObject>, string, object>(
                item => item.Key.ToString(),
                item => item.Value).Keys;

        /// <inheritdoc />
        public ICollection<object> Values => this.defaultKeyValueStore.ToDictionary()
            .ToDictionary<KeyValuePair<NSObject, NSObject>, string, object>(
                item => item.Key.ToString(),
                item => item.Value).Values;

        /// <inheritdoc />
        public event MapChangedEventHandler<string, object> MapChanged
        {
            add
            {
                if (this.mapChanged == null && !this.IsRoaming)
                {
                    this.observer = NSNotificationCenter.DefaultCenter.AddObserver(
                        new NSString("NSUserDefaultsDidChangeNotification"),
                        n =>
                            {
                                this.mapChanged?.Invoke(
                                    this,
                                    new ApplicationDataContainerSettingsMapChangedEventArgs(
                                        null,
                                        CollectionChange.Reset));
                            });
                }
                this.mapChanged += value;
            }

            remove => this.mapChanged -= value;
        }

        public T Get<T>(string key) where T : class
        {
            if (!this.ContainsKey(key)) return null;

            var type = typeof(T);
            var code = Type.GetTypeCode(type);

            object setting;

            switch (code)
            {
                case TypeCode.Boolean:
                    lock (this.obj)
                    {
                        setting = this.IsRoaming
                                      ? this.defaultKeyValueStore.GetBool(key)
                                      : this.standardUserDefaults.BoolForKey(key);
                    }
                    break;
                case TypeCode.Int32:
                    lock (this.obj)
                    {
                        setting = this.IsRoaming
                                      ? ParseHelper.SafeParseInt(this.defaultKeyValueStore.GetString(key))
                                      : this.standardUserDefaults.IntForKey(key);
                    }
                    break;
                case TypeCode.Int64:
                    lock (this.obj)
                    {
                        setting = this.IsRoaming
                                      ? ParseHelper.SafeParseInt64(this.defaultKeyValueStore.GetString(key))
                                      : ParseHelper.SafeParseInt64(this.standardUserDefaults.StringForKey(key));
                    }
                    break;
                case TypeCode.Single:
                    lock (this.obj)
                    {
                        setting = this.IsRoaming
                                      ? ParseHelper.SafeParseFloat(this.defaultKeyValueStore.GetString(key))
                                      : this.standardUserDefaults.FloatForKey(key);
                    }
                    break;
                case TypeCode.Double:
                    lock (this.obj)
                    {
                        setting = this.IsRoaming
                                      ? this.defaultKeyValueStore.GetDouble(key)
                                      : this.standardUserDefaults.DoubleForKey(key);
                    }
                    break;
                case TypeCode.Decimal:
                    lock (this.obj)
                    {
                        setting = this.IsRoaming
                                      ? ParseHelper.SafeParseDecimal(this.defaultKeyValueStore.GetString(key))
                                      : ParseHelper.SafeParseDecimal(this.standardUserDefaults.StringForKey(key));
                    }
                    break;
                case TypeCode.DateTime:
                    lock (this.obj)
                    {
                        setting = this.IsRoaming
                                      ? ParseHelper.SafeParseDateTime(this.defaultKeyValueStore.GetString(key))
                                      : ParseHelper.SafeParseDateTime(this.standardUserDefaults.StringForKey(key));
                    }
                    break;
                case TypeCode.String:
                    lock (this.obj)
                    {
                        setting = this.IsRoaming
                                      ? this.defaultKeyValueStore.GetString(key)
                                      : this.standardUserDefaults.StringForKey(key);
                    }
                    break;
                default:
                    lock (this.obj)
                    {
                        if (this.IsRoaming)
                        {
                            string json = this.defaultKeyValueStore.GetString(key);
                            setting = JsonConvert.DeserializeObject<T>(json);
                        }
                        else
                        {
                            string json = this.standardUserDefaults.StringForKey(key);
                            setting = JsonConvert.DeserializeObject<T>(json);
                        }
                    }
                    break;
            }

            var value = (T)setting;

            return value;
        }
    }
}