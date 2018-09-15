#if __IOS__
namespace XPlat.Extensions
{
    using System;
    using System.Globalization;
    using Foundation;

    public static class NSObjectExtensions
    {
        public static object ToObject(this NSObject obj)
        {
            object val = null;

            if (obj != null)
            {
                if (obj is NSString)
                {
                    return obj.ToString();
                }

                if (obj is NSDate date)
                {
                    return date.ToDateTimeOffset();
                }

                if (obj is NSUuid uuid)
                {
                    return new Guid(uuid.GetBytes());
                }

                if (obj is NSDecimalNumber)
                {
                    return decimal.Parse(obj.ToString(), CultureInfo.InvariantCulture);
                }

                if (obj is NSNumber number)
                {
                    NSNumber nsNumber = number;
                    switch (nsNumber.ObjCType)
                    {
                        case "c": return nsNumber.BoolValue;
                        case "l":
                        case "i": return nsNumber.Int32Value;
                        case "s": return nsNumber.Int16Value;
                        case "q": return nsNumber.Int64Value;
                        case "Q": return nsNumber.UInt64Value;
                        case "C": return nsNumber.ByteValue;
                        case "L":
                        case "I": return nsNumber.UInt32Value;
                        case "S": return nsNumber.UInt16Value;
                        case "f": return nsNumber.FloatValue;
                        case "d": return nsNumber.DoubleValue;
                        case "B": return nsNumber.BoolValue;
                        default: return nsNumber.ToString();
                    }
                }

                if (obj.GetType() == typeof(NSString))
                {
                    val = ((NSString)obj).ToString();
                }
                else if (obj.GetType() == typeof(NSDate))
                {
                    val = ((NSDate)obj).ToDateTimeOffset();
                }
            }

            return val;
        }
    }
}
#endif