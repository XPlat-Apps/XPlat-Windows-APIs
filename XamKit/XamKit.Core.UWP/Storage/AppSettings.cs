namespace XamKit.Core.Storage
{
    using Windows.Storage;

    using XamKit.Core.Common.Storage;

    /// <summary>
    /// Defines the app settings model for Windows UWP applications.
    /// </summary>
    public class AppSettings : IAppSettings
    {
        private readonly object obj = new object();

        private static ApplicationDataContainer LocalSettings
        {
            get
            {
                return ApplicationData.Current.LocalSettings;
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
                containsKey = LocalSettings.Values.ContainsKey(key);
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
                    var setting = LocalSettings.Values[key];
                    if (setting is T)
                    {
                        value = (T)setting;
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
                if (this.ContainsKey(key))
                {
                    LocalSettings.Values[key] = value;
                }
                else
                {
                    LocalSettings.CreateContainer(key, ApplicationDataCreateDisposition.Always);
                    LocalSettings.Values[key] = value;
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
                    LocalSettings.Values.Remove(key);
                }
            }
        }
    }
}