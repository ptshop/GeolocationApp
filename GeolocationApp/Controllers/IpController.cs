using Geolocation;
using Geolocation.DTO;
using System.Net;
using System.Web.Http;

namespace GeolocationApp.Controllers
{
    public class IpController : ApiController
    {
        public IpController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        [HttpGet]
        public Location Location(string ip)
        {
            var location = locationService.FindLocationByIp(ip);
            if (location != null)
            {
                return location;
            }

            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        #region Private

        private readonly ILocationService locationService;

        #endregion Private
    }
}
