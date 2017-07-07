namespace XPlat.Storage
{
    using System;

    using global::Foundation;

    public static partial class Extensions
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