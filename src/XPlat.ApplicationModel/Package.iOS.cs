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
        public IPackageId Id => this.id ?? (this.id = new PackageId(this.Originator));

        /// <summary>Gets the location of the installed package.</summary>
        public IStorageFolder InstalledLocation => this.installedLocation ?? (this.installedLocation = new StorageFolder(this.Originator.BundlePath));

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

        public static implicit operator Package(NSBundle package)
        {
            return new Package(package);
        }

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