using System.Runtime.InteropServices;

using static Geolocation.Implementations.Private.Constants.Location;

namespace Geolocation.Implementations.Private
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    unsafe struct Location
    {
        public fixed sbyte Country[CountrySize];
        public fixed sbyte Region[RegionSize];
        public fixed sbyte Postal[PostalSize];
        public fixed sbyte City[CitySize];
        public fixed sbyte Organization[OrganizationSize];
        public float Latitude;
        public float Longitude;
    }
}
