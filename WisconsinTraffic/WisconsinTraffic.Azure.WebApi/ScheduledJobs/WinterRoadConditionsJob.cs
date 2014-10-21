using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WisconsinTraffic.Azure.WebApi.Controllers;
using WITraffic511.Api.Models;

namespace WisconsinTraffic.Azure.WebApi.ScheduledJobs
{
    public class WinterRoadConditionsJob : BaseScheduledJob<WinterRoadCondition>
    {
        public override async Task Execute()
        {
            var response = await ApiClient.GetWinterRoadConditionsAsync();
            await ProcessResponse(response, WinterRoadConditionsController.Identifier);
        }
    }
}