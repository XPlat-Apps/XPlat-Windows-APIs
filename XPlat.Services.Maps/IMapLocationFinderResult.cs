namespace XPlat.Services.Maps
{
    using System.Collections.Generic;

    /// <summary>Returns the result of a MapLocationFinder query.</summary>
    public interface IMapLocationFinderResult
    {
        /// <summary>Gets the list of locations found by a MapLocationFinder query.</summary>
        IReadOnlyList<MapLocation> Locations { get; }

        /// <summary>Gets the status of a MapLocationFinder query.</summary>
        MapLocationFinderStatus Status { get; }
    }
}