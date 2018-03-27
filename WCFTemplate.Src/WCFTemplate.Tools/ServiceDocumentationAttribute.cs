using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Xml;

namespace WCFTemplate.Tools
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class ServiceDocumentationAttribute : DocumentationAttribute,
    IServiceBehavior,
    IContractBehavior,
    IOperationBehavior,
    IWsdlExportExtension
    {
        #region IServiceBehavior Members
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
        #endregion

        #region IContractBehavior Members
        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.DispatchRuntime dispatchRuntime)
        {
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
        }
        #endregion

        #region IOperationBehavior Members
        public void AddBindingParameters(OperationDescription description, System.ServiceModel.Channels.BindingParameterCollection parameters)
        {
        }

        public void ApplyClientBehavior(OperationDescription description, System.ServiceModel.Dispatcher.ClientOperation proxy)
        {
        }

        public void ApplyDispatchBehavior(OperationDescription description, System.ServiceModel.Dispatcher.DispatchOperation dispatch)
        {
        }

        public void Validate(OperationDescription description)
        {
        }
        #endregion

        #region IWsdlExportExtension Members
        public void ExportContract(WsdlExporter exporter, WsdlContractConversionContext context)
        {
        }

        public void ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context)
        {
            // [ServiceContract]
            var serviceAttribute = context.Endpoint.Contract.ContractType.GetCustomAttributes(typeof(ServiceDocumentationAttribute), false).FirstOrDefault() as ServiceDocumentationAttribute;
            if (serviceAttribute != null)
            {
                context.WsdlPort.Service.Documentation = string.Empty;
                serviceAttribute.ToXml(context.WsdlPort.Service.DocumentationElement);
            }
        #endregion
        }
    }

    // Documentation class for XSD
    [AttributeUsage(AttributeTargets.All)]
    public class DocumentationAttribute : Attribute
    {
        public string Description { get; set; }

        public string Version { get; set; }

        public void ToXml(XmlElement parent)
        {
            XmlDocument owner = parent.OwnerDocument;
            if (owner != null)
            {
                XmlElement descriptionElement = owner.CreateElement("description");
                descriptionElement.InnerText = Description;
                parent.AppendChild(descriptionElement);

                XmlElement versionElement = owner.CreateElement("version");
                versionElement.InnerText = Version;
                parent.AppendChild(versionElement);
            }
        }
    }
}
