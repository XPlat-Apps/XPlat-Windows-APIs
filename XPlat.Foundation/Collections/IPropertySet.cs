namespace XPlat.Foundation.Collections
{
    /// <summary>
    /// Represents a collection of key-value pairs, correlating several other collection interfaces.
    /// </summary>
    public interface IPropertySet : IObservableMap<string, object>
    {
        /// <summary>
        /// Gets a value from the set for the specified key as the specified type.
        /// </summary>
        /// <param name="key">
        /// The key to retrieve.
        /// </param>
        /// <typeparam name="T">
        /// The type of object expected.
        /// </typeparam>
        /// <returns>
        /// When complete, the method will return the value for the specified key as the specified type.
        /// </returns>
        T Get<T>(string key)
            where T : class;
    }
}