using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using WisconsinTraffic.Azure.WebApi.Models;
using Microsoft.WindowsAzure.Mobile.Service;
using WisconsinTraffic.Azure.WebApi.Utility;

namespace WisconsinTraffic.Azure.WebApi
{
    [AllowAnonymous]
    public static class WebApiConfig
    {
        public static void Register()
        {
            // Use this class to set configuration options for your mobile service
            ConfigOptions options = new ConfigOptions();
            
            // Use this class to set WebAPI configuration options
            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));
            config.Formatters.Add(new BrowserJsonFormatter());
            
            // To display errors in the browser during development, uncomment the following
            // line. Comment it out again when you deploy your service for production use.
            // config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
        }
    }

    public class BrowserJsonFormatter : JsonMediaTypeFormatter
    {
        public BrowserJsonFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings() {Formatting = Formatting.Indented};
        }

        public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        {
            base.SetDefaultContentHeaders(type, headers, mediaType);
            headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
    }
}

