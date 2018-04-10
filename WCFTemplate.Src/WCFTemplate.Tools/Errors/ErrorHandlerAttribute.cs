using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;

namespace WCFTemplate.Tools.Errors
{
    public class ExceptionHandlerAttribute : Attribute, IErrorHandler, IServiceBehavior
    {
        private readonly TelemetryClient _telemetry = new TelemetryClient();

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            FaultException faultException;

            BusinessException be = error as BusinessException;
            if (be != null || Utils.IsDebug)
            {
                faultException = new FaultException(error.Message);
            }
            else
            {
                faultException = new FaultException("An error has occurred, please try again later.");
            }

            MessageFault messageFault = faultException.CreateMessageFault();
            fault = Message.CreateMessage(version, messageFault, faultException.Action);
        }

        public bool HandleError(Exception error)
        {
            _telemetry.TrackException(error);
            return false;
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            TelemetryConfiguration.Active.DisableTelemetry = Utils.DisableTelemetry;
            TelemetryConfiguration.Active.InstrumentationKey = Utils.InstrumentationKey;
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher disp in serviceHostBase.ChannelDispatchers)
            {
                disp.ErrorHandlers.Add(this);
            }
        }
    }
}
