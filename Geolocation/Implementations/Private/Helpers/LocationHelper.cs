using System.Collections.Generic;
using static Geolocation.Implementations.Private.Helpers.StringHelper;
using static Geolocation.Implementations.Private.Helpers.LocationIndexHelper;
using static Geolocation.Implementations.Private.Constants.Location;


namespace Geolocation.Implementations.Private.Helpers
{
    static class LocationHelper
    {
        unsafe public static DTO.Location CreateLocation(Location* pLocation)
        {
            return new DTO.Location()
            {
                Country = CreateString(pLocation->Country, CountrySize),
                Region = CreateString(pLocation->Region, RegionSize),
                Postal = CreateString(pLocation->Postal, PostalSize),
                City = CreateString(pLocation->City, CitySize),
                Organization = CreateString(pLocation->Organization, OrganizationSize),
                Latitude = pLocation->Latitude,
                Longitude = pLocation->Longitude
            };
        }

        unsafe public static DTO.Location[] CreateLocations(Location* pFirstLocation, uint* startIndex, uint* endIndex)
        {
            var locations = new List<DTO.Location>((int)(endIndex - startIndex + 1));
            for (uint* index = startIndex; index <= endIndex; index++)
            {
                locations.Add(CreateLocation(pFirstLocation + ConvertByteIndexToLocationIndex(*index)));
            }

            return locations.ToArray();
        }
    }
}
