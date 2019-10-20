namespace XPlat.Services.Maps
{
    /// <summary>Returns the status of a MapLocationFinder query. This enumeration provides values for the Status property of a MapLocationFinderResult.</summary>
    public enum MapLocationFinderStatus
    {
        /// <summary>Query search operation was successful. Check result size before accessing results.</summary>
        Success,

        /// <summary>The query returned an unknown error.</summary>
        UnknownError,

        /// <summary>The query provided credentials that are not valid.</summary>
        InvalidCredentials,

        /// <summary>The specified point cannot be converted to a location. For example, the point is in an ocean or a desert.</summary>
        BadLocation,

        /// <summary>The query encountered an internal error.</summary>
        IndexFailure,

        /// <summary>The query encountered a network failure.</summary>
        NetworkFailure,

        /// <summary>The query is not supported.</summary>
        NotSupported
    }
}