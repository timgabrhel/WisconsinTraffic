﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WisconsinTraffic.Azure.WebApi.Controllers;

namespace WisconsinTraffic.Azure.WebApi.ScheduledJobs
{
    public class MainlineLinksJob : BaseScheduledJob
    {
        public override async Task Execute()
        {
            var response = await ApiClient.GetMainlineLinksAsync();
            await ProcessResponse(response, MainlineLinksController.Identifier);
        }
    }
}