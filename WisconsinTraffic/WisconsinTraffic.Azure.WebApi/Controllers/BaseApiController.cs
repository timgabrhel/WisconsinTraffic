using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using Microsoft.WindowsAzure.Mobile.Service;
using Newtonsoft.Json;
using WisconsinTraffic.Azure.WebApi.DocumentDb;
using WisconsinTraffic.Azure.WebApi.Models;
using WisconsinTraffic.Azure.WebApi.Utility;
using WITraffic511.Api.Models;

namespace WisconsinTraffic.Azure.WebApi.Controllers
{
    public class BaseApiController<T> : DocumentController<TrafficDocument<T>>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            string WITrafficDocumentDbName;
            if (!(Services.Settings.TryGetValue("WITrafficDocumentDbName", out WITrafficDocumentDbName)))
            {
                Services.Log.Error("Could not retrieve WITrafficDocumentDbName", category: LogCategories.Controllers);
                return;
            }

            string WITrafficDocumentDbCollectionName;
            if (!(Services.Settings.TryGetValue("WITrafficDocumentDbCollectionName", out WITrafficDocumentDbCollectionName)))
            {
                Services.Log.Error("Could not retrieve WITrafficDocumentDbCollectionName", category: LogCategories.Controllers);
                return;
            }

            DomainManager = new DocumentEntityDomainManager<TrafficDocument<T>>(WITrafficDocumentDbName, WITrafficDocumentDbCollectionName, Services);
        }
    }
}