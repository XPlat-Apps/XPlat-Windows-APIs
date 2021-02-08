// XPlat Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if __ANDROID__
namespace XPlat.Helpers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Android.App;
    using Android.Content.PM;

    /// <summary>
    /// Defines a helper for the Android <see cref="PackageManager"/>.
    /// </summary>
    public static class PackageManagerHelper
    {
        /// <summary>
        /// Determines whether a content provider exists with the specified <paramref name="providerName"/>.
        /// </summary>
        /// <param name="providerName">The content provider name.</param>
        /// <returns>True if the content provider exists; otherwise, false.</returns>
        /// <exception cref="T:System.ArgumentNullException">Thrown if <paramref name="providerName"/> is <see langword="null"/>.</exception>
        public static bool CheckContentProviderExists(string providerName)
        {
            if (string.IsNullOrWhiteSpace(providerName))
            {
                throw new ArgumentNullException(nameof(providerName));
            }

            PackageInfo myPackage = Application.Context.PackageManager.GetInstalledPackages(PackageInfoFlags.Providers)
                .FirstOrDefault(
                    packageInfo => packageInfo.PackageName.Equals(
                        Application.Context.PackageName,
                        StringComparison.CurrentCultureIgnoreCase));

            return myPackage != null
                   && myPackage.Providers.Any(providerInfo =>
                       CultureInfo.CurrentCulture.CompareInfo.IndexOf(providerInfo.Name, providerName,
                           CompareOptions.IgnoreCase) >= 0);
        }
    }
}
#endif