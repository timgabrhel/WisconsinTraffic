using System.Threading.Tasks;
using WisconsinTraffic.Azure.WebApi.Controllers;
using WITraffic511.Api.Models;

namespace WisconsinTraffic.Azure.WebApi.ScheduledJobs
{
    public class RoadworkJob : BaseScheduledJob<Roadwork>
    {
        public override async Task Execute()
        {
            var response = await ApiClient.GetRoadworkAsync();
            await ProcessResponse(response, RoadworkController.Identifier);
        }
    }
}