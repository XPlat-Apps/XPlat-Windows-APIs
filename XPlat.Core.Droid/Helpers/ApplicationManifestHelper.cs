namespace XPlat.Droid.Helpers
{
    using System;
    using System.Globalization;

    using Android.App;
    using Android.Content.PM;

    using System.Linq;

    public static class ApplicationManifestHelper
    {
        public static bool CheckContentProviderExists(string providerName)
        {
            if (string.IsNullOrWhiteSpace(providerName))
            {
                throw new ArgumentNullException(nameof(providerName));
            }

            var myPackage = Application.Context.PackageManager.GetInstalledPackages(PackageInfoFlags.Providers)
                .FirstOrDefault(
                    x => x.PackageName.Equals(
                        Application.Context.PackageName,
                        StringComparison.CurrentCultureIgnoreCase));

            return myPackage != null
                   && myPackage.Providers.Any(x => x.Name.Contains(providerName, CompareOptions.IgnoreCase));
        }
    }
}