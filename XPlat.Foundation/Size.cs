namespace XPlat.Foundation
{
    using System;

    /// <summary>Represents number values that specify a height and width.</summary>
    public struct Size
    {
        private float width;

        private float height;

        public Size(double width, double height)
        {
            if (width < 0.0)
            {
                throw new ArgumentException("width");
            }

            if (height < 0.0)
            {
                throw new ArgumentException("height");
            }

            this.width = (float) width;
            this.height = (float) height;
        }

        public double Width
        {
            get => (double) this.width;
            set
            {
                if (value < 0.0)
                {
                    throw new ArgumentException("Width");
                }

                this.width = (float) value;
            }
        }

        public double Height
        {
            get => (double) this.height;
            set
            {
                if (value < 0.0)
                {
                    throw new ArgumentException("Height");
                }

                this.height = (float) value;
            }
        }

        public static Size Empty => Size.CreateEmptySize();

        public bool IsEmpty => this.Width < 0.0;

        public static bool operator ==(Size size1, Size size2)
        {
            return size1.Width == size2.Width && size1.Height == size2.Height;
        }

        public static bool operator !=(Size size1, Size size2)
        {
            return !(size1 == size2);
        }

        public override bool Equals(object o)
        {
            return o is Size && Size.Equals(this, (Size) o);
        }

        public bool Equals(Size value)
        {
            return Size.Equals(this, value);
        }

        public override int GetHashCode()
        {
            return this.IsEmpty ? 0 : this.Width.GetHashCode() ^ this.Height.GetHashCode();
        }

        public override string ToString()
        {
            return this.IsEmpty ? "Empty" : $"{(object) this.width},{(object) this.height}";
        }

        private static Size CreateEmptySize()
        {
            return new Size() {width = float.NegativeInfinity, height = float.NegativeInfinity};
        }

        private static bool Equals(Size size1, Size size2)
        {
            return size1.IsEmpty ? size2.IsEmpty : size1.Width.Equals(size2.Width) && size1.Height.Equals(size2.Height);
        }
    }
}