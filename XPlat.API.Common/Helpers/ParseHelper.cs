namespace XPlat.API.Helpers
{
    public static class ParseHelper
    {
        public static uint SafeParseUInt(object integer)
        {
            uint result = 0;
            if (integer != null)
                uint.TryParse(integer.ToString(), out result);
            return result;
        }
    }
}
