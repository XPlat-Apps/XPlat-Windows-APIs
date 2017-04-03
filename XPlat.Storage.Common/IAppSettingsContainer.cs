namespace XPlat.Storage
{
    /// <summary>
    /// Defines an interface for application settings.
    /// </summary>
    public interface IAppSettingsContainer
    {
        /// <summary>
        /// Checks whether the current container contains the specified key.
        /// </summary>
        /// <param name="key">
        /// The key of the setting to check.
        /// </param>
        /// <returns>
        /// When this method completes successfully, it returns true if the key exists; else false.
        /// </returns>
        bool ContainsKey(string key);

        /// <summary>
        /// Gets a value for the specified key from the current container.
        /// </summary>
        /// <param name="key">
        /// The key to retrieve.
        /// </param>
        /// <typeparam name="T">
        /// The type for the stored value.
        /// </typeparam>
        /// <returns>
        /// When this method completes successfully, it returns an object of type T.
        /// </returns>
        T Get<T>(string key);

        /// <summary>
        /// Adds or updates the specified key with the specified value in the current container.
        /// </summary>
        /// <param name="key">
        /// The key to add or update.
        /// </param>
        /// <param name="value">
        /// The value to update.
        /// </param>
        void AddOrUpdate(string key, object value);

        /// <summary>
        /// Removes the specified key from the current container.
        /// </summary>
        /// <param name="key">
        /// The key to remove.
        /// </param>
        void Remove(string key);
    }
}