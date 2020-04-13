#if WINDOWS_UWP 
namespace XPlat.Services.Maps
{
    using System;
    using System.Threading.Tasks;

    using XPlat.Device.Geolocation;
    using XPlat.Services.Maps.Extensions;

    /// <summary>Provides methods to convert addresses to geographic locations (geocoding) and to convert geographic locations to addresses (reverse geocoding).</summary>
    public static class MapLocationFinder
    {
        /// <summary>Converts a geopoint to a collection of addresses with the desired accuracy (reverse geocoding).</summary>
        /// <param name="queryPoint">The point for which you want to get locations.</param>
        /// <param name="accuracy">The desired accuracy for which you want to get locations.</param>
        /// <returns>When this method completes successfully, it returns a list of locations contained in the MapLocationFinderResult.</returns>
        public static async Task<MapLocationFinderResult> FindLocationsAtAsync(Geopoint queryPoint, MapLocationDesiredAccuracy accuracy)
        {
            Windows.Services.Maps.MapLocationFinderResult result = await Windows.Services.Maps.MapLocationFinder.FindLocationsAtAsync(queryPoint, accuracy.ToWindowsMapLocationDesiredAccuracy());
            return result;
        }

        /// <summary>Converts a geographic location to a collection of addresses (reverse geocoding).</summary>
        /// <param name="queryPoint">The point for which you want to get locations.</param>
        /// <returns>When this method completes successfully, it returns a list of locations contained in the MapLocationFinderResult.</returns>
        public static async Task<MapLocationFinderResult> FindLocationsAtAsync(Geopoint queryPoint)
        {
            Windows.Services.Maps.MapLocationFinderResult result = await Windows.Services.Maps.MapLocationFinder.FindLocationsAtAsync(queryPoint);
            return result;
        }
    }
}
#endif