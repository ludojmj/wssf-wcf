using System.Configuration;
using System.Web.Configuration;

namespace WCFTemplate.Tools
{
    public static class Utils
    {
        public static readonly string AuthorizedGroup = ConfigurationManager.AppSettings["AuthorizedGroup"];
        public static bool IsDebug
        {
            get
            {
                var compilationSection = ConfigurationManager.GetSection(@"system.web/compilation") as CompilationSection;
                if (compilationSection != null && compilationSection.Debug)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
