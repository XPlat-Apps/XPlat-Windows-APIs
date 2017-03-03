namespace XPlat.API.Storage
{
    /// <summary>
    /// Defines an interface for application settings.
    /// </summary>
    public interface IAppSettingsContainer
    {
        /// <summary>
        /// Checks whether the settings container contains the specified key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// Returns true if exists; else false.
        /// </returns>
        bool ContainsKey(string key);

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
        T Get<T>(string key);

        /// <summary>
        /// Adds or updates a key in the settings container with the specified value.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        void AddOrUpdate(string key, object value);

        /// <summary>
        /// Removes the specified key's value from the settings container.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        void Remove(string key);
    }
}