namespace XPlat.ApplicationModel
{
    using System.Collections.Generic;

    using XPlat.Storage;

    public interface IPackage
    {
        IPackageId Id { get; }

        IStorageFolder InstalledLocation { get; }

        IReadOnlyList<IPackage> Dependencies { get; }
    }
}