namespace XPlat.Services.Maps.Extensions
{
    /// <summary>
    /// Defines a collection of extensions for the MapLocationDesiredAccuracy enum.
    /// </summary>
    public static class MapLocationDesiredAccuracyExtensions
    {
#if WINDOWS_UWP
        /// <summary>
        /// Converts the Windows MapLocationDesiredAccuracy enum to the internal XPlat equivalent.
        /// </summary>
        /// <param name="accuracy">The Windows MapLocationDesiredAccuracy to convert.</param>
        /// <returns>Returns the equivalent XPlat MapLocationDesiredAccuracy value.</returns>
        public static MapLocationDesiredAccuracy ToInternalMapLocationDesiredAccuracy(
            this Windows.Services.Maps.MapLocationDesiredAccuracy accuracy)
        {
            switch (accuracy)
            {
                case Windows.Services.Maps.MapLocationDesiredAccuracy.High:
                    return MapLocationDesiredAccuracy.High;
                case Windows.Services.Maps.MapLocationDesiredAccuracy.Low:
                    return MapLocationDesiredAccuracy.Low;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(accuracy), accuracy, null);
            }
        }

        /// <summary>
        /// Converts the XPlat MapLocationDesiredAccuracy enum to the Windows equivalent.
        /// </summary>
        /// <param name="accuracy">The XPlat MapLocationDesiredAccuracy to convert.</param>
        /// <returns>Returns the equivalent Windows MapLocationDesiredAccuracy value.</returns>
        public static Windows.Services.Maps.MapLocationDesiredAccuracy ToWindowsMapLocationDesiredAccuracy(
            this MapLocationDesiredAccuracy accuracy)
        {
            switch (accuracy)
            {
                case MapLocationDesiredAccuracy.High:
                    return Windows.Services.Maps.MapLocationDesiredAccuracy.High;
                case MapLocationDesiredAccuracy.Low:
                    return Windows.Services.Maps.MapLocationDesiredAccuracy.Low;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(accuracy), accuracy, null);
            }
        }
#endif
    }
}
