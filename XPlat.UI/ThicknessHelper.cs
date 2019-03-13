namespace XPlat.UI
{
    /// <summary>Provides helper methods to evaluate or set Thickness values.</summary>
    public class ThicknessHelper
    {
        /// <summary>Creates a Thickness value based on element values.</summary>
        /// <param name="left">The initial **Left**.</param>
        /// <param name="top">The initial **Top**.</param>
        /// <param name="right">The initial **Right**.</param>
        /// <param name="bottom">The initial **Bottom**.</param>
        /// <returns>The created Thickness.</returns>
        public static Thickness FromLengths(double left, double top, double right, double bottom)
        {
            return new Thickness(left, top, right, bottom);
        }

        /// <summary>Creates a new Thickness value using a uniform value for all the element values.</summary>
        /// <param name="uniformLength">The uniform value to apply to all four of the Thickness element values.</param>
        /// <returns>The created Thickness.</returns>
        public static Thickness FromUniformLength(double uniformLength)
        {
            return new Thickness(uniformLength);
        }
    }
}