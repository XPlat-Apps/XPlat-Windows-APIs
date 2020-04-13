#if __ANDROID__
namespace XPlat.Services.Maps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Android.App;
    using Android.Locations;

    using Java.IO;
    using Java.Lang;

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
            using (var geocoder = new Geocoder(Application.Context))
            {
                IList<Address> locationFindResult = null;

                MapLocationFinderStatus status;

                try
                {
                    locationFindResult = await geocoder.GetFromLocationAsync(queryPoint.Position.Latitude, queryPoint.Position.Longitude, 1);
                    status = MapLocationFinderStatus.Success;
                }
                catch (IOException)
                {
                    status = MapLocationFinderStatus.UnknownError;
                }
                catch (IllegalArgumentException)
                {
                    status = MapLocationFinderStatus.BadLocation;
                }

                return new MapLocationFinderResult(locationFindResult, status);
            }
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