namespace XPlat.API.Storage
{
    using System;

    public class AppStorageItemException : Exception
    {
        public AppStorageItemException(string storageItemName, string message)
            : base(message)
        {
            this.StorageItemName = storageItemName;
        }

        public AppStorageItemException(string storageItemName, string message, Exception innerException)
            : base(message, innerException)
        {
            this.StorageItemName = storageItemName;
        }

        public string StorageItemName { get; }
    }
}