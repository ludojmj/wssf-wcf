using System.Security.Permissions;
using WCFTemplate.DataContracts;
using WCFTemplate.MessageContracts;
using WCFTemplate.Tools;
using WCFTemplate.Tools.Errors;

namespace WCFTemplate.ServiceImplementation
{
    public partial class ServiceTemplate
    {
        [ServicePermission(SecurityAction.Demand)]
        public override OperationResponse Operation(OperationRequest request)
        {
            var resp = new OperationResponse
            {
                OperationOut = new OperationOut
                {
                    Output = string.Format("Input: {0}", request.OperationIn.Input)
                }
            };
            return resp;
        }
        [ServicePermission(SecurityAction.Demand)]
        public override void BusinessMistake()
        {
            throw new BusinessException("Wrong parameter in WS SOAP.");
        }

        [ServicePermission(SecurityAction.Demand)]
        public override void TechnicalMistake()
        {
            int a = 0;
            int b = 10;
            int c = b / a;
        }
    }
}
