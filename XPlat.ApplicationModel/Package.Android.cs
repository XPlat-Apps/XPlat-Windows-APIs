#if __ANDROID__
namespace XPlat.ApplicationModel
{
    using System;
    using System.Collections.Generic;

    using Android.Content.PM;
    using Android.Content.Res;

    using XPlat.Storage;

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

        public static Package Current => current ?? (current = new Package());

        public IPackageId Id => this.id ?? (this.id = new PackageId(this.Originator));

        public IStorageFolder InstalledLocation => new StorageFolder(Android.App.Application.Context.PackageCodePath);

        public IReadOnlyList<IPackage> Dependencies => new List<IPackage>();

        public string DisplayName =>
            Android.App.Application.Context.PackageManager.GetApplicationLabel(this.Originator.ApplicationInfo);

        public Uri Logo => this.logo ?? (this.logo = this.GetApplicationLogo());

        private Uri GetApplicationLogo()
        {
            Resources resources = Android.App.Application.Context.Resources;
            Android.Net.Uri uri = Android.Net.Uri.Parse(
                $"{Android.Content.ContentResolver.SchemeAndroidResource}://{resources.GetResourcePackageName(this.Originator.ApplicationInfo.Icon)}/{resources.GetResourceTypeName(this.Originator.ApplicationInfo.Icon)}/{resources.GetResourceEntryName(this.Originator.ApplicationInfo.Icon)}");

            return new Uri(uri.Path);
        }

        public bool IsDevelopmentMode =>
            Android.App.Application.Context.PackageManager.GetInstallerPackageName(this.Originator.PackageName) == null;

        public DateTimeOffset InstalledDate => DateTimeOffset.FromUnixTimeMilliseconds(this.Originator.FirstInstallTime);

        public PackageInfo Originator { get; }
    }
}
#endif