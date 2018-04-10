using System;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Microsoft.ApplicationInsights.Extensibility;

namespace WCFTemplate.Tools.Traces
{
    public class TraceHandlerAttribute : Attribute, IServiceBehavior
    {
        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            TelemetryConfiguration.Active.DisableTelemetry = Utils.DisableTelemetry;
            TelemetryConfiguration.Active.InstrumentationKey = Utils.InstrumentationKey;
        }

        public void AddBindingParameters(ServiceDescription serviceDescription,
            System.ServiceModel.ServiceHostBase serviceHostBase,
            System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints,
            System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription,
            System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                ChannelDispatcher channelDispatcher = channelDispatcherBase as ChannelDispatcher;
                if (channelDispatcher == null)
                {
                    continue;
                }
                foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
                {
                    var inspector = new MessageInspector();
                    endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector);
                }
            }
        }
    }
}
