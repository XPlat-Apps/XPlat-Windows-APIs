namespace XamKit.Core.Storage
{
    using XamKit.Core.Common.Storage;

    public class AppSettings : IAppSettings
    {
        public bool ContainsKey(string key)
        {
            throw new System.NotImplementedException();
        }

        public T Get<T>(string key, T value = default(T))
        {
            throw new System.NotImplementedException();
        }

        public void AddOrUpdate<T>(string key, T value)
        {
            throw new System.NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new System.NotImplementedException();
        }
    }
}