namespace XamarinApiToolkit.Storage
{
    using System;

    public sealed class AppStorageItemCreationException : Exception
    {
        public AppStorageItemCreationException(string storageItemName, string message)
            : base(message)
        {
            this.StorageItemName = storageItemName;
        }

        public AppStorageItemCreationException(string storageItemName, string message, Exception innerException)
            : base(message, innerException)
        {
            this.StorageItemName = storageItemName;
        }

        public string StorageItemName { get; }
    }
}