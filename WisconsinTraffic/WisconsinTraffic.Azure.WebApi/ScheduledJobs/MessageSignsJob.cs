using System.Threading.Tasks;
using WisconsinTraffic.Azure.WebApi.Controllers;
using WITraffic511.Api.Models;

namespace WisconsinTraffic.Azure.WebApi.ScheduledJobs
{
    public class MessageSignsJob : BaseScheduledJob<MessageSign>
    {
        public override async Task Execute()
        {
            var response = await ApiClient.GetMessageSignsAsync();
            await ProcessResponse(response, MessageSignsController.Identifier);
        }
    }
}