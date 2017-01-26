namespace XamarinApiToolkit.Storage
{
    using System;

    public sealed class AppStorageItemNotFoundException : AppStorageItemException
    {
        public AppStorageItemNotFoundException(string storageItemName, string message)
            : base(storageItemName, message)
        {
        }

        public AppStorageItemNotFoundException(string storageItemName, string message, Exception innerException)
            : base(storageItemName, message, innerException)
        {
        }
    }
}