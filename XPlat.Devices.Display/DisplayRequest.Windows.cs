#if WINDOWS_UWP
namespace XPlat.Device.Display
{
    /// <summary>Represents a display request.</summary>
    public class DisplayRequest : IDisplayRequest
    {
        /// <summary>Initializes a new instance of the <see cref="DisplayRequest"/> class.</summary>
        public DisplayRequest()
        {
            this.Originator = new Windows.System.Display.DisplayRequest();
        }

        public Windows.System.Display.DisplayRequest Originator { get; }

        /// <summary>Activates a display request.</summary>
        public void RequestActive()
        {
            this.Originator?.RequestActive();
        }

        /// <summary>Deactivates a display request.</summary>
        public void RequestRelease()
        {
            this.Originator?.RequestRelease();
        }
    }
}
#endif