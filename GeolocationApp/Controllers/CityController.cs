using Geolocation;
using Geolocation.DTO;
using System.Net;
using System.Web.Http;

namespace GeolocationApp.Controllers
{
    public class CityController : ApiController
    {
        public CityController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        [HttpGet]
        public Location[] Locations(string city)
        {
            var locations = locationService.FindLocationsByCity(city);
            if (locations != null)
            {
                return locations;
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        #region Private

        private readonly ILocationService locationService;

        #endregion Private
    }
}
