// XPlat Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace XPlat.Device.Display
{
    /// <summary>Represents a display request.</summary>
    public interface IDisplayRequest
    {
        /// <summary>Activates a display request.</summary>
        void RequestActive();

        /// <summary>Deactivates a display request.</summary>
        void RequestRelease();
    }
}