namespace XPlat.ApplicationModel
{
    /// <summary>Provides package identification info, such as name, version, and publisher.</summary>
    public interface IPackageId
    {
        /// <summary>Gets the name of the package.</summary>
        string Name { get; }

        /// <summary>Gets the package version info.</summary>
        PackageVersion Version { get; }

        /// <summary>Gets the full name of the package.</summary>
        string FullName { get; }
    }
}