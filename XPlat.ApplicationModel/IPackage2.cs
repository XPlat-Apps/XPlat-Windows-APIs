namespace XPlat.ApplicationModel
{
    using System;

    public interface IPackage2
    {
        string DisplayName { get; }

        Uri Logo { get; }

        bool IsDevelopmentMode { get; }
    }
}