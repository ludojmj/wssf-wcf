using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace WCFTemplate.Client.Shared
{
    public class ConflictActionResult : IHttpActionResult
    {
        private readonly string _message;

        public ConflictActionResult(string message)
        {
            _message = message;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new ObjectContent<ErrorContent>(new ErrorContent
                    {
                        Message = "An error occured.",
                        MessageDetail = _message
                    },
                    new JsonMediaTypeFormatter(),
                    JsonMediaTypeFormatter.DefaultMediaType.MediaType)
            };
            return Task.FromResult(response);
        }
    }
}