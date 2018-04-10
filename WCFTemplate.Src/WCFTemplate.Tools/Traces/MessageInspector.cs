using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Xml.Linq;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace WCFTemplate.Tools.Traces
{
    public class MessageInspector : IDispatchMessageInspector
    {
        private readonly TelemetryClient _telemetry = new TelemetryClient();

        // Request
        object IDispatchMessageInspector.AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            string url = request.Headers.To == null ? string.Empty : request.Headers.To.AbsoluteUri;
            string operation = request.Headers.Action == null ? null : request.Headers.Action.Substring(request.Headers.Action.LastIndexOf("/", StringComparison.Ordinal) + 1);
            string endUser = WebOperationContext.Current == null || string.IsNullOrEmpty(WebOperationContext.Current.IncomingRequest.Headers.Get("END_USER")) ? "NoEndUser" : WebOperationContext.Current.IncomingRequest.Headers.Get("END_USER");
            string wsConsummer = ServiceSecurityContext.Current == null ? "NoAuth" : ServiceSecurityContext.Current.PrimaryIdentity.Name;

            var sw = new Stopwatch();
            sw.Start();
            var result = new CorrelReqResp
            {
                Url = new Uri(url),
                Name = operation,
                AppClient = endUser,
                WsClient = wsConsummer,
                Id = Guid.NewGuid().ToString(),
                Chrono = sw,
                Request = GetParameters(request)
            };
            return result;
        }

        // Response
        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            var requestInfo = (CorrelReqResp)correlationState;
            requestInfo.Chrono.Stop();
            
            var result = new RequestTelemetry
            {
                Context = { Operation = { Name = requestInfo.Name } },
                Url = requestInfo.Url,
                Timestamp = DateTimeOffset.Now,
                Name = requestInfo.Name + "Request",
                Properties =
                {
                    { "AppClient", requestInfo.AppClient },
                    { "WsClient", requestInfo.WsClient },
                    { "Request", requestInfo.Request },
                    { "Response", GetParameters(reply) }
                },
                Duration = requestInfo.Chrono.Elapsed,
                Id = requestInfo.Id,
                ResponseCode = reply.IsFault ? "KO" : "OK",
                Success = !reply.IsFault,
                Source = requestInfo.WsClient
            };
            _telemetry.TrackRequest(result);
        }

        private static string GetParameters(Message requestResponse)
        {
            var message = requestResponse.ToString();
            if (string.IsNullOrWhiteSpace(message) || message.Contains("wsdl"))
            {
                return string.Empty;
            }

            XDocument xDoc;
            try
            {
                xDoc = XDocument.Parse(message);
            }
            catch (Exception)
            {
                throw new CommunicationException("Error in deserializing body of request message : request format is wrong.");
            }

            xDoc.DescendantNodes().OfType<XComment>().Remove();
            var descendants = xDoc.Descendants((XNamespace)"http://schemas.xmlsoap.org/soap/envelope/" + "Body").ToList();
            if (!descendants.Any())
            {
                return null;
            }
            XNode extract = descendants.First().FirstNode;
            string result = Environment.NewLine + extract;
            return result;
        }
    }
}
