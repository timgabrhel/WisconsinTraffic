using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WisconsinTraffic.Azure.WebApi.Controllers;

namespace WisconsinTraffic.Azure.WebApi.ScheduledJobs
{
    public class RoadwaysJob : BaseScheduledJob
    {
        public override async Task Execute()
        {
            var response = await ApiClient.GetRoadwaysAsync();
            await ProcessResponse(response, RoadwaysController.Identifier);
        }
    }
}