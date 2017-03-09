namespace XPlat.API.Device.Geolocation
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Android.Content;
    using Android.Locations;

    using XPlat.API.Foundation;

    public class Geolocator : IGeolocator
    {
        private readonly LocationManager locationManager;

        private string[] providers;

        /// <summary>
        /// Initializes a new instance of the <see cref="Geolocator"/> class.
        /// </summary>
        public Geolocator(Context context)
        {
            this.locationManager = (LocationManager)context.GetSystemService(Context.LocationService);
            this.providers = this.locationManager.GetProviders(false).Where(p => p != LocationManager.PassiveProvider).ToArray();
        }

        /// <inheritdoc />
        public event TypedEventHandler<IGeolocator, PositionChangedEventArgs> PositionChanged;

        /// <inheritdoc />
        public event TypedEventHandler<IGeolocator, StatusChangedEventArgs> StatusChanged;

        /// <inheritdoc />
        public uint ReportInterval { get; set; }

        /// <inheritdoc />
        public double MovementThreshold { get; set; }

        /// <inheritdoc />
        public PositionStatus LocationStatus { get; private set; }

        /// <inheritdoc />
        public PositionAccuracy DesiredAccuracy { get; set; }

        /// <inheritdoc />
        public uint DesiredAccuracyInMeters { get; set; }

        /// <inheritdoc />
        public Task<Geoposition> GetGeopositionAsync()
        {
            return Task.FromResult(default(Geoposition));
        }

        /// <inheritdoc />
        public Task<Geoposition> GetGeopositionAsync(TimeSpan timeout)
        {
            return Task.FromResult(default(Geoposition));
        }

        /// <inheritdoc />
        public Task<GeolocationAccessStatus> RequestAccessAsync()
        {
            return Task.FromResult(GeolocationAccessStatus.Unspecified);
        }
    }
}