using System;
using System.ServiceModel;
using System.Web.Http;
using WCFTemplate.Client.Models;
using WCFTemplate.Client.Shared;

namespace WCFTemplate.Client.Controllers
{
    public class WsBusinessErrorController : ApiController
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

                MistakeModel result = new MistakeModel();
                try
                {
                    proxy.BusinessMistake();
                    return Ok();
                }
                catch (TimeoutException error)
                {
                    result.BusinessError = string.Empty;
                    result.TechnicalError = error.Message;
                    return Ok(result);
                }
                catch (FaultException<BusinessError> error)
                {
                    result.BusinessError = error.Detail.Message;
                    result.TechnicalError = string.Empty;
                    return Ok(result);
                }
                catch (FaultException<TechnicalError> error)
                {
                    result.BusinessError = string.Empty;
                    result.TechnicalError = error.Detail.Message;
                    return Ok(result);
                }
                catch (FaultException error)
                {
                    result.BusinessError = string.Empty;
                    result.TechnicalError = error.Message;
                    return Ok(result);
                }
                catch (CommunicationException error)
                {
                    result.BusinessError = string.Empty;
                    result.TechnicalError = error.Message;
                    return Ok(result);
                }
            }
        }
    }
}
