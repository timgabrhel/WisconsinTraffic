using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortableRest;
using WITraffic511.Api.Models;

namespace WITraffic511.Api
{
    public interface ITrafficClient
    {
        Task<RestResponse<List<MainlineRoute>>> GetMainlineRoutesAsync();

        Task<RestResponse<List<Incident>>> GetIncidentsAsync();

        Task<RestResponse<List<MainlineLink>>> GetMainlineLinksAsync();

        Task<RestResponse<List<WinterRoadCondition>>> GetWinterRoadConditionsAsync();

        Task<RestResponse<List<Camera>>> GetCamerasAsync();

        Task<RestResponse<List<Roadway>>> GetRoadwaysAsync();

        Task<RestResponse<List<Roadwork>>> GetRoadworkAsync();

        Task<RestResponse<List<MessageSign>>> GetMessageSignsAsync();

        Task<RestResponse<List<Alert>>> GetAlertsAsync();
    }
}
