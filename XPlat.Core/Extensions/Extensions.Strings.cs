namespace XPlat
{
    using System.Globalization;

    public static partial class Extensions
    {
        /// <summary>
        /// Checks whether the specified text contains another phrase.
        /// </summary>
        /// <param name="text">
        /// The text to check.
        /// </param>
        /// <param name="containedString">
        /// The string to check exists within the text.
        /// </param>
        /// <param name="compareOption">
        /// The compare option.
        /// </param>
        /// <returns>
        /// Returns true if the contained string exists in the text; else false.
        /// </returns>
        public static bool Contains(this string text, string containedString, CompareOptions compareOption)
        {
            return CultureInfo.CurrentCulture.CompareInfo.IndexOf(text, containedString, compareOption) >= 0;
        }
    }
}