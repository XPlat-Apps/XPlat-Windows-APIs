// <copyright file="DisplayRequest.Windows.cs" company="James Croft">
// Copyright (c) James Croft. All rights reserved.
// </copyright>

#if WINDOWS_UWP
namespace XPlat.Devices.Display
{
    /// <summary>Represents a display request.</summary>
    public class DisplayRequest : IDisplayRequest
    {
        private readonly Windows.System.Display.DisplayRequest request;

        /// <summary>Initializes a new instance of the <see cref="DisplayRequest"/> class.</summary>
        public DisplayRequest()
        {
            this.request = new Windows.System.Display.DisplayRequest();
        }

        /// <summary>Activates a display request.</summary>
        public void RequestActive()
        {
            this.request?.RequestActive();
        }

        /// <summary>Deactivates a display request.</summary>
        public void RequestRelease()
        {
            this.request?.RequestRelease();
        }
    }
}
#endif