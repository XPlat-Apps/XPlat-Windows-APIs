namespace XamKit.Core.Storage
{
    using System;

    using XamKit.Core.Common.Storage;

    public class AppSettings : IAppSettings
    {
        public bool ContainsKey(string key)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key, T value = default(T))
        {
            throw new NotImplementedException();
        }

        public void AddOrUpdate<T>(string key, T value)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }
    }
}