﻿#if __ANDROID__
namespace XPlat.ApplicationModel
{
    using System;

    /// <summary>Provides package identification info, such as name, version, and publisher.</summary>
    public class PackageId : IPackageId
    {
        private readonly WeakReference originatorReference;

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

        public static implicit operator PackageId(Android.Content.PM.PackageInfo packageId)
        {
            return new PackageId(packageId);
        }

        public static implicit operator Android.Content.PM.PackageInfo(PackageId packageId)
        {
            return packageId.Originator;
        }
    }
}
#endif