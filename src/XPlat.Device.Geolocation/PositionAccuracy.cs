namespace XPlat.Device.Geolocation
{
    /// <summary>Indicates the requested accuracy level for the location data that the application uses.</summary>
    public enum PositionAccuracy
    {
        /// <summary>Optimize for power, performance, and other cost considerations.</summary>
        Default,

        /// <summary>Deliver the most accurate report possible. This includes using services that might charge money, or consuming higher levels of battery power or connection bandwidth. An accuracy level of High may degrade system performance and should be used only when necessary.</summary>
        High
    }
}