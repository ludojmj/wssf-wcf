//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using WcfSerialization = global::System.Runtime.Serialization;

namespace WCFTemplate.FaultContracts
{
	/// <summary>
	/// Data Contract Class - BusinessError
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://schemas.wcftemplate.jmj/wcf/template/1", Name = "BusinessError")]
	public partial class BusinessError 
	{
		private string message;
		
		[WcfSerialization::DataMember(Name = "Message", IsRequired = false, Order = 0)]
		public string Message
		{
		  get { return message; }
		  set { message = value; }
		}				
	}
}

