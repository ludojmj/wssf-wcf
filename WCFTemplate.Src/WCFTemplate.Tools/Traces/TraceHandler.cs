using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WCFTemplate.Tools.Traces
{
    // Handling HTTP requests and responses to trace
    public class TraceHandler : BehaviorExtensionElement, IServiceBehavior
    {
        private readonly Type _behaviorType = typeof(TraceHandler);

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
        }

        public override Type BehaviorType
        {
            get { return _behaviorType; }
        }

        protected override object CreateBehavior()
        {
            return this;
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
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

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }
}