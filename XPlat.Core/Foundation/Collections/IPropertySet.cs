namespace XPlat.Foundation.Collections
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a collection of key-value pairs, correlating several other collection interfaces.
    /// </summary>
    public interface IPropertySet : IObservableMap<string, object>, IDictionary<string, object>, IEnumerable<KeyValuePair<string, object>>
    {
        T Get<T>(string key) where T : class;
    }
}
