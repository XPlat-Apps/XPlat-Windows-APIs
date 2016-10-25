namespace XamarinApiToolkit.Storage
{
    using System;

    public sealed class AppSettingsContainer : IAppSettingsContainer
    {
        public bool ContainsKey(string key)
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }

        public void AddOrUpdate(string key, object value)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }
    }
}
