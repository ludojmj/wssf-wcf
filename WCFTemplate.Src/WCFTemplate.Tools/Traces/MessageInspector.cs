using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using log4net;

namespace WCFTemplate.Tools.Traces
{
    public class MessageInspector : IDispatchMessageInspector
    {
        private const string ConfigTraces = "Traces";

        private static readonly ILog Logg = LogManager.GetLogger(ConfigTraces);

        // Tracing request
        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            string operation = request.Headers.Action == null ? null : request.Headers.Action.Substring(request.Headers.Action.LastIndexOf("/", StringComparison.Ordinal) + 1);
            string endUser = WebOperationContext.Current == null || string.IsNullOrEmpty(WebOperationContext.Current.IncomingRequest.Headers.Get("END_USER")) ? "NoEndUser" : WebOperationContext.Current.IncomingRequest.Headers.Get("END_USER");
            string wsConsummer = ServiceSecurityContext.Current == null ? "NoAuth" : ServiceSecurityContext.Current.PrimaryIdentity.Name;

            var correlObj = new CorrelReqResp
            {
                Id = Guid.NewGuid(),
                Chrono = new Stopwatch(),
                ElapsedTime = 0,
                ElapsedTimeStr = "00:00.000000",
                AppClient = endUser,
                WsClient = wsConsummer,
                Requete = request.ToString(),
                Reponse = string.Empty,
                Status = "GO",
                Operation = operation,
                WebServiceMethod = operation + "Request"
            };

            if (operation == null || operation == "Get")
            {   // Not tracing WSDL
                return correlObj;
            }
            correlObj.Chrono.Start();
            TraceBuilder(correlObj);
            return correlObj;
        }

        // Tracing response
        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            var correlObj = (CorrelReqResp)correlationState;
            if (correlObj.Operation == null || correlObj.Operation == "Get")
            {   // Not tracing WSDL
                return;
            }

            correlObj.Chrono.Stop();
            correlObj.WebServiceMethod = correlObj.Operation + "Response";
            correlObj.Reponse = reply.ToString();
            correlObj.Status = reply.IsFault ? "KO" : "OK";
            TimeSpan ts = correlObj.Chrono.Elapsed;
            string elapsedTimeStr = string.Format("{0:00}:{1:00}.{2:000000}", ts.Minutes, ts.Seconds, ts.Milliseconds);
            correlObj.ElapsedTimeStr = elapsedTimeStr;
            correlObj.ElapsedTime = ts.Seconds;
            correlObj.Chrono = null;

            TraceBuilder(correlObj);
        }

        /// <summary>
        /// Building traces
        /// </summary>
        /// <param name="correlObj"></param>
        private static void TraceBuilder(CorrelReqResp correlObj)
        {
            ThreadContext.Properties["Chrono"] = correlObj.ElapsedTimeStr;
            ThreadContext.Properties["Guid"] = correlObj.Id;
            ThreadContext.Properties["AppClient"] = correlObj.AppClient;
            ThreadContext.Properties["WsClient"] = correlObj.WsClient;
            ThreadContext.Properties["Operation"] = correlObj.WebServiceMethod;
            ThreadContext.Properties["Status"] = correlObj.Status;
            Logg.Info(string.Empty);
        }

        /* Full XML Message
        private static string ExtractParametres(Message requestResponse)
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
                throw new CommunicationException("Erreur lors de la désérialisation du corps du message de demande : format de la requête invalide.");
            }

            xDoc.DescendantNodes().OfType<XComment>().Remove();
            var descendants = xDoc.Descendants((XNamespace)"http://schemas.xmlsoap.org/soap/envelope/" + "Body").ToList();
            if (!descendants.Any())
            {
                return null;
            }
            XNode extract = descendants.First().FirstNode;
            string result = Environment.NewLine + extract;
            return result.Replace("http://services.axa.fr/edr/payment/3", "");
        }
        */
    }
}
