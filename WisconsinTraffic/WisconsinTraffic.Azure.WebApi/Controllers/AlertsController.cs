using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WisconsinTraffic.Azure.WebApi.Models;
using WITraffic511.Api.Models;

namespace WisconsinTraffic.Azure.WebApi.Controllers
{
    public class AlertsController : BaseApiController<Alert>
    {
        public static string Identifier = "Alerts";

        [AllowAnonymous]
        public HttpResponseMessage Get()
        {
            var doc = Lookup(Identifier).Queryable.AsEnumerable().FirstOrDefault();
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new TrafficResult<Alert>(doc));
        }
    }
}
