#if WINDOWS_UWP
namespace XPlat.ApplicationModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using XPlat.Storage;

    public class Package : IPackage, IPackage2, IPackage3
    {
        private static Package current;

        private List<IPackage> dependencies;

        private IPackageId id;

        public Package(Windows.ApplicationModel.Package package)
        {
            this.Originator = package ?? throw new ArgumentNullException(nameof(package));
        }

        public static Package Current => current ?? (current = Windows.ApplicationModel.Package.Current);

        public IPackageId Id => id ?? (id = new PackageId(this.Originator.Id));

        public IStorageFolder InstalledLocation => new StorageFolder(this.Originator.InstalledLocation);

        public IReadOnlyList<IPackage> Dependencies =>
            this.dependencies ?? (this.dependencies = this.Originator.Dependencies.Select(item => new Package(item))
                                      .Cast<IPackage>().ToList());

        public string DisplayName => this.Originator.DisplayName;

        public Uri Logo => this.Originator.Logo;

        public bool IsDevelopmentMode => this.Originator.IsDevelopmentMode;

        public DateTimeOffset InstalledDate => this.Originator.InstalledDate;

        public Windows.ApplicationModel.Package Originator { get; }

        public static implicit operator Package(Windows.ApplicationModel.Package package)
        {
            return new Package(package);
        }

        public static implicit operator Windows.ApplicationModel.Package(Package package)
        {
            return package.Originator;
        }
    }
}
#endif