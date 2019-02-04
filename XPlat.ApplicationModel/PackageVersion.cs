namespace XPlat.ApplicationModel
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

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

        public static implicit operator PackageVersion(string versionString)
        {
            return Parse(versionString);
        }

        public static implicit operator PackageVersion(Version version)
        {
            return new PackageVersion
                       {
                           Major = (ushort)version.Major,
                           Minor = (ushort)version.Minor,
                           Build = (ushort)version.Build,
                           Revision = (ushort)version.Revision
                       };
        }

#if WINDOWS_UWP
        public static implicit operator PackageVersion(Windows.ApplicationModel.PackageVersion version)
        {
            return new PackageVersion
                       {
                           Major = version.Major,
                           Minor = version.Minor,
                           Build = version.Build,
                           Revision = version.Revision
                       };
        }

        public static implicit operator Windows.ApplicationModel.PackageVersion(PackageVersion version)
        {
            return new Windows.ApplicationModel.PackageVersion
                       {
                           Major = version.Major,
                           Minor = version.Minor,
                           Build = version.Build,
                           Revision = version.Revision
                       };
        }

#elif __IOS__
        public static implicit operator PackageVersion(global::Foundation.NSBundle bundle)
        {
            string versionNumber = bundle?.ObjectForInfoDictionary("CFBundleShortVersionString")?.ToString() ?? "0.0";
            string buildNumber = bundle?.ObjectForInfoDictionary("CFBundleVersion")?.ToString() ?? "0.0";

            string buildNumberStripped = Regex.Replace(buildNumber, "[^0-9.+-]", string.Empty);

            versionNumber = $"{versionNumber}.{buildNumberStripped}";

            return Version.Parse(versionNumber);
        }
#endif

        public static PackageVersion Parse(string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var split = input.Split('.');

            int length = split.Length;
            if (length < 2)
            {
                throw new ArgumentException(
                    "An error occured while attempting to parse version string. The value must contain a minimum of major and minor.",
                    nameof(split));
            }

            int major;
            int minor = 0;
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

        private static int ParseComponent(string component)
        {
            return int.Parse(component, NumberStyles.Integer, (IFormatProvider)CultureInfo.InvariantCulture);
        }

        /// <summary>Returns the fully qualified type name of this instance.</summary>
        /// <returns>A <see cref="T:System.String" /> containing a fully qualified type name.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"{this.Major}.{this.Minor}.{this.Build}.{this.Revision}";
        }
    }
}