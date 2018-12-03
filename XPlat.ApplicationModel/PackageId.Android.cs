#if __ANDROID__
namespace XPlat.ApplicationModel
{
    using System;

    public class PackageId : IPackageId
    {
        public PackageId(Android.Content.PM.PackageInfo packageInfo)
        {
            this.Originator = packageInfo ?? throw new ArgumentNullException(nameof(packageInfo));
        }

        public string Name => this.Originator.PackageName;

        public PackageVersion Version => this.Originator.VersionName;

        public string FullName => Android.App.Application.Context.PackageName;

        public Android.Content.PM.PackageInfo Originator { get; }

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