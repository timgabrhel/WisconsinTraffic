﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WITraffic511.Api.Models;

namespace WisconsinTraffic.Azure.WebApi.Controllers
{
    public class WinterRoadConditionsController : BaseApiController
    {
        public static string Identifier = "WinterRoadConditions";

        [AllowAnonymous]
        public async Task<HttpResponseMessage> Get()
        {
            try
            {
                var result = await GetTrafficResultAsync<WinterRoadCondition>(Identifier);
                return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                Services.Log.Error(ex, category: "WinterRoadConditionsController.Get()");
                return ControllerContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        } 
    }
}
