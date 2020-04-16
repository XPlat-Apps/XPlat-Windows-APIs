#if __IOS__ 
namespace XPlat.Services.Maps
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Mime;
    using System.Threading.Tasks;

    using CoreLocation;

    using XPlat.Device.Geolocation;

    /// <summary>Provides methods to convert addresses to geographic locations (geocoding) and to convert geographic locations to addresses (reverse geocoding).</summary>
    public static class MapLocationFinder
    {
        /// <summary>Converts a geopoint to a collection of addresses with the desired accuracy (reverse geocoding).</summary>
        /// <param name="queryPoint">The point for which you want to get locations.</param>
        /// <param name="accuracy">The desired accuracy for which you want to get locations.</param>
        /// <returns>When this method completes successfully, it returns a list of locations contained in the MapLocationFinderResult.</returns>
        public static async Task<MapLocationFinderResult> FindLocationsAtAsync(Geopoint queryPoint, MapLocationDesiredAccuracy accuracy)
        {
            var geocoder = new CLGeocoder();

            CLPlacemark[] locationFindResult = null;

            MapLocationFinderStatus status;

            try
            {
                locationFindResult = await geocoder.ReverseGeocodeLocationAsync(
                                                         new CLLocation(queryPoint.Position.Latitude, queryPoint.Position.Longitude));

                status = MapLocationFinderStatus.Success;
            }
            catch (Exception)
            {
                status = MapLocationFinderStatus.UnknownError;
            }

            return new MapLocationFinderResult(locationFindResult, status);
        }

        /// <summary>Converts a geographic location to a collection of addresses (reverse geocoding).</summary>
        /// <param name="queryPoint">The point for which you want to get locations.</param>
        /// <returns>When this method completes successfully, it returns a list of locations contained in the MapLocationFinderResult.</returns>
        public static Task<MapLocationFinderResult> FindLocationsAtAsync(Geopoint queryPoint)
        {
            return FindLocationsAtAsync(queryPoint, MapLocationDesiredAccuracy.High);
        }
    }
}
#endif