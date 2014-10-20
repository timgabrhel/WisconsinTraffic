using System.Threading.Tasks;
using WisconsinTraffic.Azure.WebApi.Controllers;

namespace WisconsinTraffic.Azure.WebApi.ScheduledJobs
{
    public class MainlineRoutesJob : BaseScheduledJob
    {
        public override async Task Execute()
        {
            var response = await ApiClient.GetMainlineRoutesAsync();
            await ProcessResponse(response, MainlineRoutesController.Identifier);
        }
    }
}