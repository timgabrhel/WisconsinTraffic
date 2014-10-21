using System.Threading.Tasks;
using WisconsinTraffic.Azure.WebApi.Controllers;
using WITraffic511.Api.Models;

namespace WisconsinTraffic.Azure.WebApi.ScheduledJobs
{
    public class MainlineLinksJob : BaseScheduledJob<MainlineLink>
    {
        public override async Task Execute()
        {
            var response = await ApiClient.GetMainlineLinksAsync();
            await ProcessResponse(response, MainlineLinksController.Identifier);
        }
    }
}