namespace XamKit.Core.Common.Storage
{
    /// <summary>
    /// Defines the interface for application settings.
    /// </summary>
    public interface IAppSettings
    {
        /// <summary>
        /// Determines whether the application settings contains an element with the specified key.
        /// </summary>
        /// <param name="key">
        /// The key to locate in the application settings.
        /// </param>
        /// <returns>
        /// Returns a <see cref="bool"/> value indicating whether the key exists.
        /// </returns>
        bool ContainsKey(string key);

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
        T Get<T>(string key, T value = default(T));

        /// <summary>
        /// Adds or updates a value in the application settings with the specified key and value.
        /// </summary>
        /// <param name="key">
        /// The key to add or update.
        /// </param>
        /// <param name="value">
        /// The value to add or update the key with.
        /// </param>
        void AddOrUpdate(string key, object value);

        /// <summary>
        /// Removes a key and value from the application settings with the specified key.
        /// </summary>
        /// <param name="key">
        /// The key to remove.
        /// </param>
        void Remove(string key);
    }
}