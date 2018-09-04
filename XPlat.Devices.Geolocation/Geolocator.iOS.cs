// <copyright file="Geolocator.iOS.cs" company="James Croft">
// Copyright (c) James Croft. All rights reserved.
// </copyright>

#if __IOS__
namespace XPlat.Devices.Geolocation
{
    using System;
    using System.Threading.Tasks;
    using CoreLocation;
    using global::Foundation;
    using UIKit;
    using XPlat.Exceptions;
    using XPlat.Foundation;

    /// <summary>
    /// Defines a service to access the current geographic location.
    /// </summary>
    public class Geolocator : IGeolocator
    {
        private readonly CLLocationManager locationManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Geolocator"/> class.
        /// </summary>
        public Geolocator()
        {
            this.locationManager = RetrieveLocationManager();
            this.locationManager.AuthorizationChanged += this.LocationManager_OnAuthorizationChanged;
            this.locationManager.Failed += this.LocationManager_OnFailed;

            if (UIDevice.CurrentDevice.CheckSystemVersion(6, 0))
            {
                this.locationManager.LocationsUpdated += this.LocationManager_OnLocationsUpdated;
            }
            else
            {
                this.locationManager.UpdatedLocation += this.LocationManager_OnUpdatedLocation;
            }
        }

        /// <inheritdoc />
        public event TypedEventHandler<IGeolocator, PositionChangedEventArgs> PositionChanged;

        /// <inheritdoc />
        public event TypedEventHandler<IGeolocator, StatusChangedEventArgs> StatusChanged;

        /// <inheritdoc />
        public Geoposition LastKnownPosition { get; private set; }

        /// <inheritdoc />
        public uint ReportInterval { get; set; }

        /// <inheritdoc />
        public double MovementThreshold { get; set; }

        /// <inheritdoc />
        public PositionAccuracy DesiredAccuracy { get; set; }

        /// <inheritdoc />
        public PositionStatus LocationStatus { get; private set; }

        /// <inheritdoc />
        public uint DesiredAccuracyInMeters { get; set; }

        /// <inheritdoc />
        public Task<GeolocationAccessStatus> RequestAccessAsync()
        {
            NSDictionary info = NSBundle.MainBundle.InfoDictionary;
            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                if (info.ContainsKey(new NSString("NSLocationWhenInUseUsageDescription")))
                {
                    this.locationManager.RequestWhenInUseAuthorization();
                }
                else if (info.ContainsKey(new NSString("NSLocationAlwaysUsageDescription")))
                {
                    this.locationManager.RequestAlwaysAuthorization();
                }
                else
                {
                    throw new AppPermissionInvalidException(
                        "NSLocationWhenInUseUsageDescription/NSLocationAlwaysUsageDescription",
                        "iOS8+ requires the NSLocationWhenInUseUsageDescription or NSLocationAlwaysUsageDescription to enable the Geolocator position changes.");
                }
            }

            this.locationManager.DesiredAccuracy = this.DesiredAccuracyInMeters;
            this.locationManager.DistanceFilter = this.MovementThreshold;
            this.locationManager.StartUpdatingLocation();

            return Task.FromResult(GeolocationAccessStatus.Allowed);
        }

        /// <inheritdoc />
        public Task<Geoposition> GetGeopositionAsync()
        {
            return this.GetGeopositionAsync(TimeSpan.MaxValue, TimeSpan.MaxValue);
        }

        /// <inheritdoc />
        public async Task<Geoposition> GetGeopositionAsync(TimeSpan maximumAge, TimeSpan timeout)
        {
            TaskCompletionSource<Geoposition> tcs = new TaskCompletionSource<Geoposition>();

            GeolocationAccessStatus access = await this.RequestAccessAsync();
            if (access == GeolocationAccessStatus.Allowed)
            {
                if (this.LastKnownPosition == null
                    || (this.LastKnownPosition.Coordinate != null
                        && !(this.LastKnownPosition.Coordinate.Timestamp <= DateTime.UtcNow.Subtract(maximumAge))))
                {
                    // Attempts to get the current location based on the event handler of this Geolocator.
                    TypedEventHandler<IGeolocator, PositionChangedEventArgs> positionResponse = null;
                    positionResponse = (s, e) =>
                    {
                        tcs.TrySetResult(e.Position);
                        this.PositionChanged -= positionResponse;
                    };

                    this.PositionChanged += positionResponse;
                }
                else
                {
                    tcs.SetResult(this.LastKnownPosition);
                }
            }
            else
            {
                tcs.SetResult(null);
            }

            return await tcs.Task;
        }

        private void LocationManager_OnUpdatedLocation(object sender, CLLocationUpdatedEventArgs args)
        {
            CLLocation loc = args.NewLocation;

            Geoposition geoposition = new Geoposition(loc);

            this.LastKnownPosition = geoposition;
            this.PositionChanged?.Invoke(this, new PositionChangedEventArgs(geoposition));

            loc?.Dispose();
        }

        private void LocationManager_OnLocationsUpdated(object sender, CLLocationsUpdatedEventArgs args)
        {
            foreach (CLLocation loc in args.Locations)
            {
                Geoposition geoposition = new Geoposition(loc);

                this.LastKnownPosition = geoposition;
                this.PositionChanged?.Invoke(this, new PositionChangedEventArgs(geoposition));

                loc?.Dispose();
            }
        }

        private void LocationManager_OnAuthorizationChanged(object sender, CLAuthorizationChangedEventArgs args)
        {
            switch (args.Status)
            {
                case CLAuthorizationStatus.NotDetermined:
                    this.LocationStatus = PositionStatus.NotAvailable;
                    break;
                case CLAuthorizationStatus.Restricted:
                case CLAuthorizationStatus.Denied:
                    this.LocationStatus = PositionStatus.Disabled;
                    break;
                case CLAuthorizationStatus.Authorized:
                case CLAuthorizationStatus.AuthorizedWhenInUse:
                    this.LocationStatus = PositionStatus.Ready;
                    break;
            }

            this.StatusChanged?.Invoke(this, new StatusChangedEventArgs(this.LocationStatus));
        }

        private void LocationManager_OnFailed(object sender, NSErrorEventArgs args)
        {
            if ((CLError)(int)args.Error.Code != CLError.Network) return;

            this.LocationStatus = PositionStatus.NotAvailable;
            this.StatusChanged?.Invoke(this, new StatusChangedEventArgs(this.LocationStatus));
        }

        private static CLLocationManager RetrieveLocationManager()
        {
            CLLocationManager manager = null;
            new NSObject().InvokeOnMainThread(() => manager = new CLLocationManager());
            return manager;
        }
    }
}
#endif