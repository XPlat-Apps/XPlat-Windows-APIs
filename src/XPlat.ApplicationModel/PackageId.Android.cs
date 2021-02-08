// XPlat Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if __ANDROID__
namespace XPlat.ApplicationModel
{
    using System;

    /// <summary>Provides package identification info, such as name, version, and publisher.</summary>
    public class PackageId : IPackageId
    {
        private readonly WeakReference originatorReference;

        /// <summary>
        /// Initializes a new instance of the <see cref="PackageId"/> class with the Android package information.
        /// </summary>
        /// <param name="packageInfo">The Android package information.</param>
        /// <exception cref="T:System.ArgumentNullException">Thrown if <paramref name="packageInfo"/> is <see langword="null"/>.</exception>
        public PackageId(Android.Content.PM.PackageInfo packageInfo)
        {
            if (packageInfo == null)
            {
                throw new ArgumentNullException(nameof(packageInfo));
            }

            this.originatorReference = new WeakReference(packageInfo);
        }

        /// <summary>Gets the name of the package.</summary>
        public string Name => this.Originator.PackageName;

        /// <summary>Gets the package version info.</summary>
        public PackageVersion Version => this.Originator.VersionName;

        /// <summary>Gets the full name of the package.</summary>
        public string FullName => Android.App.Application.Context.PackageName;

        /// <summary>Gets the original Android PackageInfo reference object.</summary>
        public Android.Content.PM.PackageInfo Originator => this.originatorReference != null && this.originatorReference.IsAlive
                                                                ? this.originatorReference.Target as Android.Content.PM.PackageInfo
                                                                : null;

        /// <summary>
        /// Allows conversion of a <see cref="Android.Content.PM.PackageInfo"/> to the <see cref="PackageId"/> without direct casting.
        /// </summary>
        /// <param name="packageId">
        /// The <see cref="Android.Content.PM.PackageInfo"/>.
        /// </param>
        /// <returns>
        /// The <see cref="PackageId"/>.
        /// </returns>
        public static implicit operator PackageId(Android.Content.PM.PackageInfo packageId)
        {
            return new PackageId(packageId);
        }

        /// <summary>
        /// Allows conversion of a <see cref="PackageId"/> to the <see cref="Android.Content.PM.PackageInfo"/> without direct casting.
        /// </summary>
        /// <param name="packageId">
        /// The <see cref="PackageId"/>.
        /// </param>
        /// <returns>
        /// The <see cref="Android.Content.PM.PackageInfo"/>.
        /// </returns>
        public static implicit operator Android.Content.PM.PackageInfo(PackageId packageId)
        {
            return packageId.Originator;
        }
    }
}
#endif