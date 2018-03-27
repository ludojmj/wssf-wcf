using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Microsoft.ApplicationInsights.Extensibility;
using WCFTemplate.Client.Shared;
#if DEBUG
using System;
using System.Linq;
using System.Net.Http;
using Swashbuckle.Application;
#endif

namespace WCFTemplate.Client
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
#if DEBUG
            // Swagger
            config
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "ApiTemplate");
                    c.RootUrl(req => new Uri(req.RequestUri, req.GetRequestContext().VirtualPathRoot).ToString());
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                })
                .EnableSwaggerUi();
#endif

            // App Insights
            TelemetryConfiguration.Active.DisableTelemetry = Utils.DisableTelemetry;
            TelemetryConfiguration.Active.InstrumentationKey = Utils.InstrumentationKey;

            // Exceptions handling
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
            config.Services.Replace(typeof(IExceptionLogger), new GlobalExceptionLogger());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
