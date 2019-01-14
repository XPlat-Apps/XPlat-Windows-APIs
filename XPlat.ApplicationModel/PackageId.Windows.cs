#if WINDOWS_UWP
namespace XPlat.ApplicationModel
{
    using System;

    /// <summary>Provides package identification info, such as name, version, and publisher.</summary>
    public class PackageId : IPackageId
    {
        public PackageId(Windows.ApplicationModel.PackageId packageId)
        {
            this.Originator = packageId ?? throw new ArgumentNullException(nameof(packageId));
        }

        /// <summary>Gets the name of the package.</summary>
        public string Name => this.Originator.Name;

        /// <summary>Gets the package version info.</summary>
        public PackageVersion Version => this.Originator.Version;

        /// <summary>Gets the full name of the package.</summary>
        public string FullName => this.Originator.FullName;

        /// <summary>Gets the originating Windows PackageId instance.</summary>
        public Windows.ApplicationModel.PackageId Originator { get; }

        public static implicit operator PackageId(Windows.ApplicationModel.PackageId packageId)
        {
            return new PackageId(packageId);
        }

        public static implicit operator Windows.ApplicationModel.PackageId(PackageId packageId)
        {
            return packageId.Originator;
        }
    }
}
#endif