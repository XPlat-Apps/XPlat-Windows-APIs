// XPlat Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if __ANDROID__
namespace XPlat.Device.Display
{
    using System;
    using Android.Views;
    using XPlat.Exceptions;

    /// <summary>Represents a display request.</summary>
    public class DisplayRequest : IDisplayRequest
    {
        /// <summary>Initializes a new instance of the <see cref="DisplayRequest"/> class.</summary>
        /// <param name="currentWindow">
        /// The current Android window.
        /// </param>
        public DisplayRequest(Window currentWindow)
        {
            this.Originator = currentWindow;
        }

        /// <summary>
        /// Gets the Android windows associated with this request.
        /// </summary>
        public Window Originator { get; }

        /// <summary>Activates a display request.</summary>
        /// <exception cref="T:XPlat.Exceptions.AppPermissionInvalidException">Thrown if android.permission.WAKE_LOCK is not permitted.</exception>
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
        /// <exception cref="T:XPlat.Exceptions.AppPermissionInvalidException">Thrown if android.permission.WAKE_LOCK is not permitted.</exception>
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