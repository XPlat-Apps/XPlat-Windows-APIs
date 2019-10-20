namespace XPlat.Services.Maps
{
    /// <summary>Represents an address.</summary>
    public interface IMapAddress
    {
        /// <summary>Gets the building name of an address.</summary>
        string BuildingName { get; }

        /// <summary>Gets the building floor of an address.</summary>
        string BuildingFloor { get; }

        /// <summary>Gets the building room of an address.</summary>
        string BuildingRoom { get; }

        /// <summary>Gets the building wing of an address.</summary>
        string BuildingWing { get; }

        /// <summary>Gets the street number of an address.</summary>
        string StreetNumber { get; }

        /// <summary>Gets the street of an address.</summary>
        string Street { get; }

        /// <summary>Gets the neighborhood of an address.</summary>
        string Neighborhood { get; }

        /// <summary>Gets the district of an address.</summary>
        string District { get; }

        /// <summary>Gets the town or city of an address.</summary>
        string Town { get; }

        /// <summary>Gets the region (for example, the state or province) of an address.</summary>
        string Region { get; }

        /// <summary>Gets the code for the region (for example, the state or province) of an address.</summary>
        string RegionCode { get; }

        /// <summary>Gets the country of an address.</summary>
        string Country { get; }

        /// <summary>Gets the country code of an address.</summary>
        string CountryCode { get; }

        /// <summary>Gets the postal code of an address.</summary>
        string PostCode { get; }

        /// <summary>Gets the continent of an address.</summary>
        string Continent { get; }
    }
}