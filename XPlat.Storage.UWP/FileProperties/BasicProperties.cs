namespace XPlat.Storage.FileProperties
{
    using System;

    public class BasicProperties : IBasicProperties
    {
        public BasicProperties(DateTime dateModified, ulong size)
        {
            this.DateModified = dateModified;
            this.Size = size;
        }

        public DateTime DateModified { get; }

        public ulong Size { get; }
    }
}