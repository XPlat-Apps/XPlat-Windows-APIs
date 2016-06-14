namespace XamKit.Core.Storage
{
    using System;

    using Android.App;
    using Android.Content;
    using Android.Preferences;

    using XamKit.Common;
    using XamKit.Core.Common.Storage;

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
                    Type type = typeof(T);
                    if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        type = Nullable.GetUnderlyingType(type);
                    }

                    object setting = null;

                    var code = Type.GetTypeCode(type);

                    using (SharedPreferences)
                    {
                        switch (code)
                        {
                            case TypeCode.Boolean:
                                setting = SharedPreferences.GetBoolean(key, Helpers.SafeParseBool(value));
                                break;
                            case TypeCode.Int32:
                                break;
                            case TypeCode.Int64:
                                break;
                            case TypeCode.Single:
                                break;
                            case TypeCode.Double:
                                break;
                            case TypeCode.Decimal:
                                // Not supported
                                break;
                            case TypeCode.DateTime:
                                break;
                            case TypeCode.String:
                                break;
                            default:
                                if (value is Guid)
                                {

                                }
                                else
                                {
                                    throw new ArgumentException("The provided value is not a supported type.");
                                }
                                break;
                        }
                    }
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
                using (SharedPreferences)
                {
                    using (var editor = SharedPreferences.Edit())
                    {
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