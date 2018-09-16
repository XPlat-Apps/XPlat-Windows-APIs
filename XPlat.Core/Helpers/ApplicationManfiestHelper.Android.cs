#if __ANDROID__
namespace XPlat.Helpers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Android.App;
    using Android.Content.PM;

    public static class PackageManagerHelper
    {
        public static bool CheckContentProviderExists(string providerName)
        {
            if (string.IsNullOrWhiteSpace(providerName))
            {
                throw new ArgumentNullException(nameof(providerName));
            }

            PackageInfo myPackage = Application.Context.PackageManager.GetInstalledPackages(PackageInfoFlags.Providers)
                .FirstOrDefault(
                    x => x.PackageName.Equals(
                        Application.Context.PackageName,
                        StringComparison.CurrentCultureIgnoreCase));

            return myPackage != null
                   && myPackage.Providers.Any(x =>
                       CultureInfo.CurrentCulture.CompareInfo.IndexOf(x.Name, providerName,
                           CompareOptions.IgnoreCase) >= 0);
        }
    }
}
#endif