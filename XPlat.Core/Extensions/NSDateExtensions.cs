#if __IOS__
namespace XPlat.Extensions
{
    using System;
    using Foundation;

    public static class NSDateExtensions
    {
        private static readonly DateTimeOffset DateReference = new DateTimeOffset(2001, 1, 1, 0, 0, 0, TimeSpan.Zero);

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