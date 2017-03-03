namespace XPlat.API.Storage
{
    using System;

    using Android.App;
    using Android.Content;
    using Android.Preferences;

    using WinUX.Common;

    /// <summary>
    /// Defines application settings.
    /// </summary>
    public sealed class AppSettingsContainer : IAppSettingsContainer
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
        /// Checks whether the settings container contains the specified key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// Returns true if exists; else false.
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
        /// Gets the value from the settings container for the specified key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <typeparam name="T">
        /// The type for the stored value.
        /// </typeparam>
        /// <returns>
        /// Returns the stored value.
        /// </returns>
        public T Get<T>(string key)
        {
            var value = default(T);

            if (!this.ContainsKey(key)) return value;

            lock (this.obj)
            {
                var type = typeof(T);
                var code = Type.GetTypeCode(type);

                object setting;

                using (SharedPreferences)
                {
                    switch (code)
                    {
                        case TypeCode.Boolean:
                            setting = SharedPreferences.GetBoolean(key, ParseHelper.SafeParseBool(value));
                            break;
                        case TypeCode.Int32:
                            setting = SharedPreferences.GetInt(key, ParseHelper.SafeParseInt(value));
                            break;
                        case TypeCode.Int64:
                            setting = SharedPreferences.GetLong(key, ParseHelper.SafeParseInt64(value));
                            break;
                        case TypeCode.Single:
                            setting = SharedPreferences.GetFloat(key, ParseHelper.SafeParseFloat(value));
                            break;
                        case TypeCode.Double:
                            var dbl = SharedPreferences.GetString(key, "0");
                            setting = ParseHelper.SafeParseDouble(dbl);
                            break;
                        case TypeCode.Decimal:
                            var dcml = SharedPreferences.GetString(key, "0");
                            setting = ParseHelper.SafeParseDecimal(dcml);
                            break;
                        case TypeCode.DateTime:
                            var dateTimeTicks = SharedPreferences.GetLong(key, 0);
                            setting = new DateTime(dateTimeTicks, DateTimeKind.Utc);
                            break;
                        case TypeCode.String:
                            setting = SharedPreferences.GetString(key, ParseHelper.SafeParseString(value));
                            break;
                        default:
                            if (value is Guid)
                            {
                                setting = Guid.Empty;
                                var val = SharedPreferences.GetString(key, Guid.Empty.ToString());
                                if (!string.IsNullOrWhiteSpace(val))
                                {
                                    setting = ParseHelper.SafeParseGuid(val);
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

            return value;
        }

        /// <summary>
        /// Adds or updates a key in the settings container with the specified value.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
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
        /// Removes the specified key's value from the settings container.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        public void Remove(string key)
        {
            if (!this.ContainsKey(key)) return;

            lock (this.obj)
            {
                using (SharedPreferences)
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