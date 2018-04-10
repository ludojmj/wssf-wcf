using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web.Http;
using WCFTemplate.Client.Shared;

namespace WCFTemplate.Client.Controllers
{
    public class OperationController : ApiController
    {
        public IHttpActionResult Get()
        {
            using (var proxy = new ServiceTemplateContractClient())
            {
                if (proxy.ClientCredentials != null)
                {
                    // Basic Auth
                    proxy.ClientCredentials.UserName.UserName = Utils.WsUser;
                    proxy.ClientCredentials.UserName.Password = Utils.WsPass;
                }

                OperationRequest req = new OperationRequest
                {
                    OperationIn = new OperationIn
                    {
                        Input = "XXX YYY ZZZ"
                    }
                };

                try
                {
                    using (new OperationContextScope(proxy.InnerChannel))
                    {
                        if (WebOperationContext.Current != null)
                        {   // Tell the WS about the end user identity
                            var headerValues = Request.Headers.GetValues("X-EndUser");
                            var endUser = headerValues.FirstOrDefault();
                            WebOperationContext.Current.OutgoingRequest.Headers.Add("END_USER", endUser);
                        }
                        var resp = proxy.Operation(req.OperationIn);
                        return Ok(resp.Output);
                    }
                }
                catch (TimeoutException error)
                {
                    return BadRequest(error.Message);
                }
                catch (FaultException<BusinessError> error)
                {
                    return BadRequest(error.Detail.Message);
                }
                catch (FaultException<TechnicalError> error)
                {
                    return BadRequest(error.Detail.Message);
                }
                catch (FaultException error)
                {
                    return BadRequest(error.Message);
                }
                catch (CommunicationException error)
                {
                    return BadRequest(error.Message);
                }
            }
        }
    }
}
