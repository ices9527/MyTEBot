using System.Web.Routing;

using MyTEBot.Service.App_Start;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(RegisterRebarWebHealthCheck), "PostApplicationStart")]

namespace MyTEBot.Service.App_Start
{
    /// <summary>
    /// Registers the REBAR HealthCheck and Version HttpHandlers.
    /// Code goes in here instead of directly into <c>global.asax</c> to support
    /// NuGet packaging.
    /// </summary>
    public static class RegisterRebarWebHealthCheck
    {
        /// <summary>
        /// Calls made after application start.
        /// </summary>
        public static void PostApplicationStart()
        {
            var healthCheckHandler = new HealthCheckRouteHandler("MyTEBot-service", true);
            RouteTable.Routes.Insert(
                0, new Route("MyTEBot-service/HealthCheck/{*all}", healthCheckHandler));

            RouteTable.Routes.Insert(0, new Route("MyTEBot-service/Version/{*all}", new VersionRouteHandler()));
        }
    }
}