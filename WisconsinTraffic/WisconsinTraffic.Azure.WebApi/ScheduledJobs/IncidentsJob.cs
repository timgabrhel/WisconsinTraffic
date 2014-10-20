using System.Threading.Tasks;
using WisconsinTraffic.Azure.WebApi.Controllers;

namespace WisconsinTraffic.Azure.WebApi.ScheduledJobs
{
    public class IncidentsJob : BaseScheduledJob
    {
        public override async Task Execute()
        {
            var response = await ApiClient.GetIncidentsAsync();
            await ProcessResponse(response, IncidentsController.Identifier);
        }
    }
}