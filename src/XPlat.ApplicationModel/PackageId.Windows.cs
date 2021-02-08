// XPlat Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if WINDOWS_UWP
namespace XPlat.ApplicationModel
{
    using System;

    /// <summary>Provides package identification info, such as name, version, and publisher.</summary>
    public class PackageId : IPackageId
    {
        private readonly WeakReference originatorReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageId"/> class with the Windows application package identification info.
        /// </summary>
        /// <param name="packageId">The Windows application package identification info.</param>
        /// <exception cref="T:System.ArgumentNullException">Thrown if <paramref name="packageId"/> is <see langword="null"/>.</exception>
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

        /// <summary>
        /// Allows conversion of a <see cref="Windows.ApplicationModel.PackageId"/> to the <see cref="PackageId"/> without direct casting.
        /// </summary>
        /// <param name="packageId">
        /// The <see cref="Windows.ApplicationModel.PackageId"/>.
        /// </param>
        /// <returns>
        /// The <see cref="PackageId"/>.
        /// </returns>
        public static implicit operator PackageId(Windows.ApplicationModel.PackageId packageId)
        {
            return new PackageId(packageId);
        }

        /// <summary>
        /// Allows conversion of a <see cref="PackageId"/> to the <see cref="Windows.ApplicationModel.PackageId"/> without direct casting.
        /// </summary>
        /// <param name="packageId">
        /// The <see cref="PackageId"/>.
        /// </param>
        /// <returns>
        /// The <see cref="Windows.ApplicationModel.PackageId"/>.
        /// </returns>
        public static implicit operator Windows.ApplicationModel.PackageId(PackageId packageId)
        {
            return packageId.Originator;
        }
    }
}
#endif