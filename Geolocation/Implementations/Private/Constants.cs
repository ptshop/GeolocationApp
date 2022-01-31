namespace Geolocation.Implementations.Private
{
    static class Constants
    {
        public class Header
        {
            public const int NameSize = 32;
        }

        public class Location
        {
            public const int Size = 96;

            public const int CountrySize = 8;
            public const int RegionSize = 12;
            public const int PostalSize = 12;
            public const int CitySize = 24;
            public const int OrganizationSize = 32;
        }
    }
}
