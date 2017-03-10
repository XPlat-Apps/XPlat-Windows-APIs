namespace XPlat.API.Device.Geolocation
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using Android.Locations;
    using Android.OS;

    using XPlat.API.Foundation;
    using XPlat.API.Helpers;

    internal class GeolocatorLocationListener : Java.Lang.Object, ILocationListener
    {
        private readonly LocationManager locationManager;

        private readonly IEnumerable<string> allLocationProviders;

        private readonly HashSet<string> activeLocationProviders = new HashSet<string>();

        private string activeProvider;

        private TimeSpan reportInterval;

        private Location lastLocation;

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

        /// <summary>
        /// Gets or sets the requested minimum time interval between location updates, in milliseconds.
        /// </summary>
        public uint ReportInterval
        {
            get
            {
                return ParseHelper.SafeParseUInt(this.reportInterval.TotalMilliseconds);
            }
            set
            {
                this.reportInterval = TimeSpan.FromMilliseconds(value);
            }
        }

        /// <summary>
        /// Raised when the location is updated.
        /// </summary>
        public event TypedEventHandler<GeolocatorLocationListener, PositionChangedEventArgs> PositionChanged;

        /// <summary>
        /// Raised when the ability of the Geolocator to provide updated location changes.
        /// </summary>
        public event TypedEventHandler<GeolocatorLocationListener, StatusChangedEventArgs> StatusChanged;
        
        /// <inheritdoc />
        public void OnLocationChanged(Location location)
        {
            if (location.Provider != this.activeProvider)
            {
                if (this.activeProvider != null && this.locationManager.IsProviderEnabled(this.activeProvider))
                {
                    var locationProvider = this.locationManager.GetProvider(location.Provider);
                    var timeSinceLastLocation = location.Time.TimeSpanFromMilliseconds()
                                                - this.lastLocation.Time.TimeSpanFromMilliseconds();

                    if (locationProvider.Accuracy > this.locationManager.GetProvider(this.activeProvider).Accuracy
                        && timeSinceLastLocation < this.reportInterval.Add(this.reportInterval))
                    {
                        location.Dispose();
                        return;
                    }
                }

                this.activeProvider = location.Provider;
            }

            var previous = Interlocked.Exchange(ref this.lastLocation, location);
            previous?.Dispose();

            this.PositionChanged?.Invoke(this, new PositionChangedEventArgs(location.ToLocalGeoposition()));
        }

        /// <inheritdoc />
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

        private void UpdateReadyStatus()
        {
            this.StatusChanged?.Invoke(
                this,
                this.activeLocationProviders.Count == 0
                    ? new StatusChangedEventArgs(PositionStatus.NotAvailable)
                    : new StatusChangedEventArgs(PositionStatus.Ready));
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        private void UpdateActiveLocationManagerProviders()
        {
            foreach (var p in this.allLocationProviders)
            {
                if (this.locationManager.IsProviderEnabled(p))
                {
                    this.activeLocationProviders.Add(p);
                }
            }
        }
    }
}