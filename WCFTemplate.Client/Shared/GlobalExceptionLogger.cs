using System.Web.Http.ExceptionHandling;
using Microsoft.ApplicationInsights;

namespace WCFTemplate.Client.Shared
{
    //  Unmanaged Exceptions Logging (for bugs)
    public class GlobalExceptionLogger : ExceptionLogger
    {
        private readonly TelemetryClient _telemetry = new TelemetryClient();

        public override void Log(ExceptionLoggerContext context)
        {
            var exception = context.Exception as BusinessException;
            if (exception == null)
            {
                _telemetry.TrackException(context.Exception);
            }
        }
    }
}