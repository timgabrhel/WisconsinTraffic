using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WisconsinTraffic.Azure.WebApi.Controllers;

namespace WisconsinTraffic.Azure.WebApi.ScheduledJobs
{
    public class MessageSignsJob : BaseScheduledJob
    {
        public override async Task Execute()
        {
            var response = await ApiClient.GetMessageSignsAsync();
            await ProcessResponse(response, MessageSignsController.Identifier);
        }
    }
}