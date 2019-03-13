namespace XPlat.UI
{
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>Describes the thickness of a frame around a rectangle. Four Double values describe the Left, Top, Right, and Bottom sides of the rectangle, respectively.</summary>
    public struct Thickness
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness"/> struct.
        /// </summary>
        /// <param name="uniformLength">
        /// A single length value to apply to all parts of the thickness in pixels.
        /// </param>
        public Thickness(double uniformLength)
        {
            this.Left = this.Top = this.Right = this.Bottom = uniformLength;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness"/> struct.
        /// </summary>
        /// <param name="left">
        /// The left value.
        /// </param>
        /// <param name="top">
        /// The top value.
        /// </param>
        /// <param name="right">
        /// The right value.
        /// </param>
        /// <param name="bottom">
        /// The bottom value.
        /// </param>
        public Thickness(double left, double top, double right, double bottom)
        {
            this.Left = left;
            this.Top = top;
            this.Right = right;
            this.Bottom = bottom;
        }

#if WINDOWS_UWP
        /// <summary>
        /// Initializes a new instance of the <see cref="Thickness"/> class.
        /// </summary>
        /// <param name="thickness">
        /// The thickness value.
        /// </param>
        public Thickness(Windows.UI.Xaml.Thickness thickness)
        {
            this.Bottom = thickness.Bottom;
            this.Left = thickness.Left;
            this.Right = thickness.Right;
            this.Top = thickness.Top;
        }

        /// <summary>
        /// Creates a new <see cref="Thickness"/> object based on the given <see cref="Windows.UI.Xaml.Thickness"/> value.
        /// </summary>
        /// <param name="thickness">
        /// The thickness value.
        /// </param>
        public static implicit operator Thickness(Windows.UI.Xaml.Thickness thickness)
        {
            return new Thickness(thickness);
        }

        /// <summary>
        /// Creates a new <see cref="Windows.UI.Xaml.Thickness"/> object based on the given <see cref="Thickness"/> value.
        /// </summary>
        /// <param name="thickness">
        /// The thickness value.
        /// </param>
        public static implicit operator Windows.UI.Xaml.Thickness(Thickness thickness)
        {
            return new Windows.UI.Xaml.Thickness(thickness.Left, thickness.Top, thickness.Right, thickness.Bottom);
        }
#endif

        /// <summary>
        /// Gets or sets the left value.
        /// </summary>
        public double Left { get; set; }

        /// <summary>
        /// Gets or sets the top value.
        /// </summary>
        public double Top { get; set; }

        /// <summary>
        /// Gets or sets the right value.
        /// </summary>
        public double Right { get; set; }

        /// <summary>
        /// Gets or sets the bottom value.
        /// </summary>
        public double Bottom { get; set; }

        /// <summary>
        /// Checks the equality of two thickness items.
        /// </summary>
        /// <param name="t1">
        /// The thickness 1.
        /// </param>
        /// <param name="t2">
        /// The thickness 2.
        /// </param>
        /// <returns>
        /// Returns true if the thickness values are equal.
        /// </returns>
        public static bool operator ==(Thickness t1, Thickness t2)
        {
            return t1.Equals(t2);
        }

        /// <summary>
        /// Checks the inequality of two thickness items.
        /// </summary>
        /// <param name="t1">
        /// The thickness 1.
        /// </param>
        /// <param name="t2">
        /// The thickness 2.
        /// </param>
        /// <returns>
        /// Returns true if the thickness values are not equal.
        /// </returns>
        public static bool operator !=(Thickness t1, Thickness t2)
        {
            return !t1.Equals(t2);
        }

#if __ANDROID__
        /// <summary>
        /// Converts the thickness value to the correct density pixels for the device.
        /// </summary>
        /// <returns>
        /// Returns the thickness converted to density pixels.
        /// </returns>
        public Thickness InDensityPixels()
        {
            float d = Android.App.Application.Context.Resources.DisplayMetrics.Density;
            return new Thickness(this.Left / d, this.Top / d, this.Right / d, this.Bottom / d);
        }
#endif

        /// <summary>Returns the fully qualified type name of this instance.</summary>
        /// <returns>A <see cref="T:System.String" /> containing a fully qualified type name.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return this.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">
        /// The object to compare with the current object.
        /// </param>
        /// <returns>
        /// Returns true if the specified object is equal to the current object.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == this.GetType() && this.Equals((Thickness)obj);
        }

        /// <summary>
        /// Checks the equality of the current thickness and the given thickness.
        /// </summary>
        /// <param name="other">
        /// The other thickness to compare.
        /// </param>
        /// <returns>
        /// Return true if the thickness values are equal.
        /// </returns>
        public bool Equals(Thickness other)
        {
            return this.Bottom.Equals(other.Bottom) && this.Left.Equals(other.Left) && this.Right.Equals(other.Right) && this.Top.Equals(other.Top);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>
        /// A hash code for the current object.
        /// </returns>
        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.Bottom.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Left.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Right.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Top.GetHashCode();
                return hashCode;
            }
        }

        internal string ToString(CultureInfo cultureInfo)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(this.InternalToString(this.Left, cultureInfo));
            stringBuilder.Append(",");
            stringBuilder.Append(this.InternalToString(this.Top, cultureInfo));
            stringBuilder.Append(",");
            stringBuilder.Append(this.InternalToString(this.Right, cultureInfo));
            stringBuilder.Append(",");
            stringBuilder.Append(this.InternalToString(this.Bottom, cultureInfo));
            return stringBuilder.ToString();
        }

        internal string InternalToString(double value, CultureInfo cultureInfo)
        {
            return double.IsNaN(value) ? "Auto" : Convert.ToString(value, (IFormatProvider)cultureInfo);
        }
    }
}