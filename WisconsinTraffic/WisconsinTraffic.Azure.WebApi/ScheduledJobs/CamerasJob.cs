using System.Threading.Tasks;
using WisconsinTraffic.Azure.WebApi.Controllers;
using WITraffic511.Api.Models;

namespace WisconsinTraffic.Azure.WebApi.ScheduledJobs
{
    public class CamerasJob : BaseScheduledJob<Camera>
    {
        public override async Task Execute()
        {
            var response = await ApiClient.GetCamerasAsync();
            await ProcessResponse(response, CamerasController.Identifier);
        }
    }
}