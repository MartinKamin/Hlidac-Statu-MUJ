﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.6.1055.0.
// 
namespace HlidacStatu.Entities.XSD {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://portal.gov.cz/rejstriky/ISRS/1.2/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://portal.gov.cz/rejstriky/ISRS/1.2/", IsNullable=false)]
    public partial class index {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("dump")]
        public indexDump[] dump;
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://portal.gov.cz/rejstriky/ISRS/1.2/")]
    public partial class indexDump {
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="integer")]
        public string mesic;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="integer")]
        public string rok;
        
        /// <remarks/>
        public tHash hashDumpu;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType="integer")]
        public string velikostDumpu;
        
        /// <remarks/>
        public System.DateTime casGenerovani;
        
        /// <remarks/>
        public bool dokoncenyMesic;
        
        /// <remarks/>
        public string odkaz;
    }
    
   
}
