// <copyright file="IStatusChangedEventArgs.cs" company="James Croft">
// Copyright (c) James Croft. All rights reserved.
// </copyright>

namespace XPlat.Devices.Geolocation
{
    public interface IStatusChangedEventArgs
    {
        /// <summary>Gets the updated status of the Geolocator object.</summary>
        PositionStatus Status { get; }
    }
}