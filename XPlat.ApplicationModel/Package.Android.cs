#if __ANDROID__
namespace XPlat.ApplicationModel
{
    using System;
    using System.Collections.Generic;

    using Android.Content.PM;
    using Android.Content.Res;

    using XPlat.Storage;

    /// <summary>Provides information about a package.</summary>
    public class Package : IPackage, IPackage2, IPackage3
    {
        private static Package current;

        private IPackageId id;

        private Uri logo;

        public Package()
        {
            this.Originator = Android.App.Application.Context.PackageManager.GetPackageInfo(
                Android.App.Application.Context.PackageName,
                Android.Content.PM.PackageInfoFlags.MetaData);
        }

        /// <summary>Gets the package for the current app.</summary>
        public static Package Current => current ?? (current = new Package());

        /// <summary>Gets the package identity of the current package.</summary>
        public IPackageId Id => this.id ?? (this.id = new PackageId(this.Originator));

        /// <summary>Gets the location of the installed package.</summary>
        public IStorageFolder InstalledLocation => new StorageFolder(Android.App.Application.Context.PackageCodePath);

        /// <summary>Gets the packages on which the current package depends.</summary>
        public IReadOnlyList<IPackage> Dependencies => new List<IPackage>();

        /// <summary>Gets the display name of the package.</summary>
        public string DisplayName =>
            Android.App.Application.Context.PackageManager.GetApplicationLabel(this.Originator.ApplicationInfo);

        /// <summary>Gets the logo of the package.</summary>
        public Uri Logo => this.logo ?? (this.logo = this.GetApplicationLogo());

        /// <summary>Indicates whether the package is installed in development mode.</summary>
        public bool IsDevelopmentMode =>
            Android.App.Application.Context.PackageManager.GetInstallerPackageName(this.Originator.PackageName) == null;

        /// <summary>Gets the date on which the application package was installed or last updated.</summary>
        public DateTimeOffset InstalledDate => DateTimeOffset.FromUnixTimeMilliseconds(this.Originator.FirstInstallTime);

        /// <summary>Gets the original Android PackageInfo reference object.</summary>
        public PackageInfo Originator { get; }

        private Uri GetApplicationLogo()
        {
            Resources resources = Android.App.Application.Context.Resources;
            Android.Net.Uri uri = Android.Net.Uri.Parse(
                $"{Android.Content.ContentResolver.SchemeAndroidResource}://{resources.GetResourcePackageName(this.Originator.ApplicationInfo.Icon)}/{resources.GetResourceTypeName(this.Originator.ApplicationInfo.Icon)}/{resources.GetResourceEntryName(this.Originator.ApplicationInfo.Icon)}");

            return new Uri(uri.Path);
        }
    }
}
#endif