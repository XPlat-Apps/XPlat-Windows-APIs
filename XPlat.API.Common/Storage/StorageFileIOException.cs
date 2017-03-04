namespace XPlat.API.Storage
{
    using System;

    public sealed class StorageFileIOException : StorageItemException
    {
        public StorageFileIOException(string storageItemName, string message)
            : base(storageItemName, message)
        {
        }

        public StorageFileIOException(string storageItemName, string message, Exception innerException)
            : base(storageItemName, message, innerException)
        {
        }
    }
}