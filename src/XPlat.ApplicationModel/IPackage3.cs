namespace XPlat.ApplicationModel
{
    using System;

    /// <summary>Provides information about a package.</summary>
    public interface IPackage3
    {
        /// <summary>Gets the date on which the application package was installed or last updated.</summary>
        DateTimeOffset InstalledDate { get; }
    }
}
