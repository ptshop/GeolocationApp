using Geolocation.DTO;

namespace Geolocation
{
    public interface ILocationService
    {
        Location FindLocationByIp(string ip);
        Location[] FindLocationsByCity(string city);
    }
}
