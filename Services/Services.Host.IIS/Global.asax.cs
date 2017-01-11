using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

using Microsoft.Practices.EnterpriseLibrary.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Rebar.Service.ExceptionHandling;
using Rebar.Service.Handlers;
using Rebar.Telemetry.Data;
using Rebar.CacheAdapter.Core;
using Rebar.CacheAdapter.MemoryCache;
using Rebar.Telemetry.EF6;

namespace Services.Host.IIS
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            DataAccessProfiler.Initialize();
            Logger.SetLogWriter(new LogWriterFactory().Create(), true);

            var config = GlobalConfiguration.Configuration;
            IEnumerable<Func<DelegatingHandler>> perRouteServiceHandlers = new Func<DelegatingHandler>[] { () => new ClientIdHandler() };

            var formatters = config.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();


            MyTEBot.Service.Config.MyTEBotServiceRouteConfig.RegisterRoutes(config, perRouteServiceHandlers);

            // ServiceConfigPlaceHolder - DO NOT DELETE THIS LINE

            // Set the Global exception filter for 500 errors.
            // Requires configuraiton for each exception type for shielding. 
            config.Filters.Add(new ExceptionHandlingAttribute());

            Services.Host.IIS.App_Start.TracingConfig.Register(config);

            // This registers local memory cache
            // CacheGateway.Register<MemoryCacheAdapter>(cacheAdapter => new MemoryCacheAdapter());
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}
