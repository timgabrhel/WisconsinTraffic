using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.WindowsAzure.Mobile.Service;
using Newtonsoft.Json;
using WisconsinTraffic.Azure.WebApi.Models;
using WisconsinTraffic.Azure.WebApi.Utility;

namespace WisconsinTraffic.Azure.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        public ApiServices Services { get; set; }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Services = new ApiServices(controllerContext.Configuration);

            base.Initialize(controllerContext);
        }

        protected async Task<TrafficResult<T>> GetTrafficResultAsync<T>(string docId)
        {
            using (var db = new TrafficDocumentProvider())
            {
                await db.Init();

                var result = new TrafficResult<T>();

                var obj = db.Get(docId);
                if (obj != null)
                {
                    var doc = JsonConvert.SerializeObject(obj);
                    result = JsonConvert.DeserializeObject<TrafficResult<T>>(doc);
                }

                return result;
            }
        }
    }
}