namespace XamKit.Core.Storage
{
    using System;

    using Foundation;

    using XamKit.Common.Helpers;
    using XamKit.Core.Common.Storage;

    public class AppSettings : IAppSettings
    {
        private readonly object obj = new object();

        private static NSUserDefaults StandardUserDefaults
        {
            get
            {
                return NSUserDefaults.StandardUserDefaults;
            }
        }

        /// <summary>
        /// Determines whether the application settings contains an element with the specified key.
        /// </summary>
        /// <param name="key">
        /// The key to locate in the application settings.
        /// </param>
        /// <returns>
        /// Returns a <see cref="bool"/> value indicating whether the key exists.
        /// </returns>
        public bool ContainsKey(string key)
        {
            bool containsKey;

            lock (this.obj)
            {
                containsKey = StandardUserDefaults.ValueForKey(new NSString(key)) != null;
            }

            return containsKey;
        }

        /// <summary>
        /// Gets a value from the application settings with the specified key.
        /// </summary>
        /// <param name="key">
        /// The key to locate the value for in the application settings.
        /// </param>
        /// <param name="value">
        /// The value to return.
        /// </param>
        /// <typeparam name="T">
        /// The type of the value stored in the application settings.
        /// </typeparam>
        /// <returns>
        /// Returns the stored <see cref="T"/> value, if available, else returns the default.
        /// </returns>
        public T Get<T>(string key, T value = default(T))
        {
            lock (this.obj)
            {
                if (this.ContainsKey(key))
                {
                    Type type = typeof(T);
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        type = Nullable.GetUnderlyingType(type);
                    }

                    object setting = null;

                    var code = Type.GetTypeCode(type);
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
                            setting = Helpers.SafeParseInt64(int64);
                            break;
                        case TypeCode.Single:
                            setting = StandardUserDefaults.FloatForKey(key);
                            break;
                        case TypeCode.Double:
                            setting = StandardUserDefaults.DoubleForKey(key);
                            break;
                        case TypeCode.Decimal:
                            var dec = StandardUserDefaults.StringForKey(key);
                            setting = Helpers.SafeParseDecimal(dec);
                            break;
                        case TypeCode.DateTime:
                            var date = StandardUserDefaults.StringForKey(key);
                            setting = Helpers.SafeParseDateTime(date);
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
                                    setting = Helpers.SafeParseGuid(val);
                                }
                            }
                            else
                            {
                                throw new ArgumentException("The provided value is not a supported type.");
                            }
                            break;
                    }

                    value = (T)setting;
                }
            }

            return value;
        }

        /// <summary>
        /// Adds or updates a value in the application settings with the specified key and value.
        /// </summary>
        /// <param name="key">
        /// The key to add or update.
        /// </param>
        /// <param name="value">
        /// The value to add or update the key with.
        /// </param>
        public void AddOrUpdate(string key, object value)
        {
            lock (this.obj)
            {
                var type = value.GetType();
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    type = Nullable.GetUnderlyingType(type);
                }

                var code = Type.GetTypeCode(type);
                switch (code)
                {
                    case TypeCode.Boolean:
                        StandardUserDefaults.SetBool(Helpers.SafeParseBool(value), key);
                        break;
                    case TypeCode.Int32:
                        StandardUserDefaults.SetInt(Helpers.SafeParseInt(value), key);
                        break;
                    case TypeCode.Int64:
                        StandardUserDefaults.SetString(Helpers.SafeParseString(value), key);
                        break;
                    case TypeCode.Single:
                        StandardUserDefaults.SetFloat(Helpers.SafeParseFloat(value), key);
                        break;
                    case TypeCode.Double:
                        StandardUserDefaults.SetDouble(Helpers.SafeParseDouble(value), key);
                        break;
                    case TypeCode.Decimal:
                        StandardUserDefaults.SetString(Helpers.SafeParseString(value), key);
                        break;
                    case TypeCode.DateTime:
                        StandardUserDefaults.SetString(Helpers.SafeParseString(value), key);
                        break;
                    case TypeCode.String:
                        StandardUserDefaults.SetString(Helpers.SafeParseString(value), key);
                        break;
                    default:
                        if (value is Guid)
                        {
                            StandardUserDefaults.SetString(Helpers.SafeParseString(value), key);
                        }
                        else
                        {
                            throw new ArgumentException("The provided value is not a supported type.");
                        }
                        break;
                }

                try
                {
                    StandardUserDefaults.Synchronize();
                }
                catch (Exception ex)
                {
                    // Log exception
                }
            }
        }

        /// <summary>
        /// Removes a key and value from the application settings with the specified key.
        /// </summary>
        /// <param name="key">
        /// The key to remove.
        /// </param>
        public void Remove(string key)
        {
            lock (this.obj)
            {
                if (this.ContainsKey(key))
                {
                    try
                    {
                        StandardUserDefaults.RemoveObject(key);
                        StandardUserDefaults.Synchronize();
                    }
                    catch (Exception ex)
                    {
                        // Log exception
                    }
                }
            }
        }
    }
}