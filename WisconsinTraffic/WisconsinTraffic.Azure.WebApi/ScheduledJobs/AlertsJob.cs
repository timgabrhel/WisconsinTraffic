using System.Threading.Tasks;
using WisconsinTraffic.Azure.WebApi.Controllers;

namespace WisconsinTraffic.Azure.WebApi.ScheduledJobs
{
    public class AlertsJob : BaseScheduledJob
    {
        public override async Task Execute()
        {
            var response = await ApiClient.GetAlertsAsync();
            //await ProcessResponse(response, AlertsController.Identifier);
        }
    }
}