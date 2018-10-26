#if __IOS__
namespace XPlat.Extensions
{
    using System;
    using Foundation;

    /// <summary>
    /// Defines a collection of extensions for NSDate objects.
    /// </summary>
    public static class NSDateExtensions
    {
        private static readonly DateTimeOffset DateReference = new DateTimeOffset(2001, 1, 1, 0, 0, 0, TimeSpan.Zero);

        /// <summary>
        /// Converts an NSDate object to a DateTimeOffset object.
        /// </summary>
        /// <param name="date">The date to convert to a DateTimeOffset.</param>
        /// <returns>Returns the converted DateTimeOffset value.</returns>
        public static DateTimeOffset ToDateTimeOffset(this NSDate date)
        {
            return date.Equals(NSDate.DistantPast)
                ? DateTimeOffset.MinValue
                : (date.Equals(NSDate.DistantFuture)
                    ? DateTimeOffset.MaxValue
                    : DateReference.AddSeconds(date.SecondsSinceReferenceDate));
        }
    }
}
#endif