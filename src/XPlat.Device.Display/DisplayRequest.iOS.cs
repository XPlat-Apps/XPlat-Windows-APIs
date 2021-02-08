// XPlat Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if __IOS__
namespace XPlat.Device.Display
{
    using UIKit;

    /// <summary>Represents a display request.</summary>
    public class DisplayRequest : IDisplayRequest
    {
        /// <summary>Activates a display request.</summary>
        public void RequestActive()
        {
            UIApplication.SharedApplication.IdleTimerDisabled = true;
        }

        /// <summary>Deactivates a display request.</summary>
        public void RequestRelease()
        {
            UIApplication.SharedApplication.IdleTimerDisabled = false;
        }
    }
}
#endif