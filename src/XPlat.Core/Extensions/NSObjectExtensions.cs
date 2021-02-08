// XPlat Apps licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if __IOS__
namespace XPlat.Extensions
{
    using System;
    using System.Globalization;
    using Foundation;

    /// <summary>
    /// Defines a collection of extensions for NSObject objects.
    /// </summary>
    public static class NSObjectExtensions
    {
        /// <summary>
        /// Takes a base NSObject and, depending on it's actual type, will convert the iOS object to a .NET equivalent.
        /// </summary>
        /// <param name="obj">The iOS object to convert to a .NET object.</param>
        /// <returns>Returns the converted .NET object value.</returns>
        public static object ToObject(this NSObject obj)
        {
            if (obj == null)
            {
                return null;
            }

            object val = null;
            
            switch (obj)
            {
                case NSString _:
                    return obj.ToString();
                case NSDate date:
                    return date.ToDateTimeOffset();
                case NSUuid uuid:
                    return new Guid(uuid.GetBytes());
                case NSDecimalNumber _:
                    return decimal.Parse(obj.ToString(), CultureInfo.InvariantCulture);
                case NSNumber number:
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
            }

            if (obj.GetType() == typeof(NSString))
            {
                val = ((NSString)obj).ToString();
            }
            else if (obj.GetType() == typeof(NSDate))
            {
                val = ((NSDate)obj).ToDateTimeOffset();
            }

            return val;
        }
    }
}
#endif