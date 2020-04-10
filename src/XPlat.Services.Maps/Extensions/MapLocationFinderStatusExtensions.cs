namespace XPlat.Services.Maps.Extensions
{
    /// <summary>
    /// Defines a collection of extensions for the MapLocationFinderStatus enum.
    /// </summary>
    public static class MapLocationFinderStatusExtensions
    {
#if WINDOWS_UWP
        /// <summary>
        /// Converts the Windows MapLocationFinderStatus enum to the internal XPlat equivalent.
        /// </summary>
        /// <param name="status">The Windows MapLocationFinderStatus to convert.</param>
        /// <returns>Returns the equivalent XPlat MapLocationFinderStatus value.</returns>
        public static MapLocationFinderStatus ToInternalMapLocationFinderStatus(
            this Windows.Services.Maps.MapLocationFinderStatus status)
        {
            switch (status)
            {
                case Windows.Services.Maps.MapLocationFinderStatus.Success:
                    return MapLocationFinderStatus.Success;
                case Windows.Services.Maps.MapLocationFinderStatus.UnknownError:
                    return MapLocationFinderStatus.UnknownError;
                case Windows.Services.Maps.MapLocationFinderStatus.InvalidCredentials:
                    return MapLocationFinderStatus.InvalidCredentials;
                case Windows.Services.Maps.MapLocationFinderStatus.BadLocation:
                    return MapLocationFinderStatus.BadLocation;
                case Windows.Services.Maps.MapLocationFinderStatus.IndexFailure:
                    return MapLocationFinderStatus.IndexFailure;
                case Windows.Services.Maps.MapLocationFinderStatus.NetworkFailure:
                    return MapLocationFinderStatus.NetworkFailure;
                case Windows.Services.Maps.MapLocationFinderStatus.NotSupported:
                    return MapLocationFinderStatus.NotSupported;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }

        /// <summary>
        /// Converts the XPlat MapLocationFinderStatus enum to the Windows equivalent.
        /// </summary>
        /// <param name="accuracy">The XPlat MapLocationFinderStatus to convert.</param>
        /// <returns>Returns the equivalent Windows MapLocationFinderStatus value.</returns>
        public static Windows.Services.Maps.MapLocationFinderStatus ToWindowsMapLocationFinderStatus(
            this MapLocationFinderStatus accuracy)
        {
            switch (accuracy)
            {
                case MapLocationFinderStatus.Success:
                    return Windows.Services.Maps.MapLocationFinderStatus.Success;
                case MapLocationFinderStatus.UnknownError:
                    return Windows.Services.Maps.MapLocationFinderStatus.UnknownError;
                case MapLocationFinderStatus.InvalidCredentials:
                    return Windows.Services.Maps.MapLocationFinderStatus.InvalidCredentials;
                case MapLocationFinderStatus.BadLocation:
                    return Windows.Services.Maps.MapLocationFinderStatus.BadLocation;
                case MapLocationFinderStatus.IndexFailure:
                    return Windows.Services.Maps.MapLocationFinderStatus.IndexFailure;
                case MapLocationFinderStatus.NetworkFailure:
                    return Windows.Services.Maps.MapLocationFinderStatus.NetworkFailure;
                case MapLocationFinderStatus.NotSupported:
                    return Windows.Services.Maps.MapLocationFinderStatus.NotSupported;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(accuracy), accuracy, null);
            }
        }
#endif
    }
}