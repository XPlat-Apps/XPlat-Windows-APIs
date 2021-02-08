// XPlat Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XPlat.ApplicationModel
{
    using System;
    using System.Globalization;

    /// <summary>Represents the package version info.</summary>
    public struct PackageVersion
    {
        /// <summary>The major version number of the package.</summary>
        public ushort Major;

        /// <summary>The minor version number of the package.</summary>
        public ushort Minor;

        /// <summary>The build version number of the package.</summary>
        public ushort Build;

        /// <summary>The revision version number of the package.</summary>
        public ushort Revision;

        /// <summary>
        /// Allows conversion of a <see cref="string"/> to the <see cref="PackageVersion"/> without direct casting.
        /// </summary>
        /// <param name="versionString">
        /// The <see cref="string"/> version number.
        /// </param>
        /// <returns>
        /// The <see cref="PackageVersion"/>.
        /// </returns>
        public static implicit operator PackageVersion(string versionString)
        {
            return Parse(versionString);
        }

        /// <summary>
        /// Allows conversion of a <see cref="Version"/> to the <see cref="PackageVersion"/> without direct casting.
        /// </summary>
        /// <param name="version">
        /// The <see cref="Version"/>.
        /// </param>
        /// <returns>
        /// The <see cref="PackageVersion"/>.
        /// </returns>
        public static implicit operator PackageVersion(Version version)
        {
            return new PackageVersion
                       {
                           Major = (ushort)version.Major,
                           Minor = (ushort)version.Minor,
                           Build = (ushort)version.Build,
                           Revision = (ushort)version.Revision,
                       };
        }

#if WINDOWS_UWP
        /// <summary>
        /// Allows conversion of a <see cref="Windows.ApplicationModel.PackageVersion"/> to the <see cref="PackageVersion"/> without direct casting.
        /// </summary>
        /// <param name="version">
        /// The <see cref="Windows.ApplicationModel.PackageVersion"/>.
        /// </param>
        /// <returns>
        /// The <see cref="PackageVersion"/>.
        /// </returns>
        public static implicit operator PackageVersion(Windows.ApplicationModel.PackageVersion version)
        {
            return new PackageVersion
                       {
                           Major = version.Major,
                           Minor = version.Minor,
                           Build = version.Build,
                           Revision = version.Revision,
                       };
        }

        /// <summary>
        /// Allows conversion of a <see cref="PackageVersion"/> to the <see cref="Windows.ApplicationModel.PackageVersion"/> without direct casting.
        /// </summary>
        /// <param name="version">
        /// The <see cref="PackageVersion"/>.
        /// </param>
        /// <returns>
        /// The <see cref="Windows.ApplicationModel.PackageVersion"/>.
        /// </returns>
        public static implicit operator Windows.ApplicationModel.PackageVersion(PackageVersion version)
        {
            return new Windows.ApplicationModel.PackageVersion
                       {
                           Major = version.Major,
                           Minor = version.Minor,
                           Build = version.Build,
                           Revision = version.Revision,
                       };
        }

#elif __IOS__
        /// <summary>
        /// Allows conversion of a NSBundle to the <see cref="PackageVersion"/> without direct casting.
        /// </summary>
        /// <param name="bundle">
        /// The NSBundle.
        /// </param>
        /// <returns>
        /// The <see cref="PackageVersion"/>.
        /// </returns>
        public static implicit operator PackageVersion(global::Foundation.NSBundle bundle)
        {
            string versionNumber = bundle?.ObjectForInfoDictionary("CFBundleShortVersionString")?.ToString() ?? "0.0";
            string buildNumber = bundle?.ObjectForInfoDictionary("CFBundleVersion")?.ToString() ?? "0.0";

            string buildNumberStripped = System.Text.RegularExpressions.Regex.Replace(buildNumber, "[^0-9.+-]", string.Empty);

            versionNumber = $"{versionNumber}.{buildNumberStripped}";

            return Version.Parse(versionNumber);
        }
#endif

        /// <summary>
        /// Parses a string version number to a <see cref="PackageVersion"/>.
        /// </summary>
        /// <param name="input">The <see cref="string"/> version number.</param>
        /// <returns>
        /// The <see cref="PackageVersion"/>.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">Thrown if <paramref name="input"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.ArgumentException">Thrown if <paramref name="input"/> is not a valid version number.</exception>
        public static PackageVersion Parse(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            string[] split = input.Split('.');

            int length = split.Length;
            if (length < 2)
            {
                throw new ArgumentException(
                    "An error occurred while attempting to parse version string. The value must contain a minimum of major and minor.",
                    nameof(split));
            }

            int major;
            int minor;
            int build = 0;
            int revision = 0;

            switch (length)
            {
                case 2:
                    major = ParseComponent(split[0]);
                    minor = ParseComponent(split[1]);
                    break;
                case 3:
                    major = ParseComponent(split[0]);
                    minor = ParseComponent(split[1]);
                    build = ParseComponent(split[2]);
                    break;
                default:
                    major = ParseComponent(split[0]);
                    minor = ParseComponent(split[1]);
                    build = ParseComponent(split[2]);
                    revision = ParseComponent(split[3]);
                    break;
            }

            return new Version(major, minor, build, revision);
        }

        /// <summary>Returns the fully qualified type name of this instance.</summary>
        /// <returns>A <see cref="T:System.String" /> containing a fully qualified type name.</returns>
        /// <filterpriority>2.</filterpriority>
        public override string ToString()
        {
            return $"{this.Major}.{this.Minor}.{this.Build}.{this.Revision}";
        }

        private static int ParseComponent(string component)
        {
            return int.Parse(component, NumberStyles.Integer, (IFormatProvider)CultureInfo.InvariantCulture);
        }
    }
}