using System.Threading.Tasks;
using WisconsinTraffic.Azure.WebApi.Controllers;
using WisconsinTraffic.Azure.WebApi.ScheduledJobs;

namespace WisconsinTraffic.Azure.WebApi.Models
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