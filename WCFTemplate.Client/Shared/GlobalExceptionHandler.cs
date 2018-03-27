using System;
using System.Text.RegularExpressions;
using System.Web.Http.ExceptionHandling;

namespace WCFTemplate.Client.Shared
{
    // Intercept exceptions so as to avoid us to try catch everywhere.
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            string msg = "An error occured. Please try again later.";
            var aggregateException = context.Exception as AggregateException;
            if (aggregateException != null)
            {
                aggregateException.Handle(ex =>
                {
                   // FaultException et TimeoutException
                    msg = ex.Message;
                    return true;
                });
            }

            var businessException = context.Exception as BusinessException;
            if (businessException != null)
            {
                // BusinessException = Managed exception (we can show the exception message to the user).
                msg = context.Exception.Message;
            }
#if DEBUG   // Unhandled exceptions
            msg = context.Exception.Message;
#endif

            msg = Regex.Replace(msg, @"\s+", " ").Replace("\r", " ").Replace("\n", "").Replace("\"", "");
            context.Result = new ConflictActionResult(msg);
        }
    }
}