#if WINDOWS_UWP
namespace XPlat.Device.Profile
{
    /// <summary>Provides information about the device for profiling purposes.</summary>
    public static class AnalyticsInfo
    {
        /// <summary>Gets version info about the device family.</summary>
        /// <returns>Version info about the device family.</returns>
        public static AnalyticsVersionInfo VersionInfo =>
            new AnalyticsVersionInfo(Windows.System.Profile.AnalyticsInfo.VersionInfo);

        /// <summary>Gets the device form factor. For example, the app could be running on a phone, tablet, desktop, and so on.</summary>
        /// <returns>The device form factor.</returns>
        public static string DeviceForm => Windows.System.Profile.AnalyticsInfo.DeviceForm;
    }
}
#endif