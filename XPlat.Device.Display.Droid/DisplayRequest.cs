namespace XPlat.Device.Display
{
    using System;

    using Android.Views;

    using XPlat.Exceptions;

    public class DisplayRequest : IDisplayRequest
    {
        private readonly Window currentWindow;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisplayRequest"/> class.
        /// </summary>
        /// <param name="currentWindow">
        /// The current window.
        /// </param>
        public DisplayRequest(Window currentWindow)
        {
            this.currentWindow = currentWindow;
        }

        /// <inheritdoc />
        public void RequestActive()
        {
            try
            {
                this.currentWindow?.SetFlags(WindowManagerFlags.KeepScreenOn, WindowManagerFlags.KeepScreenOn);
            }
            catch (Exception ex)
            {
                throw new AppPermissionInvalidException("android.permission.WAKE_LOCK", ex.ToString(), ex);
            }
        }

        /// <inheritdoc />
        public void RequestRelease()
        {
            try
            {
                this.currentWindow?.ClearFlags(WindowManagerFlags.KeepScreenOn);
            }
            catch (Exception ex)
            {
                throw new AppPermissionInvalidException("android.permission.WAKE_LOCK", ex.ToString(), ex);
            }
        }
    }
}