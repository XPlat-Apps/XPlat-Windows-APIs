namespace XPlat.API.Storage
{
    using System;

    public sealed class StorageItemNotFoundException : StorageItemException
    {
        public StorageItemNotFoundException(string storageItemName, string message)
            : base(storageItemName, message)
        {
        }

        public StorageItemNotFoundException(string storageItemName, string message, Exception innerException)
            : base(storageItemName, message, innerException)
        {
        }
    }
}