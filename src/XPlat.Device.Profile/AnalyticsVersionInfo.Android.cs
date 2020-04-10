#if __ANDROID__
namespace XPlat.Device.Profile
{
    using System;

    using Android.App;
    using Android.Content;
    using Android.Content.Res;
    using Android.OS;

    /// <summary>Provides version information about the device family.</summary>
    public class AnalyticsVersionInfo : IAnalyticsVersionInfo
    {
        internal const string DeviceFamilyCar = "Android.Auto";

        internal const string DeviceFamilyDesktop = "Android.Desktop";

        internal const string DeviceFamilyMobile = "Android.Mobile";

        internal const string DeviceFamilyTablet = "Android.Tablet";

        internal const string DeviceFamilyTelevision = "Android.TV";

        internal const string DeviceFamilyUnknown = "Android.Unknown";

        internal const string DeviceFamilyVirtualReality = "Android.VR";

        internal const string DeviceFamilyWatch = "Android.Wear";

        /// <summary>Gets a string that represents the type of device the application is running on.</summary>
        public string DeviceFamily => DetermineDeviceFamily();

        /// <summary>Gets the version within the device family.</summary>
        public string DeviceFamilyVersion => Build.VERSION.Release;

        internal static string DetermineDeviceFamily()
        {
            try
            {
                // Returns the TV device family.
                if (Build.VERSION.SdkInt >= BuildVersionCodes.HoneycombMr2
                    && Application.Context.GetSystemService(Context.UiModeService) is UiModeManager uiModeManager
                    && uiModeManager.CurrentModeType == UiMode.TypeTelevision)
                {
                    return DeviceFamilyTelevision;
                }
            }
            catch (Exception ex)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }

            try
            {
                // Returns the Auto device family.
                if (Application.Context.GetSystemService(Context.UiModeService) is UiModeManager uiModeManager
                    && uiModeManager.CurrentModeType == UiMode.TypeCar)
                {
                    return DeviceFamilyCar;
                }
            }
            catch (Exception ex)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }

            try
            {
                // Returns the Wear device family.
                if (Build.VERSION.SdkInt >= BuildVersionCodes.KitkatWatch
                    && Application.Context.GetSystemService(Context.UiModeService) is UiModeManager uiModeManager
                    && uiModeManager.CurrentModeType == UiMode.TypeWatch)
                {
                    return DeviceFamilyWatch;
                }
            }
            catch (Exception ex)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }

            try
            {
                // Returns the VR device family.
                if (Application.Context.GetSystemService(Context.UiModeService) is UiModeManager uiModeManager
                    && uiModeManager.CurrentModeType == UiMode.TypeVrHeadset)
                {
                    return DeviceFamilyVirtualReality;
                }
            }
            catch (Exception ex)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }

            try
            {
                // Returns the VR device family.
                if (Application.Context.GetSystemService(Context.UiModeService) is UiModeManager uiModeManager
                    && uiModeManager.CurrentModeType == UiMode.TypeDesk)
                {
                    return DeviceFamilyDesktop;
                }
            }
            catch (Exception ex)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }

            try
            {
                // Returns the mobile or tablet device family.
                return Application.Context.Resources.GetString(XPlat.Device.Profile.Resource.String.device_family);
            }
            catch (Exception ex)
            {
                if (System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }
            }

            // If nothing is returned, we cannot determine the family.
            return DeviceFamilyUnknown;
        }
    }
}
#endif