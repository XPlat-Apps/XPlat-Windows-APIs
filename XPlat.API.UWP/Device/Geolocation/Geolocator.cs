namespace XPlat.API.Device.Geolocation
{
    using System;
    using System.Threading.Tasks;

    using Windows.Foundation;

    using XPlat.API.Threading.Tasks;

    /// <summary>
    /// Defines a service to access the current geographic location.
    /// </summary>
    public class Geolocator : IGeolocator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Geolocator"/> class.
        /// </summary>
        public Geolocator()
        {
            this.Locator.StatusChanged += this.Locator_OnStatusChanged;
        }

        private Windows.Devices.Geolocation.Geolocator Locator { get; } = new Windows.Devices.Geolocation.Geolocator();

        /// <inheritdoc />
        public event Foundation.TypedEventHandler<IGeolocator, PositionChangedEventArgs> PositionChanged;

        /// <inheritdoc />
        public event Foundation.TypedEventHandler<IGeolocator, StatusChangedEventArgs> StatusChanged;

        /// <inheritdoc />
        public Geoposition LastKnownPosition { get; private set; }

        /// <inheritdoc />
        public uint ReportInterval
        {
            get
            {
                return this.Locator.ReportInterval;
            }
            set
            {
                this.Locator.ReportInterval = value;
            }
        }

        /// <inheritdoc />
        public double MovementThreshold
        {
            get
            {
                return this.Locator.MovementThreshold;
            }
            set
            {
                this.Locator.MovementThreshold = value;
            }
        }

        /// <inheritdoc />
        public PositionStatus LocationStatus { get; private set; }

        /// <inheritdoc />
        public PositionAccuracy DesiredAccuracy
        {
            get
            {
                return this.Locator.DesiredAccuracy == Windows.Devices.Geolocation.PositionAccuracy.Default
                           ? PositionAccuracy.Default
                           : PositionAccuracy.High;
            }
            set
            {
                this.Locator.DesiredAccuracy = value == PositionAccuracy.Default
                                                   ? Windows.Devices.Geolocation.PositionAccuracy.Default
                                                   : Windows.Devices.Geolocation.PositionAccuracy.High;
            }
        }

        /// <inheritdoc />
        public uint DesiredAccuracyInMeters
        {
            get
            {
                return this.Locator.DesiredAccuracyInMeters ?? 0;
            }
            set
            {
                this.Locator.DesiredAccuracyInMeters = value;
            }
        }

        /// <inheritdoc />
        public async Task<Geoposition> GetGeopositionAsync()
        {
            return (await this.Locator.GetGeopositionAsync()).ToLocalGeoposition();
        }

        /// <inheritdoc />
        public Task<Geoposition> GetGeopositionAsync(TimeSpan maximumAge, TimeSpan timeout)
        {
            // Setting the timeout on the Windows API to a year as an exception is thrown when it times out. 
            var getPositionTask = this.Locator.GetGeopositionAsync(maximumAge, TimeSpan.FromDays(365));

            // Creating a specific timeout task to handle this in a nicer way.
            var timeoutTask = new TimeoutTask(timeout, getPositionTask.Cancel);

            var tcs = new TaskCompletionSource<Geoposition>();

            getPositionTask.Completed = (op, s) =>
                {
                    timeoutTask.Cancel();

                    switch (s)
                    {
                        case AsyncStatus.Canceled:
                            tcs.SetCanceled();
                            break;
                        case AsyncStatus.Completed:
                            this.LastKnownPosition = op.GetResults().ToLocalGeoposition();
                            tcs.SetResult(this.LastKnownPosition);
                            break;
                        case AsyncStatus.Error:
                            tcs.SetException(op.ErrorCode);
                            break;
                    }
                };

            return tcs.Task;
        }

        /// <inheritdoc />
        public async Task<GeolocationAccessStatus> RequestAccessAsync()
        {
            Windows.Devices.Geolocation.GeolocationAccessStatus accessStatus;

            if (this.Locator != null)
            {
                this.Locator.PositionChanged -= this.Locator_OnPositionChanged;
            }

            try
            {
                accessStatus = await Windows.Devices.Geolocation.Geolocator.RequestAccessAsync();
            }
            catch (Exception)
            {
                accessStatus = Windows.Devices.Geolocation.GeolocationAccessStatus.Denied;
            }

            if (accessStatus == Windows.Devices.Geolocation.GeolocationAccessStatus.Allowed && this.Locator != null)
            {
                this.Locator.PositionChanged += this.Locator_OnPositionChanged;
            }

            return accessStatus.ToLocalGeolocationAccessStatus();
        }

        private void Locator_OnPositionChanged(
            Windows.Devices.Geolocation.Geolocator sender,
            Windows.Devices.Geolocation.PositionChangedEventArgs args)
        {
            this.PositionChanged?.Invoke(this, new PositionChangedEventArgs(args.Position.ToLocalGeoposition()));
        }

        private void Locator_OnStatusChanged(
            Windows.Devices.Geolocation.Geolocator sender,
            Windows.Devices.Geolocation.StatusChangedEventArgs args)
        {
            this.LocationStatus = args.Status.ToLocalPositionStatus();
            this.StatusChanged?.Invoke(this, new StatusChangedEventArgs(this.LocationStatus));
        }
    }
}