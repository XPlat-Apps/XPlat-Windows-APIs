namespace XPlat.Device.Display
{
    using UIKit;

    public class DisplayRequest : IDisplayRequest
    {
        /// <inheritdoc />
        public void RequestActive()
        {
            UIApplication.SharedApplication.IdleTimerDisabled = true;
        }

        /// <inheritdoc />
        public void RequestRelease()
        {
            UIApplication.SharedApplication.IdleTimerDisabled = false;
        }
    }
}