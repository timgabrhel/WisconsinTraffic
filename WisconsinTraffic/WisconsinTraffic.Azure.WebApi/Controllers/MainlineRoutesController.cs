using System;
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
    public class MainlineRoutesController : BaseApiController
    {
        public const string MainlineRoutesDocName = "docMainlineRoutes";
        
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                var result = await GetTrafficResultAsync<MainlineRoute>(MainlineRoutesDocName);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Services.Log.Error(ex, category: "MainlineRoutesController.Get()");
                return ControllerContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        } 
    }
}
