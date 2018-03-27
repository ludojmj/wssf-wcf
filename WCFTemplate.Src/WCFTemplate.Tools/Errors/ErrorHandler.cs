using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using log4net;
using WCFTemplate.FaultContracts;

namespace WCFTemplate.Tools.Errors
{
    // Logging exceptions
    public class ErrorHandler : Attribute, IErrorHandler, IServiceBehavior
    {
        private const string ConfigErrors = "Errors";

        private static readonly ILog Logg = LogManager.GetLogger(ConfigErrors);

        public bool HandleError(Exception error)
        {
            BusinessException busErr = error as BusinessException;
            if (busErr == null)
            {   // Only logging unmanaged exceptions
                Logg.Error(error);
            }

            // Returning true indicates you performed your behavior.
            return true;
        }

        // Mapping Exceptions ==> Faults
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            string technicalMessage = "An error occured. Please try again later.";

            BusinessException busErr = error as BusinessException;
            FaultException faultException;
            if (busErr == null)
            {
#if DEBUG       // Clear message if debug on Technical Faults
                technicalMessage = error.Message;
#endif
                faultException = new FaultException<TechnicalError>(new TechnicalError { Message = technicalMessage }, new FaultReason("Technical Fault"));
            }
            else
            {   // Clear message on Business Faults
                faultException = new FaultException<BusinessError>(new BusinessError { Message = busErr.Message }, new FaultReason("Business Fault"));
            }
            MessageFault messaFault = faultException.CreateMessageFault();
            fault = Message.CreateMessage(version, messaFault, faultException.Action);
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
        }
        
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            IErrorHandler errorHandler = new ErrorHandler();
            foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                var channelDispatcher = channelDispatcherBase as ChannelDispatcher;
                if (channelDispatcher == null)
                {
                    continue;
                }
                channelDispatcher.ErrorHandlers.Add(errorHandler);
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }
}
