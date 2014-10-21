using System.Threading.Tasks;
using WisconsinTraffic.Azure.WebApi.Controllers;
using WITraffic511.Api.Models;

namespace WisconsinTraffic.Azure.WebApi.ScheduledJobs
{
    public class RoadwaysJob : BaseScheduledJob<Roadway>
    {
        public override async Task Execute()
        {
            var response = await ApiClient.GetRoadwaysAsync();
            await ProcessResponse(response, RoadwaysController.Identifier);
        }
    }
}