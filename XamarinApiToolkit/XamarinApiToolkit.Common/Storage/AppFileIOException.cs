namespace XamarinApiToolkit.Storage
{
    using System;

    public sealed class AppFileIOException : AppStorageItemException
    {
        public AppFileIOException(string storageItemName, string message)
            : base(storageItemName, message)
        {
        }

        public AppFileIOException(string storageItemName, string message, Exception innerException)
            : base(storageItemName, message, innerException)
        {
        }
    }
}