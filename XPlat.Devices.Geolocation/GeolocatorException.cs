// <copyright file="GeolocatorException.cs" company="James Croft">
// Copyright (c) James Croft. All rights reserved.
// </copyright>

namespace XPlat.Devices.Geolocation
{
    using System;

    public class GeolocatorException : Exception
    {
        public GeolocatorException()
            : this(string.Empty)
        {
        }

        public GeolocatorException(string message)
            : this(message, null)
        {
        }

        public GeolocatorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}