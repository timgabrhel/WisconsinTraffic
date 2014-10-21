using System.Threading.Tasks;
using WisconsinTraffic.Azure.WebApi.Controllers;
using WITraffic511.Api.Models;

namespace WisconsinTraffic.Azure.WebApi.ScheduledJobs
{
    public class MainlineRoutesJob : BaseScheduledJob<MainlineRoute>
    {
        public override async Task Execute()
        {
            var response = await ApiClient.GetMainlineRoutesAsync();
            await ProcessResponse(response, MainlineRoutesController.Identifier);
        }
    }
}