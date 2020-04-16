namespace XPlat.Services.Maps
{
    using System.Collections.Generic;

    using XPlat.Services.Maps.Extensions;

    /// <summary>Returns the result of a MapLocationFinder query.</summary>
    public class MapLocationFinderResult : IMapLocationFinderResult
    {
        private readonly List<MapLocation> locations;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapLocationFinderResult"/> class.
        /// </summary>
        public MapLocationFinderResult()
        {
            this.locations = new List<MapLocation>();
        }

#if WINDOWS_UWP
        /// <summary>
        /// Initializes a new instance of the <see cref="MapLocationFinderResult"/> class.
        /// </summary>
        public MapLocationFinderResult(Windows.Services.Maps.MapLocationFinderResult result)
        {
            this.locations = new List<MapLocation>();

            if (result != null)
            {
                if (result.Locations != null)
                {
                    foreach (Windows.Services.Maps.MapLocation location in result.Locations)
                    {
                        this.locations.Add(location);
                    }
                }

                this.Status = result.Status.ToInternalMapLocationFinderStatus();
            }
        }

        public static implicit operator MapLocationFinderResult(Windows.Services.Maps.MapLocationFinderResult result)
        {
            return new MapLocationFinderResult(result);
        }
#elif __ANDROID__
        /// <summary>
        /// Initializes a new instance of the <see cref="MapLocationFinderResult"/> class.
        /// </summary>
        public MapLocationFinderResult(IEnumerable<Android.Locations.Address> result, MapLocationFinderStatus status)
        {
            this.locations = new List<MapLocation>();

            if (result != null)
            {
                foreach (Android.Locations.Address location in result)
                {
                    this.locations.Add(location);
                }
            }

            this.Status = status;
        }
#elif __IOS__
        /// <summary>
        /// Initializes a new instance of the <see cref="MapLocationFinderResult"/> class.
        /// </summary>
        public MapLocationFinderResult(CoreLocation.CLPlacemark[] result, MapLocationFinderStatus status)
        {
            this.locations = new List<MapLocation>();

            if (result != null)
            {
                foreach (CoreLocation.CLPlacemark location in result)
                {
                    this.locations.Add(location);
                }
            }

            this.Status = status;
        }
#endif

        /// <summary>Gets the list of locations found by a MapLocationFinder query.</summary>
        public IReadOnlyList<MapLocation> Locations => this.locations;

        /// <summary>Gets the status of a MapLocationFinder query.</summary>
        public MapLocationFinderStatus Status { get; internal set; }
    }
}