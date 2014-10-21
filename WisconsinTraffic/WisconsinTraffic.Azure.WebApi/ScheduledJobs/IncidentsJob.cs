using System.Threading.Tasks;
using WisconsinTraffic.Azure.WebApi.Controllers;
using WITraffic511.Api.Models;

namespace WisconsinTraffic.Azure.WebApi.ScheduledJobs
{
    public class IncidentsJob : BaseScheduledJob<Incident>
    {
        public override async Task Execute()
        {
            var response = await ApiClient.GetIncidentsAsync();
            await ProcessResponse(response, IncidentsController.Identifier);
        }
    }
}