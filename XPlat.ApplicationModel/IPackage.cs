namespace XPlat.ApplicationModel
{
    using System.Collections.Generic;

    using XPlat.Storage;

    /// <summary>Provides information about a package.</summary>
    public interface IPackage
    {
        /// <summary>Gets the package identity of the current package.</summary>
        IPackageId Id { get; }

        /// <summary>Gets the location of the installed package.</summary>
        IStorageFolder InstalledLocation { get; }

        /// <summary>Gets the packages on which the current package depends.</summary>
        IReadOnlyList<IPackage> Dependencies { get; }
    }
}