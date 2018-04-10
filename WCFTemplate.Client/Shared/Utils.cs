using System.Configuration;

namespace WCFTemplate.Client.Shared
{
    public static class Utils
    {
        public static readonly bool DisableTelemetry = bool.Parse(ConfigurationManager.AppSettings["applicationInsights:DisableTelemetry"]);
        public static readonly string InstrumentationKey = ConfigurationManager.AppSettings["applicationInsights:InstrumentationKeyString"];

        public static readonly string WsUser = ConfigurationManager.AppSettings["WsUser"];
        public static readonly string WsPass = ConfigurationManager.AppSettings["WsPass"];

        public static readonly string AuthCors = ConfigurationManager.AppSettings["AuthCors"];
    }
}
