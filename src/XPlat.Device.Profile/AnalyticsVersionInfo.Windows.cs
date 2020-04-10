#if WINDOWS_UWP
namespace XPlat.Device.Profile
{
    using System;

    /// <summary>Provides version information about the device family.</summary>
    public class AnalyticsVersionInfo : IAnalyticsVersionInfo
    {
        public AnalyticsVersionInfo(Windows.System.Profile.AnalyticsVersionInfo versionInfo)
        {
            if (versionInfo == null)
            {
                throw new ArgumentNullException(nameof(versionInfo));
            }

            this.DeviceFamily = versionInfo.DeviceFamily;
            this.DeviceFamilyVersion = versionInfo.DeviceFamilyVersion;
        }

        /// <summary>Gets a string that represents the type of device the application is running on.</summary>
        public string DeviceFamily { get; }

        /// <summary>Gets the version within the device family.</summary>
        public string DeviceFamilyVersion { get; }
    }
}
#endif