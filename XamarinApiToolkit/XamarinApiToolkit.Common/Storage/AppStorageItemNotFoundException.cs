namespace XamarinApiToolkit.Storage
{
    using System;

    public sealed class AppStorageItemNotFoundException : Exception
    {
        public AppStorageItemNotFoundException(string storageItemName, string message)
            : base(message)
        {
            this.StorageItemName = storageItemName;
        }

        public AppStorageItemNotFoundException(string storageItemName, string message, Exception innerException)
            : base(message, innerException)
        {
            this.StorageItemName = storageItemName;
        }

        public string StorageItemName { get; }
    }
}