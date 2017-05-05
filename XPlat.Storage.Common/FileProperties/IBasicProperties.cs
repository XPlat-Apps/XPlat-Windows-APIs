namespace XPlat.Storage.FileProperties
{
    using System;

    public interface IBasicProperties
    {
        DateTime DateModified { get; }

        ulong Size { get; }
    }
}