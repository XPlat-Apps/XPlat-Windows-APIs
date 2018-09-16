#if __ANDROID__
namespace XPlat.Devices.Geolocation
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Android.Locations;
    using Android.OS;

    internal class LocationRetriever : Java.Lang.Object, ILocationListener
    {
        private readonly HashSet<string> activeLocationProviders;

        private readonly TaskCompletionSource<Geoposition> tcs = new TaskCompletionSource<Geoposition>();

        private readonly object obj = new object();

        private readonly Action completedAction;

        private readonly uint desiredAccuracyInMeters;

        private Timer timer;

        private Location undesiredLocation;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationRetriever"/> class.
        /// </summary>
        /// <param name="desiredAccuracyInMeters">
        /// The desired accuracy to retrieve a location for, in meters.
        /// </param>
        /// <param name="timeout">
        /// The time required for the retrieval to timeout.
        /// </param>
        /// <param name="activeLocationProviders">
        /// The active location providers.
        /// </param>
        /// <param name="completedAction">
        /// An action to be called when the retrieval is complete.
        /// </param>
        public LocationRetriever(
            uint desiredAccuracyInMeters,
            TimeSpan timeout,
            IEnumerable<string> activeLocationProviders,
            Action completedAction)
        {
            this.desiredAccuracyInMeters = desiredAccuracyInMeters;
            this.activeLocationProviders = new HashSet<string>(activeLocationProviders);
            this.completedAction = completedAction;

            if (timeout != TimeSpan.MaxValue)
            {
                this.timer = new Timer(this.OnTimeout, null, timeout, TimeSpan.Zero);
            }
        }

        /// <summary>
        /// Gets the task used to return the Geoposition as a result.
        /// </summary>
        public Task<Geoposition> Task => this.tcs.Task;

        /// <summary>Called when the location has changed.</summary>
        /// <param name="location">The new location, as a Location object.</param>
        public void OnLocationChanged(Location location)
        {
            if (location.Accuracy <= this.desiredAccuracyInMeters)
            {
                this.CompleteWithResult(location);
                return;
            }

            lock (this.obj)
            {
                // We are going to store the undesired location if we can't get a higher accuracy.
                if (this.undesiredLocation == null || location.Accuracy <= this.undesiredLocation.Accuracy)
                {
                    this.undesiredLocation = location;
                }
            }
        }

        /// <summary>Called when the provider is disabled by the user.</summary>
        /// <param name="provider">The name of the location provider associated with this update.</param>
        public void OnProviderDisabled(string provider)
        {
            lock (this.activeLocationProviders)
            {
                if (this.activeLocationProviders.Remove(provider) && this.activeLocationProviders.Count == 0)
                {
                    this.tcs.TrySetException(
                        new GeolocatorException("A location cannot be retrieved as the provider is unavailable."));
                }
            }
        }

        /// <summary>Called when the provider is enabled by the user.</summary>
        /// <param name="provider">The name of the location provider associated with this update.</param>
        public void OnProviderEnabled(string provider)
        {
            lock (this.activeLocationProviders)
            {
                this.activeLocationProviders.Add(provider);
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
            }
        }

        /// <summary>
        /// Cancels the retrieval of the location.
        /// </summary>
        public void Cancel()
        {
            this.tcs.TrySetCanceled();
        }

        private void OnTimeout(object state)
        {
            lock (this.obj)
            {
                if (this.undesiredLocation == null)
                {
                    if (this.tcs.TrySetResult(default(Geoposition)))
                    {
                        this.completedAction?.Invoke();
                    }
                }
                else
                {
                    this.CompleteWithResult(this.undesiredLocation);
                }
            }
        }

        private void CompleteWithResult(Location location)
        {
            this.completedAction?.Invoke();
            this.tcs.TrySetResult(new Geoposition(location));
        }
    }
}
#endif