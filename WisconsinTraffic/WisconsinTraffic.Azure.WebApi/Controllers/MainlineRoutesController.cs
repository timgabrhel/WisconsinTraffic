using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.UI;
using Microsoft.WindowsAzure.Mobile.Service;
using Newtonsoft.Json;
using WisconsinTraffic.Azure.WebApi.Models;
using WisconsinTraffic.Azure.WebApi.Utility;
using WITraffic511.Api.Models;

namespace WisconsinTraffic.Azure.WebApi.Controllers
{
    public class MainlineRoutesController : BaseApiController<MainlineRoute>
    {
        public static string Identifier = "MainlineRoutes";

        [AllowAnonymous]
        public HttpResponseMessage Get()
        {
            var doc = Lookup(Identifier).Queryable.AsEnumerable().FirstOrDefault();
            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new TrafficResult<MainlineRoute>(doc));
        }
    }
}
