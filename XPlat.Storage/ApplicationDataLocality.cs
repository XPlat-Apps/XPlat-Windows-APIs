namespace XPlat.Storage
{
    /// <summary>Specifies the type of an application data store.</summary>
    public enum ApplicationDataLocality
    {
        /// <summary>The data resides in the local application data store.</summary>
        Local,

        /// <summary>The data resides in the roaming application data store.</summary>
        Roaming,

        /// <summary>The data resides in the temporary application data store.</summary>
        Temporary,

        /// <summary>The data resides in the local cache for the application data store.</summary>
        LocalCache,
        Shared
    }
}