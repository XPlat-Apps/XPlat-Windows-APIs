namespace XPlat.ApplicationModel
{
    using System;

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
            return Version.Parse(versionString);
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
#endif
    }
}