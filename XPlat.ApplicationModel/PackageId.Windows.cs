#if WINDOWS_UWP
namespace XPlat.ApplicationModel
{
    using System;

    /// <summary>Provides package identification info, such as name, version, and publisher.</summary>
    public class PackageId : IPackageId
    {
        private readonly WeakReference originatorReference;

        public PackageId(Windows.ApplicationModel.PackageId packageId)
        {
            if (packageId == null)
            {
                throw new ArgumentNullException(nameof(packageId));
            }

            this.originatorReference = new WeakReference(packageId);
        }

        /// <summary>Gets the name of the package.</summary>
        public string Name => this.Originator.Name;

        /// <summary>Gets the package version info.</summary>
        public PackageVersion Version => this.Originator.Version;

        /// <summary>Gets the full name of the package.</summary>
        public string FullName => this.Originator.FullName;

        /// <summary>Gets the originating Windows PackageId instance.</summary>
        public Windows.ApplicationModel.PackageId Originator => this.originatorReference != null && this.originatorReference.IsAlive
                                                                    ? this.originatorReference.Target as Windows.ApplicationModel.PackageId
                                                                    : null;

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