namespace XPlat.API.Storage
{
    using System;

    public sealed class StorageItemCreationException : StorageItemException
    {
        public StorageItemCreationException(string storageItemName, string message)
            : base(storageItemName, message)
        {
        }

        public StorageItemCreationException(string storageItemName, string message, Exception innerException)
            : base(storageItemName, message, innerException)
        {
        }
    }
}