namespace XPlat.API
{
    using System;

    public static partial class Extensions
    {
        /// <summary>
        /// Checks whether a DateTime object is valid by checking it is within the maximum age to the current DateTime.
        /// </summary>
        /// <param name="date">
        /// The date to check.
        /// </param>
        /// <param name="maximumAge">
        /// The maximum valid age.
        /// </param>
        /// <returns>
        /// When this method completes, returns true if valid; else false.
        /// </returns>
        public static bool IsValid(this DateTime date, TimeSpan maximumAge)
        {
            return date <= DateTime.UtcNow.Subtract(maximumAge);
        }

        /// <summary>
        /// Checks whether a DateTimeOffset object is valid by checking it is within the maximum age to the current DateTime.
        /// </summary>
        /// <param name="date">
        /// The date to check.
        /// </param>
        /// <param name="maximumAge">
        /// The maximum valid age.
        /// </param>
        /// <returns>
        /// When this method completes, returns true if valid; else false.
        /// </returns>
        public static bool IsValid(this DateTimeOffset date, TimeSpan maximumAge)
        {
            return date <= DateTime.UtcNow.Subtract(maximumAge);
        }
    }
}