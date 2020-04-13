namespace XPlat.Services.Maps
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>Represents an address.</summary>
    public class MapAddress : IMapAddress
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapAddress"/> class.
        /// </summary>
        public MapAddress()
        {
        }

#if WINDOWS_UWP
        /// <summary>
        /// Initializes a new instance of the <see cref="MapAddress"/> class.
        /// </summary>
        public MapAddress(Windows.Services.Maps.MapAddress mapAddress)
        {
            this.BuildingName = mapAddress.BuildingName;
            this.BuildingFloor = mapAddress.BuildingFloor;
            this.BuildingRoom = mapAddress.BuildingRoom;
            this.BuildingWing = mapAddress.BuildingWing;
            this.StreetNumber = mapAddress.StreetNumber;
            this.Street = mapAddress.Street;
            this.Neighborhood = mapAddress.Neighborhood;
            this.District = mapAddress.District;
            this.Town = mapAddress.Town;
            this.Region = mapAddress.Region;
            this.RegionCode = mapAddress.RegionCode;
            this.Country = mapAddress.Country;
            this.CountryCode = mapAddress.CountryCode;
            this.PostCode = mapAddress.PostCode;
            this.Continent = mapAddress.Continent;
        }

        public static implicit operator MapAddress(Windows.Services.Maps.MapAddress mapAddress)
        {
            return new MapAddress(mapAddress);
        }
#elif __ANDROID__
        /// <summary>
        /// Initializes a new instance of the <see cref="MapAddress"/> class.
        /// </summary>
        public MapAddress(Android.Locations.Address mapLocation)
        {
            this.StreetNumber = mapLocation.SubThoroughfare;
            this.Street = mapLocation.Thoroughfare;
            this.Neighborhood = mapLocation.SubLocality;
            this.District = mapLocation.SubAdminArea;
            this.Town = mapLocation.Locality;
            this.Region = mapLocation.AdminArea;
            this.Country = mapLocation.CountryName;
            this.CountryCode = mapLocation.CountryCode;
            this.PostCode = mapLocation.PostalCode;
        }

        public static implicit operator MapAddress(Android.Locations.Address mapAddress)
        {
            return new MapAddress(mapAddress);
        }
#elif __IOS__
        /// <summary>
        /// Initializes a new instance of the <see cref="MapAddress"/> class.
        /// </summary>
        public MapAddress(CoreLocation.CLPlacemark mapLocation)
        {
            this.StreetNumber = mapLocation.SubThoroughfare;
            this.Street = mapLocation.Thoroughfare;
            this.Neighborhood = mapLocation.SubLocality;
            this.District = mapLocation.SubAdministrativeArea;
            this.Town = mapLocation.Locality;
            this.Region = mapLocation.AdministrativeArea;
            this.Country = mapLocation.Country;
            this.CountryCode = mapLocation.IsoCountryCode;
            this.PostCode = mapLocation.PostalCode;
        }

        public static implicit operator MapAddress(CoreLocation.CLPlacemark mapAddress)
        {
            return new MapAddress(mapAddress);
        }
#endif

        /// <summary>Gets the building name of an address.</summary>
        public string BuildingName { get; internal set; }

        /// <summary>Gets the building floor of an address.</summary>
        public string BuildingFloor { get; internal set; }

        /// <summary>Gets the building room of an address.</summary>
        public string BuildingRoom { get; internal set; }

        /// <summary>Gets the building wing of an address.</summary>
        public string BuildingWing { get; internal set; }

        /// <summary>Gets the street number of an address.</summary>
        public string StreetNumber { get; internal set; }

        /// <summary>Gets the street of an address.</summary>
        public string Street { get; internal set; }

        /// <summary>Gets the neighborhood of an address.</summary>
        public string Neighborhood { get; internal set; }

        /// <summary>Gets the district of an address.</summary>
        public string District { get; internal set; }

        /// <summary>Gets the town or city of an address.</summary>
        public string Town { get; internal set; }

        /// <summary>Gets the region (for example, the state or province) of an address.</summary>
        public string Region { get; internal set; }

        /// <summary>Gets the code for the region (for example, the state or province) of an address.</summary>
        public string RegionCode { get; internal set; }

        /// <summary>Gets the country of an address.</summary>
        public string Country { get; internal set; }

        /// <summary>Gets the country code of an address.</summary>
        public string CountryCode { get; internal set; }

        /// <summary>Gets the postal code of an address.</summary>
        public string PostCode { get; internal set; }

        /// <summary>Gets the continent of an address.</summary>
        public string Continent { get; internal set; }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            string address = string.Empty;
            var addressList = new List<string>();

            if (!string.IsNullOrWhiteSpace(this.StreetNumber))
            {
                address = !string.IsNullOrWhiteSpace(this.BuildingName)
                              ? string.Format("{0} {1},", this.StreetNumber, this.BuildingName)
                              : string.Format("{0}", this.StreetNumber);
            }
            else if (!string.IsNullOrWhiteSpace(this.BuildingName))
            {
                address = string.Format("{0},", this.BuildingName);
            }

            if (!string.IsNullOrWhiteSpace(this.Street))
            {
                address = string.Format("{0} {1}", address, this.Street);
            }

            addressList.Add(address);

            if (!string.IsNullOrWhiteSpace(this.Town))
            {
                addressList.Add(this.Town);
            }

            if (!string.IsNullOrWhiteSpace(this.District))
            {
                addressList.Add(this.District);
            }

            if (!string.IsNullOrWhiteSpace(this.PostCode))
            {
                addressList.Add(this.PostCode);
            }

            string toString = string.Join(", ", addressList.Where(x => !string.IsNullOrWhiteSpace(x)));
            return toString.Trim();
        }
    }
}