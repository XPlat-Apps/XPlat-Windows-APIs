namespace XPlat.API.Storage
{
    using System;

    public sealed class AppStorageItemCreationException : AppStorageItemException
    {
        public AppStorageItemCreationException(string storageItemName, string message)
            : base(storageItemName, message)
        {
        }

        public AppStorageItemCreationException(string storageItemName, string message, Exception innerException)
            : base(storageItemName, message, innerException)
        {
        }
    }
}