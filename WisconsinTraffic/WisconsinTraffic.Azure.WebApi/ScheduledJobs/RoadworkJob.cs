using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WisconsinTraffic.Azure.WebApi.Controllers;

namespace WisconsinTraffic.Azure.WebApi.ScheduledJobs
{
    public class RoadworkJob : BaseScheduledJob
    {
        public override async Task Execute()
        {
            var response = await ApiClient.GetRoadworkAsync();
            await ProcessResponse(response, RoadworkController.Identifier);
        }
    }
}