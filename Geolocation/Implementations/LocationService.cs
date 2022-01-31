using Geolocation.Implementations.Private;
using Geolocation.Implementations.Private.Finders;
using static Geolocation.Implementations.Private.Helpers.StringHelper;
using static Geolocation.Implementations.Private.Helpers.LocationHelper;
using static Geolocation.Implementations.Private.Helpers.IpAddressHelper;


namespace Geolocation.Implementations
{
    /// <summary>
    /// Сервис поиска местоположений.
    /// 
    /// Ключевым моментом, позволяющим загружать базу в память не дольше 50 мс, является применение unsafe-кода.
    /// Это дает возможность тратить время только на непосредственную загрузку бинарных данных как есть с диска в память,
    /// не занимаясь разбором и перекладыванием этих данных в разнообразные структуры и классы.
    /// </summary>
    unsafe public class LocationService : ILocationService
    {
        public LocationService(IDatabaseLoader databaseLoader)
        {
            database = databaseLoader.LoadDatabase();

            fixed (byte* pByte = database)
            {
                var header = (Header*)pByte;

                DatabaseVersion = header->Version;
                DatabaseName = CreateString(header->Name, Constants.Header.NameSize);
                DatabaseTimestamp = header->Timestamp;
            }
        }

        public int DatabaseVersion { get; private set; }
        public string DatabaseName { get; private set; }
        public ulong DatabaseTimestamp { get; private set; }

        public DTO.Location FindLocationByIp(string ipString)
        {
            var ip = ConvertIpToUInt32(ipString);

            fixed (byte* pByte = database)
            {
                var header = (Header*)pByte;

                var pFirstIpRange = (IpRange*)(pByte + header->IpRangesOffset);          // первая запись интервала IP адресов
                var pLastIpRange = pFirstIpRange + header->RecordsCount - 1;             // последняя запись интервала IP адресов

                var foundIpRange = IpRangeFinder.Find(ip, pFirstIpRange, pLastIpRange);
                if (foundIpRange != null)
                {
                    var pFirstLocation = (Location*)(pByte + header->LocationsOffset);   // первая запись местоположения

                    return CreateLocation(pFirstLocation + foundIpRange->LocationIndex); // искомая запись местоположения
                }
            }

            return null;
        }

        public DTO.Location[] FindLocationsByCity(string city)
        {
            fixed (byte* pByte = database)
            {
                var header = (Header*)pByte;

                var pFirstLocation = (Location*)(pByte + header->LocationsOffset);       // первая запись местоположения

                var pFirstIndex = (uint*)(pByte + header->IndexesOffset);                // первый индекс записей местоположений
                var pLastIndex = pFirstIndex + header->RecordsCount - 1;                 // последний индекс записей местоположений

                var foundIndexRange = IndexesByCityFinder.Find(city, pFirstLocation, pFirstIndex, pLastIndex);
                if (foundIndexRange != null)
                {
                    return CreateLocations(pFirstLocation, foundIndexRange.StartIndex, foundIndexRange.EndIndex);
                }
            }

            return null;
        }

        #region Private

        private readonly byte[] database;

        #endregion Private
    }
}
