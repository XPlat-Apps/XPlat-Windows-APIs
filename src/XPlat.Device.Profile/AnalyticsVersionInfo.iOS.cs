#if __IOS__
namespace XPlat.Device.Profile
{
    using UIKit;

    /// <summary>Provides version information about the device family.</summary>
    public class AnalyticsVersionInfo : IAnalyticsVersionInfo
    {
        internal const string DeviceFamilyCar = "Apple.CarPlay";

        internal const string DeviceFamilyDesktop = "Apple.MacOS";

        internal const string DeviceFamilyMobile = "Apple.iPhone";

        internal const string DeviceFamilyTablet = "Apple.iPad";

        internal const string DeviceFamilyTelevision = "Apple.TV";

        internal const string DeviceFamilyUnknown = "Apple.Unknown";

        internal const string DeviceFamilyWatch = "Apple.Watch";

        public string DeviceFamily => DetermineDeviceFamily();

        public string DeviceFamilyVersion => UIKit.UIDevice.CurrentDevice.SystemVersion;

        internal static string DetermineDeviceFamily()
        {
            switch (UIKit.UIDevice.CurrentDevice.UserInterfaceIdiom)
            {
                case UIUserInterfaceIdiom.Phone:
                    return DeviceFamilyMobile;
                case UIUserInterfaceIdiom.Pad:
                    return DeviceFamilyTablet;
                case UIUserInterfaceIdiom.TV:
                    return DeviceFamilyTelevision;
                case UIUserInterfaceIdiom.CarPlay:
                    return DeviceFamilyCar;
                default:
                    return DeviceFamilyUnknown;
            }
        }
    }
}
#endif