namespace XPlat.Storage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using global::Foundation;

    using Newtonsoft.Json;

    using XPlat.Helpers;

    /// <summary>
    /// Defines application settings.
    /// </summary>
    public sealed class AppSettingsContainer : IAppSettingsContainer
    {
        private readonly object obj = new object();

        private static NSUserDefaults StandardUserDefaults => NSUserDefaults.StandardUserDefaults;

        public IDictionary<string, object> Values => GetAllValues();

        private static IDictionary<string, object> GetAllValues()
        {
            var items = StandardUserDefaults.ToDictionary();
            return items.ToDictionary<KeyValuePair<NSObject, NSObject>, string, object>(item => item.Key.ToString(), item => item.Value);
        }

        /// <inheritdoc />
        public bool ContainsKey(string key)
        {
            bool containsKey;

            lock (this.obj)
            {
                containsKey = StandardUserDefaults.ValueForKey(new NSString(key)) != null;
            }

            return containsKey;
        }

        /// <inheritdoc />
        public T Get<T>(string key)
        {
            var value = default(T);

            if (!this.ContainsKey(key)) return value;

            lock (this.obj)
            {
                var type = typeof(T);
                var code = Type.GetTypeCode(type);

                object setting;

                switch (code)
                {
                    case TypeCode.Boolean:
                        setting = StandardUserDefaults.BoolForKey(key);
                        break;
                    case TypeCode.Int32:
                        setting = StandardUserDefaults.IntForKey(key);
                        break;
                    case TypeCode.Int64:
                        var int64 = StandardUserDefaults.StringForKey(key);
                        setting = ParseHelper.SafeParseInt64(int64);
                        break;
                    case TypeCode.Single:
                        setting = StandardUserDefaults.FloatForKey(key);
                        break;
                    case TypeCode.Double:
                        setting = StandardUserDefaults.DoubleForKey(key);
                        break;
                    case TypeCode.Decimal:
                        var dec = StandardUserDefaults.StringForKey(key);
                        setting = ParseHelper.SafeParseDecimal(dec);
                        break;
                    case TypeCode.DateTime:
                        var date = StandardUserDefaults.StringForKey(key);
                        setting = ParseHelper.SafeParseDateTime(date);
                        break;
                    case TypeCode.String:
                        setting = StandardUserDefaults.StringForKey(key);
                        break;
                    default:
                        if (value is Guid)
                        {
                            setting = Guid.Empty;
                            var val = StandardUserDefaults.StringForKey(key);
                            if (!string.IsNullOrWhiteSpace(val))
                            {
                                setting = ParseHelper.SafeParseGuid(val);
                            }
                        }
                        else
                        {
                            string json = StandardUserDefaults.StringForKey(key);
                            setting = JsonConvert.DeserializeObject<T>(json);
                        }
                        break;
                }

                value = (T)setting;
            }

            return value;
        }

        /// <inheritdoc />
        public void AddOrUpdate(string key, object value)
        {
            lock (this.obj)
            {
                var type = value.GetType();
                var code = Type.GetTypeCode(type);

                switch (code)
                {
                    case TypeCode.Boolean:
                        StandardUserDefaults.SetBool(ParseHelper.SafeParseBool(value), key);
                        break;
                    case TypeCode.Int32:
                        StandardUserDefaults.SetInt(ParseHelper.SafeParseInt(value), key);
                        break;
                    case TypeCode.Int64:
                        StandardUserDefaults.SetString(ParseHelper.SafeParseString(value), key);
                        break;
                    case TypeCode.Single:
                        StandardUserDefaults.SetFloat(ParseHelper.SafeParseFloat(value), key);
                        break;
                    case TypeCode.Double:
                        StandardUserDefaults.SetDouble(ParseHelper.SafeParseDouble(value), key);
                        break;
                    case TypeCode.Decimal:
                        StandardUserDefaults.SetString(ParseHelper.SafeParseString(value), key);
                        break;
                    case TypeCode.DateTime:
                        StandardUserDefaults.SetString(ParseHelper.SafeParseString(value), key);
                        break;
                    case TypeCode.String:
                        StandardUserDefaults.SetString(ParseHelper.SafeParseString(value), key);
                        break;
                    default:
                        if (value is Guid)
                        {
                            StandardUserDefaults.SetString(ParseHelper.SafeParseString(value), key);
                        }
                        else
                        {
                            StandardUserDefaults.SetString(JsonConvert.SerializeObject(value), key);
                        }
                        break;
                }

                try
                {
                    StandardUserDefaults.Synchronize();
                }
                catch (Exception)
                {
                }
            }
        }

        /// <inheritdoc />
        public void Remove(string key)
        {
            if (!this.ContainsKey(key)) return;

            lock (this.obj)
            {
                try
                {
                    StandardUserDefaults.RemoveObject(key);
                    StandardUserDefaults.Synchronize();
                }
                catch (Exception)
                {
                }
            }
        }
    }
}