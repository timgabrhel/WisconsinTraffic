using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WisconsinTraffic.Azure.WebApi.Controllers;
using WITraffic511.Api.Models;

namespace WisconsinTraffic.Azure.WebApi.ScheduledJobs
{
    public class AlertsJob : BaseScheduledJob<Alert>
    {
        public override async Task Execute()
        {
            var response = await ApiClient.GetAlertsAsync();
            await ProcessResponse(response, AlertsController.Identifier);
        }
    }
}