//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a �t� g�n�r� par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apport�es � ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est r�g�n�r�.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WCFTemplate.DataContracts {
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.2053.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.wcftemplate.jmj/wcf/template/1")]
    [System.Xml.Serialization.XmlRootAttribute("OperationResponse", Namespace="http://services.wcftemplate.jmj/wcf/template/1", IsNullable=false)]
    public partial class OperationOut {
        
        private string outputField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Output {
            get {
                return this.outputField;
            }
            set {
                this.outputField = value;
            }
        }
    }
}
