namespace XPlat.Storage
{
    using System;
    using System.Globalization;

    using global::Foundation;

    public static partial class Extensions
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

                var date = obj as NSDate;
                if (date != null)
                {
                    return date.ToDateTimeOffset();
                }

                var uuid = obj as NSUuid;
                if (uuid != null)
                {
                    return new Guid(uuid.GetBytes());
                }

                if (obj is NSDecimalNumber)
                {
                    return decimal.Parse(obj.ToString(), CultureInfo.InvariantCulture);
                }

                var number = obj as NSNumber;
                if (number != null)
                {
                    var nsNumber = number;
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