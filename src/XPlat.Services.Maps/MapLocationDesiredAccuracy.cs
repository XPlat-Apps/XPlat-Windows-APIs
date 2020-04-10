namespace XPlat.Services.Maps
{
    /// <summary>Indicates the desired accuracy to use when converting latitude and longitude coordinates to a physical location like a city or address.</summary>
    public enum MapLocationDesiredAccuracy
    {
        /// <summary>Leverage the underlying REST API call to get richer and more accurate results.</summary>
        High,

        /// <summary>Leverage the maps disk cache to get accurate info up to the city level.</summary>
        Low,
    }
}