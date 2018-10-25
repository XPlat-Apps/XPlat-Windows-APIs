#if __ANDROID__
namespace XPlat.Device.Geolocation
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Android.Content;
    using Android.Locations;
    using Android.OS;

    using Java.Lang;

    using XPlat.Foundation;

    using Exception = System.Exception;

    /// <summary>Provides access to the current geographic location.</summary>
    public class Geolocator : IGeolocator
    {
        private readonly object obj = new object();

        private readonly LocationManager locationManager;

        private readonly string[] locationProviders;

        private GeolocatorLocationListener locationListener;

        private uint reportInterval = 1;

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

        /// <summary>Raised when the location is updated.</summary>
        public event TypedEventHandler<IGeolocator, PositionChangedEventArgs> PositionChanged;

        /// <summary>Raised when the ability of the Geolocator to provide updated location changes.</summary>
        public event TypedEventHandler<IGeolocator, StatusChangedEventArgs> StatusChanged;

        /// <summary>Gets the time used for converting the Android <see cref="Location"/> objects Time property to a DateTime.</summary>
        public static DateTime AndroidLocationTime => new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>Gets the last known position recorded by the Geolocator.</summary>
        public Geoposition LastKnownPosition { get; private set; }

        /// <summary>Gets or sets the requested minimum time interval between location updates, in milliseconds. If your application requires updates infrequently, set this value so that location services can conserve power by calculating location only when needed.</summary>
        public uint ReportInterval
        {
            get => this.reportInterval;
            set
            {
                this.reportInterval = value;

                if (this.locationListener != null)
                {
                    this.locationListener.ReportInterval = value;
                }
            }
        }

        /// <summary>Gets or sets the distance of movement, in meters, relative to the coordinate from the last PositionChanged event, that is required for the Geolocator to raise a PositionChanged event.</summary>
        public double MovementThreshold { get; set; }

        /// <summary>Gets the status that indicates the ability of the Geolocator to provide location updates.</summary>
        public PositionStatus LocationStatus { get; private set; }

        /// <summary>Gets or sets the accuracy level at which the Geolocator provides location updates.</summary>
        public PositionAccuracy DesiredAccuracy
        {
            get => this.desiredAccuracy;
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

        /// <summary>Gets or sets the desired accuracy in meters for data returned from the location service.</summary>
        public uint DesiredAccuracyInMeters { get; set; }

        /// <summary>Starts an asynchronous operation to retrieve the current location of the device.</summary>
        /// <returns>An asynchronous operation that, upon completion, returns a Geoposition marking the found location.</returns>
        public Task<Geoposition> GetGeopositionAsync()
        {
            return this.GetGeopositionAsync(TimeSpan.MaxValue, TimeSpan.FromMinutes(1));
        }

        /// <summary>Starts an asynchronous operation to retrieve the current location of the device.</summary>
        /// <returns>An asynchronous operation that, upon completion, returns a Geoposition marking the found location.</returns>
        public async Task<Geoposition> GetGeopositionAsync(TimeSpan maximumAge, TimeSpan timeout)
        {
            TaskCompletionSource<Geoposition> tcs = new TaskCompletionSource<Geoposition>();

            GeolocationAccessStatus access = await this.RequestAccessAsync();
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
                            System.Diagnostics.Debug.WriteLine(ex.ToString());
                        }
                    });

                try
                {
                    Looper looperThread = Looper.MyLooper() ?? Looper.MainLooper;

                    int numEnabledProviders = 0;
                    foreach (string provider in this.locationProviders)
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
                            System.Diagnostics.Debug.WriteLine(ex.ToString());
                        }

                        tcs.TrySetException(
                            new GeolocatorException("A location cannot be retrieved as the provider is unavailable."));
                        return await tcs.Task;
                    }
                }
                catch (SecurityException ex)
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

            return await tcs.Task;
        }

        /// <summary>Requests permission to access location data.</summary>
        /// <returns>A GeolocationAccessStatus that indicates if permission to location data has been granted.</returns>
        public Task<GeolocationAccessStatus> RequestAccessAsync()
        {
            GeolocationAccessStatus status = this.locationProviders.Any(this.locationManager.IsProviderEnabled)
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

                Looper looperThread = Looper.MyLooper() ?? Looper.MainLooper;
                foreach (string provider in this.locationProviders)
                {
                    this.locationManager.RequestLocationUpdates(
                        provider,
                        this.reportInterval,
                        (float)this.MovementThreshold,
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
#endif