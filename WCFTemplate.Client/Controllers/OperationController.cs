using System;
using System.ServiceModel;
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
                    var resp = proxy.Operation(req.OperationIn);
                    return Ok(resp.Output);
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
