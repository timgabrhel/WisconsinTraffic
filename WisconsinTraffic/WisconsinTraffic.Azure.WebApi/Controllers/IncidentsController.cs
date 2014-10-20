using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WisconsinTraffic.Azure.WebApi.Models;
using WITraffic511.Api.Models;

namespace WisconsinTraffic.Azure.WebApi.Controllers
{
    public class IncidentsController : BaseApiController<Incident>
    {
        public static string Identifier = "Incidents";

        [AllowAnonymous]
        public HttpResponseMessage Get()
        {
            var doc = Lookup(Identifier).Queryable.AsEnumerable().FirstOrDefault();
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new TrafficResult<Incident>(doc));
        }
    }
}
