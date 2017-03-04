namespace XPlat.API.Device.Display
{
#if ANDROID
    using System;

    using Android.Views;

    using XPlat.API.Exceptions;
#elif IOS
    using UIKit;
#endif

    public class DisplayRequest : IDisplayRequest
    {
#if WINDOWS_UWP
        private readonly Windows.System.Display.DisplayRequest request;
#elif ANDROID
        private Window currentWindow;
#endif


#if WINDOWS_UWP
        public DisplayRequest()
        {
            this.request = new Windows.System.Display.DisplayRequest();
#elif ANDROID
        public DisplayRequest(Window currentWindow)
        {
            this.currentWindow = currentWindow;
#else
        public DisplayRequest()
        {
#endif
        }

        public void RequestActive()
        {
#if WINDOWS_UWP
            this.request?.RequestActive();
#elif ANDROID
            try
            {
                this.currentWindow?.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);
            }
            catch (Exception ex)
            {
                throw new AppPermissionInvalidException("android.permission.WAKE_LOCK", ex.ToString(), ex);
            }
#elif IOS
            UIApplication.SharedApplication.IdleTimerDisabled = true;
#endif
        }

        public void RequestRelease()
        {
#if WINDOWS_UWP
            this.request?.RequestRelease();
#elif ANDROID
            try
            {
                this.currentWindow?.ClearFlags(WindowManagerFlags.KeepScreenOn);
            }
            catch (Exception ex)
            {
                throw new AppPermissionInvalidException("android.permission.WAKE_LOCK", ex.ToString(), ex);
            }
#elif IOS
            UIApplication.SharedApplication.IdleTimerDisabled = false;
#endif
        }
    }
}