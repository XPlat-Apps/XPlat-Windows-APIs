﻿#if __IOS__
namespace XPlat.ApplicationModel
{
    using System;

    using global::Foundation;

    /// <summary>Provides package identification info, such as name, version, and publisher.</summary>
    public class PackageId : IPackageId
    {
        private readonly WeakReference originatorReference;

        public PackageId(NSBundle bundle)
        {
            if (bundle == null)
            {
                throw new ArgumentNullException(nameof(bundle));
            }

            this.originatorReference = new WeakReference(bundle);
        }

        /// <summary>Gets the name of the package.</summary>
        public string Name => this.Originator?.ObjectForInfoDictionary("CFBundleDisplayName")?.ToString();

        /// <summary>Gets the package version info.</summary>
        public PackageVersion Version => this.Originator;

        /// <summary>Gets the full name of the package.</summary>
        public string FullName => this.Originator?.ObjectForInfoDictionary("CFBundleName")?.ToString();

        /// <summary>Gets the original iOS NSBundle reference object.</summary>
        public NSBundle Originator =>
            this.originatorReference != null && this.originatorReference.IsAlive
                ? this.originatorReference.Target as NSBundle
                : null;
    }
}
#endif