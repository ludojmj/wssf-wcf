//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using WCF = global::System.ServiceModel;

namespace WCFTemplate.ServiceImplementation
{	
	/// <summary>
	/// Service Class - ServiceTemplate
	/// </summary>
	[WCF::ServiceBehavior(Name = "ServiceTemplate", 
		Namespace = "http://services.wcftemplate.jmj/wcf/template/1", 
		InstanceContextMode = WCF::InstanceContextMode.PerCall, 
		ConcurrencyMode = WCF::ConcurrencyMode.Multiple )]
	public abstract class ServiceTemplateBase : WCFTemplate.ServiceContracts.IServiceTemplateContract
	{
		#region ServiceTemplateContract Members

		public virtual WCFTemplate.MessageContracts.OperationResponse Operation(WCFTemplate.MessageContracts.OperationRequest request)
		{
			return null;
		}

		public virtual void BusinessMistake()
		{
			
		}

		public virtual void TechnicalMistake()
		{
			
		}

		#endregion		
		
	}
	
	public partial class ServiceTemplate : ServiceTemplateBase
	{
	}
	
}

