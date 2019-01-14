#if WINDOWS_UWP
namespace XPlat.ApplicationModel
{
    using System;

    public class PackageId : IPackageId
    {
        public PackageId(Windows.ApplicationModel.PackageId packageId)
        {
            this.Originator = packageId ?? throw new ArgumentNullException(nameof(packageId));
        }

        public string Name => this.Originator.Name;

        public PackageVersion Version => this.Originator.Version;

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