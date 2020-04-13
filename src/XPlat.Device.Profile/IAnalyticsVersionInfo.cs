namespace XPlat.Device.Profile
{
    /// <summary>Provides version information about the device family.</summary>
    public interface IAnalyticsVersionInfo
    {
        /// <summary>Gets a string that represents the type of device the application is running on.</summary>
        string DeviceFamily { get; }

        /// <summary>Gets the version within the device family.</summary>
        string DeviceFamilyVersion { get; }
    }
}