﻿namespace XPlat.Device.Display
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