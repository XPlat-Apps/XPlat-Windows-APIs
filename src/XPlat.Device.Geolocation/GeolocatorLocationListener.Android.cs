// XPlat Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if __ANDROID__
namespace XPlat.Device.Geolocation
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using Android.Locations;
    using Android.OS;
    using XPlat.Foundation;

    /// <summary>
    /// Defines an <see cref="ILocationListener"/> instance for providing geolocation information.
    /// </summary>
    internal class GeolocatorLocationListener : Java.Lang.Object, ILocationListener
    {
        private readonly LocationManager locationManager;

        private readonly IEnumerable<string> allLocationProviders;

        private readonly HashSet<string> activeLocationProviders = new HashSet<string>();

        private string activeProvider;

        private TimeSpan reportInterval;

        private Location lastLocation;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeolocatorLocationListener"/> class.
        /// </summary>
        /// <param name="locationManager">The Android location manager.</param>
        /// <param name="reportInterval">The interval of location reporting.</param>
        /// <param name="allLocationProviders">The location providers.</param>
        public GeolocatorLocationListener(
            LocationManager locationManager,
            uint reportInterval,
            IEnumerable<string> allLocationProviders)
        {
            this.locationManager = locationManager;
            this.ReportInterval = reportInterval;
            this.allLocationProviders = allLocationProviders;

            this.UpdateActiveLocationManagerProviders();
        }

        /// <summary>Raised when the location is updated.</summary>
        public event TypedEventHandler<GeolocatorLocationListener, PositionChangedEventArgs> PositionChanged;

        /// <summary>Raised when the ability of the Geolocator to provide updated location changes.</summary>
        public event TypedEventHandler<GeolocatorLocationListener, StatusChangedEventArgs> StatusChanged;

        /// <summary>Gets or sets the requested minimum time interval between location updates, in milliseconds.</summary>
        public uint ReportInterval
        {
            get => uint.TryParse(this.reportInterval.TotalMilliseconds.ToString(CultureInfo.CurrentCulture), out uint interval) ? interval : 0;
            set => this.reportInterval = TimeSpan.FromMilliseconds(value);
        }

        /// <summary>Called when the location has changed.</summary>
        /// <param name="location">The new location, as a Location object.</param>
        public void OnLocationChanged(Location location)
        {
            if (location.Provider != this.activeProvider)
            {
                if (this.activeProvider != null && this.locationManager.IsProviderEnabled(this.activeProvider))
                {
                    LocationProvider locationProvider = this.locationManager.GetProvider(location.Provider);
                    TimeSpan timeSinceLastLocation = new TimeSpan(TimeSpan.TicksPerMillisecond * location.Time)
                                                     - new TimeSpan(
                                                         TimeSpan.TicksPerMillisecond * this.lastLocation.Time);

                    if (locationProvider.Accuracy > this.locationManager.GetProvider(this.activeProvider).Accuracy
                        && timeSinceLastLocation < this.reportInterval.Add(this.reportInterval))
                    {
                        location.Dispose();
                        return;
                    }
                }

                this.activeProvider = location.Provider;
            }

            Location previous = Interlocked.Exchange(ref this.lastLocation, location);
            previous?.Dispose();

            this.PositionChanged?.Invoke(this, new PositionChangedEventArgs(location));
        }

        /// <summary>Called when the provider is disabled by the user.</summary>
        /// <param name="provider">The name of the location provider associated with this update.</param>
        public void OnProviderDisabled(string provider)
        {
            if (provider == LocationManager.PassiveProvider)
            {
                return;
            }

            lock (this.activeLocationProviders)
            {
                this.activeLocationProviders.Remove(provider);
                this.UpdateReadyStatus();
            }
        }

        /// <summary>Called when the provider is enabled by the user.</summary>
        /// <param name="provider">The name of the location provider associated with this update.</param>
        public void OnProviderEnabled(string provider)
        {
            if (provider == LocationManager.PassiveProvider)
            {
                return;
            }

            lock (this.activeLocationProviders)
            {
                this.activeLocationProviders.Add(provider);
                this.UpdateReadyStatus();
            }
        }

        /// <summary>Called when the provider status changes.</summary>
        /// <param name="provider">The name of the location provider associated with this update.</param>
        /// <param name="status">The status changed to.</param>
        /// <param name="extras">An optional Bundle which will contain provider specific status variables.</param>
        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
            switch (status)
            {
                case Availability.Available:
                    this.OnProviderEnabled(provider);
                    break;
                case Availability.OutOfService:
                    this.OnProviderDisabled(provider);
                    break;
                case Availability.TemporarilyUnavailable:
                    this.StatusChanged?.Invoke(this, new StatusChangedEventArgs(PositionStatus.NotAvailable));
                    break;
            }
        }

        private void UpdateReadyStatus()
        {
            StatusChangedEventArgs status = this.activeLocationProviders.Count == 0
                ? new StatusChangedEventArgs(PositionStatus.NotAvailable)
                : new StatusChangedEventArgs(PositionStatus.Ready);

            this.StatusChanged?.Invoke(this, status);
        }

        private void UpdateActiveLocationManagerProviders()
        {
            foreach (string p in this.allLocationProviders)
            {
                if (this.locationManager.IsProviderEnabled(p))
                {
                    this.activeLocationProviders.Add(p);
                }
            }
        }
    }
}
#endif