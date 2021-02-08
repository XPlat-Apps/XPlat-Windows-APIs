// XPlat Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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

        /// <summary>
        /// Gets the originating <see cref="Windows.System.Display.DisplayRequest"/>.
        /// </summary>
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