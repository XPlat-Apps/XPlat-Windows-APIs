#if __ANDROID__
namespace XPlat.Devices.Display
{
    using System;
    using Android.Views;
    using XPlat.Exceptions;

    /// <summary>Represents a display request.</summary>
    public class DisplayRequest : IDisplayRequest
    {
        /// <summary>Initializes a new instance of the <see cref="DisplayRequest"/> class.</summary>
        public DisplayRequest(Window currentWindow)
        {
            this.Originator = currentWindow;
        }

        public Window Originator { get; }

        /// <summary>Activates a display request.</summary>
        public void RequestActive()
        {
            try
            {
                this.Originator?.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);
            }
            catch (Exception ex)
            {
                throw new AppPermissionInvalidException("android.permission.WAKE_LOCK", ex.ToString(), ex);
            }
        }

        /// <summary>Deactivates a display request.</summary>
        public void RequestRelease()
        {
            try
            {
                this.Originator?.ClearFlags(WindowManagerFlags.KeepScreenOn);
            }
            catch (Exception ex)
            {
                throw new AppPermissionInvalidException("android.permission.WAKE_LOCK", ex.ToString(), ex);
            }
        }
    }
}
#endif