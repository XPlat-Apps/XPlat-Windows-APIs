namespace XamKit.Core.Storage
{
    using System;

    using Android.App;
    using Android.Content;
    using Android.Preferences;

    using XamKit.Common;
    using XamKit.Core.Common.Storage;

    using Helpers = XamKit.Helpers.Helpers;

    public class AppSettings : IAppSettings
    {
        private readonly object obj = new object();

        private static ISharedPreferences SharedPreferences
        {
            get
            {
                return PreferenceManager.GetDefaultSharedPreferences(Application.Context);
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
                using (SharedPreferences)
                {
                    containsKey = SharedPreferences.Contains(key);
                }
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
                    var type = value.GetType();
                    var code = Type.GetTypeCode(type);

                    object setting;

                    using (SharedPreferences)
                    {
                        switch (code)
                        {
                            case TypeCode.Boolean:
                                setting = SharedPreferences.GetBoolean(key, Helpers.SafeParseBool(value));
                                break;
                            case TypeCode.Int32:
                                setting = SharedPreferences.GetInt(key, Helpers.SafeParseInt(value));
                                break;
                            case TypeCode.Int64:
                                setting = SharedPreferences.GetLong(key, Helpers.SafeParseInt64(value));
                                break;
                            case TypeCode.Single:
                                setting = SharedPreferences.GetFloat(key, Helpers.SafeParseFloat(value));
                                break;
                            case TypeCode.Double:
                                var dbl = SharedPreferences.GetString(key, "0");
                                setting = Helpers.SafeParseDouble(dbl);
                                break;
                            case TypeCode.Decimal:
                                var dcml = SharedPreferences.GetString(key, "0");
                                setting = Helpers.SafeParseDecimal(dcml);
                                break;
                            case TypeCode.DateTime:
                                var dateTimeTicks = SharedPreferences.GetLong(key, 0);
                                setting = new DateTime(dateTimeTicks, DateTimeKind.Utc);
                                break;
                            case TypeCode.String:
                                setting = SharedPreferences.GetString(key, Helpers.SafeParseString(value));
                                break;
                            default:
                                if (value is Guid)
                                {
                                    setting = Guid.Empty;
                                    var val = SharedPreferences.GetString(key, Guid.Empty.ToString());
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
                var code = Type.GetTypeCode(type);

                using (SharedPreferences)
                {
                    using (var editor = SharedPreferences.Edit())
                    {
                        switch (code)
                        {
                            case TypeCode.Boolean:
                                editor.PutBoolean(key, Helpers.SafeParseBool(value));
                                break;
                            case TypeCode.Int32:
                                editor.PutInt(key, Helpers.SafeParseInt(value));
                                break;
                            case TypeCode.Int64:
                                editor.PutLong(key, Helpers.SafeParseInt64(value));
                                break;
                            case TypeCode.Single:
                                editor.PutFloat(key, Helpers.SafeParseFloat(value));
                                break;
                            case TypeCode.Double:
                                editor.PutString(key, Helpers.SafeParseString(value));
                                break;
                            case TypeCode.Decimal:
                                editor.PutString(key, Helpers.SafeParseString(value));
                                break;
                            case TypeCode.DateTime:
                                editor.PutLong(key, Helpers.SafeParseDateTime(value).ToUniversalTime().Ticks);
                                break;
                            case TypeCode.String:
                                editor.PutString(key, Helpers.SafeParseString(value));
                                break;
                            default:
                                if (value is Guid)
                                {
                                    editor.PutString(key, Helpers.SafeParseGuid(value).ToString());
                                }
                                else
                                {
                                    throw new ArgumentException("The provided value is not a supported type.");
                                }
                                break;
                        }

                        editor.Commit();
                    }
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
                using (SharedPreferences)
                {
                    if (SharedPreferences.Contains(key))
                    {
                        using (var editor = SharedPreferences.Edit())
                        {
                            editor.Remove(key);
                            editor.Commit();
                        }
                    }
                }
            }
        }
    }
}