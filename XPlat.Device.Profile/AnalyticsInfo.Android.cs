#if __ANDROID__
namespace XPlat.Device.Profile
{
    /// <summary>Provides information about the device for profiling purposes.</summary>
    public static class AnalyticsInfo
    {
        /// <summary>Gets version info about the device family.</summary>
        /// <returns>Version info about the device family.</returns>
        public static AnalyticsVersionInfo VersionInfo => new AnalyticsVersionInfo();

        /// <summary>Gets the device form factor. For example, the app could be running on a phone, tablet, desktop, and so on.</summary>
        /// <returns>The device form factor.</returns>
        public static string DeviceForm => DetermineDeviceForm();

        private static string DetermineDeviceForm()
        {
            string deviceFamily = AnalyticsVersionInfo.DetermineDeviceFamily();

            if (deviceFamily.Equals(AnalyticsVersionInfo.DeviceFamilyDesktop))
            {
                return "Desktop";
            }

            if (deviceFamily.Equals(AnalyticsVersionInfo.DeviceFamilyCar))
            {
                return "Car";
            }

            if (deviceFamily.Equals(AnalyticsVersionInfo.DeviceFamilyMobile)
                || deviceFamily.Equals(AnalyticsVersionInfo.DeviceFamilyTablet))
            {
                return "Mobile";
            }

            if (deviceFamily.Equals(AnalyticsVersionInfo.DeviceFamilyTelevision))
            {
                return "Television";
            }

            if (deviceFamily.Equals(AnalyticsVersionInfo.DeviceFamilyWatch) || deviceFamily.Equals(AnalyticsVersionInfo.DeviceFamilyVirtualReality))
            {
                return "Wearable";
            }

            return "Unknown";
        }
    }
}
#endif