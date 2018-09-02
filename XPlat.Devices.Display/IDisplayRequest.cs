// <copyright file="IDisplayRequest.cs" company="James Croft">
// Copyright (c) James Croft. All rights reserved.
// </copyright>

namespace XPlat.Devices.Display
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