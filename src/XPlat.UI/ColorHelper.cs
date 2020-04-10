namespace XPlat.UI
{
    /// <summary>Provides static helper methods for processing Color values. C# and Microsoft Visual Basic code should use methods of Color instead.</summary>
    public class ColorHelper
    {
        /// <summary>Generates a Color structure, based on discrete **Byte** values for **ARGB** components. C# and Microsoft Visual Basic code should use **Color.FromArgb** instead.</summary>
        /// <param name="a">The **A** (transparency) component of the desired color. Range is 0-255.</param>
        /// <param name="r">The **R** component of the desired color. Range is 0-255.</param>
        /// <param name="g">The **G** component of the desired color. Range is 0-255.</param>
        /// <param name="b">The **B** component of the desired color. Range is 0-255.</param>
        /// <returns>The generated Color value.</returns>
        public static Color FromArgb(byte a, byte r, byte g, byte b)
        {
            return new Color(a, r, g, b);
        }
    }
}