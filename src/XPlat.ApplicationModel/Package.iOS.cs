// XPlat Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if __IOS__
namespace XPlat.ApplicationModel
{
    using System;
    using System.Collections.Generic;

    using global::Foundation;

    using XPlat.Storage;

    /// <summary>Provides information about a package.</summary>
    public class Package : IPackage, IPackage2, IPackage3
    {
        private static Package current;

        private readonly WeakReference originatorReference;

        private IStorageFolder installedLocation;

        private IPackageId id;

        /// <summary>
        /// Initializes a new instance of the <see cref="Package"/> class with the application bundle.
        /// </summary>
        /// <param name="bundle">The application bundle.</param>
        /// <exception cref="T:System.ArgumentNullException">Thrown if <paramref name="bundle"/> is <see langword="null"/>.</exception>
        public Package(NSBundle bundle)
        {
            if (bundle == null)
            {
                throw new ArgumentNullException(nameof(bundle));
            }

            this.originatorReference = new WeakReference(bundle);
        }

        /// <summary>Gets the package for the current app.</summary>
        public static Package Current => current != null && current.originatorReference.IsAlive ? current : (current = NSBundle.MainBundle);

        /// <summary>Gets the package identity of the current package.</summary>
        public IPackageId Id => this.id ??= new PackageId(this.Originator);

        /// <summary>Gets the location of the installed package.</summary>
        public IStorageFolder InstalledLocation => this.installedLocation ??= new StorageFolder(this.Originator.BundlePath);

        /// <summary>Gets the packages on which the current package depends.</summary>
        public IReadOnlyList<IPackage> Dependencies => new List<IPackage>();

        /// <summary>Gets the display name of the package.</summary>
        public string DisplayName => this.Originator?.ObjectForInfoDictionary("CFBundleDisplayName")?.ToString();

        /// <summary>Gets the logo of the package.</summary>
        public Uri Logo => null;

        /// <summary>Indicates whether the package is installed in development mode.</summary>
        public bool IsDevelopmentMode => this.DetermineDevelopmentMode();

        /// <summary>Gets the date on which the application package was installed or last updated.</summary>
        public DateTimeOffset InstalledDate => ApplicationData.Current.LocalFolder.DateCreated;

        /// <summary>Gets the original iOS NSBundle reference object.</summary>
        public NSBundle Originator =>
            this.originatorReference != null && this.originatorReference.IsAlive
                ? this.originatorReference.Target as NSBundle
                : null;

        /// <summary>
        /// Allows conversion of a <see cref="NSBundle"/> to the <see cref="Package"/> without direct casting.
        /// </summary>
        /// <param name="package">
        /// The <see cref="NSBundle"/>.
        /// </param>
        /// <returns>
        /// The <see cref="Package"/>.
        /// </returns>
        public static implicit operator Package(NSBundle package)
        {
            return new Package(package);
        }

        /// <summary>
        /// Allows conversion of a <see cref="Package"/> to the <see cref="NSBundle"/> without direct casting.
        /// </summary>
        /// <param name="package">
        /// The <see cref="Package"/>.
        /// </param>
        /// <returns>
        /// The <see cref="NSBundle"/>.
        /// </returns>
        public static implicit operator NSBundle(Package package)
        {
            return package.Originator;
        }

        private bool DetermineDevelopmentMode()
        {
            NSUrl appStoreReceipt = this.Originator?.AppStoreReceiptUrl;
            if (appStoreReceipt == null)
            {
                // Couldn't retrieve an app store receipt so assume this is development.
                return true;
            }

            string appStoreReceiptLastComponent = appStoreReceipt.LastPathComponent;
            bool isSandboxReceipt = appStoreReceiptLastComponent.Equals("sandboxReceipt", StringComparison.CurrentCultureIgnoreCase);

            return isSandboxReceipt;
        }
    }
}
#endif