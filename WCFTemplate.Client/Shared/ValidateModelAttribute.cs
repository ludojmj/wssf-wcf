using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WCFTemplate.Client.Shared
{
    // Cheking properties integrity
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid)
            {
                return;
            }
            var erreurs = actionContext.ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage);
            throw new BusinessException(string.Join("- ", erreurs));
        }
    }
}
