namespace XPlat.Helpers
{
    /// <summary>Represents a helper for generating request codes for applications.</summary>
    public static class RequestCodeHelper
    {
        public static int LastRequestCode { get; private set; }

        /// <summary>Generates an integer request code.</summary>
        /// <returns>Returns a value ranging from 1 to 65535 (ushort.MaxValue).</returns>
        public static int GenerateRequestCode()
        {
            // Request codes have a maximum value of 65535.
            if (LastRequestCode == ushort.MaxValue)
            {
                LastRequestCode = 0;
            }

            return ++LastRequestCode;
        }

        /// <summary>Resets the request code.</summary>
        public static void Reset()
        {
            LastRequestCode = 0;
        }
    }
}