namespace XPlat.API.Device.Geolocation
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Android.Content;
    using Android.Locations;
    using Android.OS;

    using XPlat.API.Foundation;

    /// <summary>
    /// Defines a service to access the current geographic location.
    /// </summary>
    public class Geolocator : IGeolocator
    {
        private readonly object obj = new object();

        private readonly LocationManager locationManager;

        private readonly string[] locationProviders;

        private GeolocatorLocationListener locationListener;

        private uint reportInterval;

        private PositionAccuracy desiredAccuracy;

        /// <summary>
        /// Initializes a new instance of the <see cref="Geolocator"/> class.
        /// </summary>
        /// <param name="context">
        /// The Android context.
        /// </param>
        public Geolocator(Context context)
        {
            this.locationManager = (LocationManager)context.GetSystemService(Context.LocationService);
            this.locationProviders =
                this.locationManager.GetProviders(false).Where(p => p != LocationManager.PassiveProvider).ToArray();
        }

        /// <inheritdoc />
        public event TypedEventHandler<IGeolocator, PositionChangedEventArgs> PositionChanged;

        /// <inheritdoc />
        public event TypedEventHandler<IGeolocator, StatusChangedEventArgs> StatusChanged;

        /// <inheritdoc />
        public Geoposition LastKnownPosition { get; private set; }

        /// <inheritdoc />
        public uint ReportInterval
        {
            get
            {
                return this.reportInterval;
            }
            set
            {
                this.reportInterval = value;

                if (this.locationListener != null)
                {
                    this.locationListener.ReportInterval = value;
                }
            }
        }

        /// <inheritdoc />
        public double MovementThreshold { get; set; }

        /// <inheritdoc />
        public PositionStatus LocationStatus { get; private set; }

        /// <inheritdoc />
        public PositionAccuracy DesiredAccuracy
        {
            get
            {
                return this.desiredAccuracy;
            }
            set
            {
                this.desiredAccuracy = value;

                switch (value)
                {
                    case PositionAccuracy.Default:
                        if (this.DesiredAccuracyInMeters != 500)
                        {
                            this.DesiredAccuracyInMeters = 500;
                        }
                        break;
                    case PositionAccuracy.High:
                        if (this.DesiredAccuracyInMeters != 10)
                        {
                            this.DesiredAccuracyInMeters = 10;
                        }
                        break;
                }
            }
        }

        /// <inheritdoc />
        public uint DesiredAccuracyInMeters { get; set; }

        /// <inheritdoc />
        public Task<Geoposition> GetGeopositionAsync()
        {
            return this.GetGeopositionAsync(new TimeSpan(), TimeSpan.MaxValue);
        }

        /// <inheritdoc />
        public async Task<Geoposition> GetGeopositionAsync(TimeSpan maximumAge, TimeSpan timeout)
        {
            var tcs = new TaskCompletionSource<Geoposition>();

            var access = await this.RequestAccessAsync();
            if (access == GeolocationAccessStatus.Allowed)
            {
                LocationRetriever[] retriever = { null };
                retriever[0] = new LocationRetriever(
                    this.DesiredAccuracyInMeters,
                    timeout,
                    this.locationProviders.Where(this.locationManager.IsProviderEnabled),
                    () =>
                        {
                            try
                            {
                                this.locationManager.RemoveUpdates(retriever[0]);
                            }
                            catch (Exception ex)
                            {
#if DEBUG
                                System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif
                            }
                        });

                try
                {
                    var looperThread = Looper.MyLooper() ?? Looper.MainLooper;

                    var numEnabledProviders = 0;
                    foreach (var provider in this.locationProviders)
                    {
                        if (this.locationManager.IsProviderEnabled(provider))
                        {
                            numEnabledProviders++;
                        }

                        this.locationManager.RequestLocationUpdates(provider, 0, 0, retriever[0], looperThread);
                    }

                    if (numEnabledProviders == 0)
                    {
                        try
                        {
                            this.locationManager.RemoveUpdates(retriever[0]);
                        }
                        catch (Exception ex)
                        {
#if DEBUG
                            System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif
                        }

                        tcs.TrySetException(
                            new GeolocatorException("A location cannot be retrieved as the provider is unavailable."));
                        return await tcs.Task;
                    }
                }
                catch (Java.Lang.SecurityException ex)
                {
                    tcs.TrySetException(
                        new GeolocatorException("A location cannot be retrieved as the access was unauthorized.", ex));
                    return await tcs.Task;
                }

                return await retriever[0].Task;
            }

            lock (this.obj)
            {
                if (this.LastKnownPosition == null
                    || (this.LastKnownPosition.Coordinate != null
                        && !this.LastKnownPosition.Coordinate.Timestamp.IsValid(maximumAge)))
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

            return await tcs.Task;
        }

        /// <inheritdoc />
        public Task<GeolocationAccessStatus> RequestAccessAsync()
        {
            var status = this.locationProviders.Any(this.locationManager.IsProviderEnabled)
                             ? GeolocationAccessStatus.Allowed
                             : GeolocationAccessStatus.Denied;

            if (this.locationListener == null && status == GeolocationAccessStatus.Allowed)
            {
                this.locationListener = new GeolocatorLocationListener(
                    this.locationManager,
                    this.ReportInterval,
                    this.locationProviders);

                this.locationListener.PositionChanged += this.LocationListener_PositionChanged;
                this.locationListener.StatusChanged += this.LocationListener_StatusChanged;

                var looperThread = Looper.MyLooper() ?? Looper.MainLooper;
                foreach (var provider in this.locationProviders)
                {
                    this.locationManager.RequestLocationUpdates(
                        provider,
                        this.reportInterval,
                        this.DesiredAccuracyInMeters,
                        this.locationListener,
                        looperThread);
                }
            }

            return Task.FromResult(status);
        }

        private void LocationListener_PositionChanged(GeolocatorLocationListener sender, PositionChangedEventArgs args)
        {
            lock (this.obj)
            {
                this.LastKnownPosition = args.Position;
                this.PositionChanged?.Invoke(this, args);
            }
        }

        private void LocationListener_StatusChanged(GeolocatorLocationListener sender, StatusChangedEventArgs args)
        {
            this.LocationStatus = args.Status;
            this.StatusChanged?.Invoke(this, args);
        }
    }
}