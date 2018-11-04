namespace XPlat.UI
{
    using System.Globalization;

    /// <summary>Describes a color in terms of alpha, red, green, and blue channels.</summary>
    public struct Color
    {
#if WINDOWS_UWP
        public Color(Windows.UI.Color color)
        {
            this.A = color.A;
            this.R = color.R;
            this.G = color.G;
            this.B = color.B;
        }
#elif __ANDROID__
        public Color(System.Drawing.Color color)
        {
            this.A = color.A;
            this.R = color.R;
            this.G = color.G;
            this.B = color.B;
        }

        public Color(Android.Graphics.Color color)
        {
            this.A = color.A;
            this.R = color.R;
            this.G = color.G;
            this.B = color.B;
        }
#elif __IOS__
        public Color(UIKit.UIColor color)
        {
            if (color != null && color.CGColor != null)
            {
                if (color.CGColor.NumberOfComponents == 2)
                {
                    byte rgb = (byte)(color.CGColor.Components[0] * 255);

                    this.R = rgb;
                    this.G = rgb;
                    this.B = rgb;
                    this.A = (byte)(color.CGColor.Components[1] * 255);
                }
                else
                {
                    this.R = (byte)(color.CGColor.Components[0] * 255);
                    this.G = (byte)(color.CGColor.Components[1] * 255);
                    this.B = (byte)(color.CGColor.Components[2] * 255);
                    this.A = (byte)(color.CGColor.Components[3] * 255);
                }
            }
            else
            {
                this.R = 0;
                this.G = 0;
                this.B = 0;
                this.A = 0;
            }
        }
#endif

        internal Color(byte a, byte r, byte g, byte b)
        {
            this.A = a;
            this.R = r;
            this.G = g;
            this.B = b;
        }

        internal Color(string hexValue)
        {
            string val = hexValue.ToUpper();

            switch (val.Length)
            {
                case 7:
                    this.A = 255;
                    this.R = byte.Parse(val.Substring(1, 2), NumberStyles.AllowHexSpecifier);
                    this.G = byte.Parse(val.Substring(3, 2), NumberStyles.AllowHexSpecifier);
                    this.B = byte.Parse(val.Substring(5, 2), NumberStyles.AllowHexSpecifier);
                    break;
                case 9:
                    this.A = byte.Parse(val.Substring(1, 2), NumberStyles.AllowHexSpecifier);
                    this.R = byte.Parse(val.Substring(3, 2), NumberStyles.AllowHexSpecifier);
                    this.G = byte.Parse(val.Substring(5, 2), NumberStyles.AllowHexSpecifier);
                    this.B = byte.Parse(val.Substring(7, 2), NumberStyles.AllowHexSpecifier);
                    break;
                default:
                    this.A = 0;
                    this.R = 0;
                    this.G = 0;
                    this.B = 0;
                    break;
            }
        }

#if WINDOWS_UWP
        public static implicit operator Color(Windows.UI.Color color)
        {
            return new Color(color);
        }

        public static implicit operator Color(Windows.UI.Xaml.Media.SolidColorBrush colorBrush)
        {
            return new Color(colorBrush.Color);
        }

        public static implicit operator Windows.UI.Color(Color color)
        {
            return Windows.UI.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static implicit operator Windows.UI.Xaml.Media.SolidColorBrush(Color color)
        {
            return new Windows.UI.Xaml.Media.SolidColorBrush(color);
        }
#elif __ANDROID__
        public static implicit operator Color(System.Drawing.Color color)
        {
            return new Color(color);
        }

        public static implicit operator System.Drawing.Color(Color color)
        {
            return System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static implicit operator Color(Android.Graphics.Color color)
        {
            return new Color(color);
        }

        public static implicit operator Android.Graphics.Color(Color color)
        {
            return new Android.Graphics.Color(color.R, color.G, color.B, color.A);
        }
#elif __IOS__
        public static implicit operator Color(UIKit.UIColor color)
        {
            return new Color(color);
        }

        public static implicit operator UIKit.UIColor(Color color)
        {
            return UIKit.UIColor.FromRGBA(color.R, color.G, color.B, color.A);
        }
#endif

        /// <summary>Gets or sets the sRGB alpha channel value of the color. </summary>
        public byte A { get; set; }

        /// <summary>Gets or sets the sRGB blue channel value of the color. </summary>
        public byte B { get; set; }

        /// <summary>Gets or sets the sRGB green channel value of the color. </summary>
        public byte G { get; set; }

        /// <summary>Gets or sets the sRGB red channel value of the color. </summary>
        public byte R { get; set; }

        /// <summary>Tests whether two <see cref="T:Windows.UI.Color" /> structures are identical. </summary>
        /// <returns>true if <paramref name="color1" /> and <paramref name="color2" /> are exactly identical; otherwise, false.</returns>
        /// <param name="color1">The first <see cref="T:Windows.UI.Color" /> structure to compare.</param>
        /// <param name="color2">The second <see cref="T:Windows.UI.Color" /> structure to compare.</param>
        public static bool operator ==(Color color1, Color color2)
        {
            return color1.Equals(color2);
        }

        /// <summary>Tests whether two <see cref="T:Windows.UI.Color" /> structures are not identical. </summary>
        /// <returns>true if <paramref name="color1" /> and <paramref name="color2" /> are not equal; otherwise, false.</returns>
        /// <param name="color1">The first <see cref="T:Windows.UI.Color" /> structure to compare.</param>
        /// <param name="color2">The second <see cref="T:Windows.UI.Color" /> structure to compare.</param>
        public static bool operator !=(Color color1, Color color2)
        {
            return !color1.Equals(color2);
        }

        /// <summary>Creates a new <see cref="Color" /> structure by using the specified sRGB alpha channel and color channel values. </summary>
        /// <returns>A <see cref="Color" /> structure with the specified values.</returns>
        /// <param name="a">The alpha channel, <see cref="A" />, of the new color. The value must be between 0 and 255.</param>
        /// <param name="r">The red channel, <see cref="R" />, of the new color. The value must be between 0 and 255.</param>
        /// <param name="g">The green channel, <see cref="G" />, of the new color. The value must be between 0 and 255.</param>
        /// <param name="b">The blue channel, <see cref="B" />, of the new color. The value must be between 0 and 255.</param>
        public static Color FromArgb(byte a, byte r, byte g, byte b)
        {
            return new Color(a, r, g, b);
        }

        /// <summary>Tests whether the specified object is a <see cref="Color" /> structure and is equivalent to the current color. </summary>
        /// <returns>true if the specified object is a <see cref="Color" /> structure and is identical to the current <see cref="Color" /> structure; otherwise, false.</returns>
        /// <param name="obj">The object to compare to the current <see cref="Color" /> structure.</param>
        public override bool Equals(object obj)
        {
            if (obj is Color color)
            {
                return this.Equals(color);
            }

            return base.Equals(obj);
        }

        /// <summary>Tests whether the specified <see cref="Color" /> structure is identical to the current color.</summary>
        /// <returns>true if the specified <see cref="Color" /> structure is identical to the current <see cref="Color" /> structure; otherwise, false.</returns>
        /// <param name="other">The <see cref="Color" /> structure to compare to the current <see cref="Color" /> structure.</param>
        public bool Equals(Color other)
        {
            return this.A == other.A && this.B == other.B && this.G == other.G && this.R == other.R;
        }

        /// <summary>Gets a hash code for the current <see cref="Color" /> structure. </summary>
        /// <returns>A hash code for the current <see cref="Color" /> structure.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = this.A.GetHashCode();
                hashCode = (hashCode * 397) ^ this.B.GetHashCode();
                hashCode = (hashCode * 397) ^ this.G.GetHashCode();
                hashCode = (hashCode * 397) ^ this.R.GetHashCode();
                return hashCode;
            }
        }
    }
}