namespace XPlat.API.Storage
{
    using System;

    public class StorageItemException : Exception
    {
        public StorageItemException(string storageItemName, string message)
            : base(message)
        {
            this.StorageItemName = storageItemName;
        }

        public StorageItemException(string storageItemName, string message, Exception innerException)
            : base(message, innerException)
        {
            this.StorageItemName = storageItemName;
        }

        public string StorageItemName { get; }
    }
}