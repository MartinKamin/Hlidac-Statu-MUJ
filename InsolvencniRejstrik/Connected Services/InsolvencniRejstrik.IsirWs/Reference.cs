﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InsolvencniRejstrik.IsirWs
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://isirpublicws.cca.cz/", ConfigurationName="InsolvencniRejstrik.IsirWs.IsirWsPublicPortType")]
    public interface IsirWsPublicPortType
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://isirpublicws.cca.cz/IsirWsPublicPortType/getIsirWsPublicPodnetIdRequest", ReplyAction="http://isirpublicws.cca.cz/IsirWsPublicPortType/getIsirWsPublicPodnetIdResponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        InsolvencniRejstrik.IsirWs.getIsirWsPublicPodnetIdResponse getIsirWsPublicPodnetId(InsolvencniRejstrik.IsirWs.getIsirWsPublicPodnetIdRequest request);
        
        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action="http://isirpublicws.cca.cz/IsirWsPublicPortType/getIsirWsPublicPodnetIdRequest", ReplyAction="http://isirpublicws.cca.cz/IsirWsPublicPortType/getIsirWsPublicPodnetIdResponse")]
        System.Threading.Tasks.Task<InsolvencniRejstrik.IsirWs.getIsirWsPublicPodnetIdResponse> getIsirWsPublicPodnetIdAsync(InsolvencniRejstrik.IsirWs.getIsirWsPublicPodnetIdRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://isirpublicws.cca.cz/IsirWsPublicPortType/getIsirWsPublicPodnetPosledniIdRe" +
            "quest", ReplyAction="http://isirpublicws.cca.cz/IsirWsPublicPortType/getIsirWsPublicPodnetPosledniIdRe" +
            "sponse")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        InsolvencniRejstrik.IsirWs.getIsirWsPublicPodnetPosledniIdResponse getIsirWsPublicPodnetPosledniId(InsolvencniRejstrik.IsirWs.getIsirWsPublicPodnetPosledniIdRequest request);
        
        // CODEGEN: Generating message contract since the operation has multiple return values.
        [System.ServiceModel.OperationContractAttribute(Action="http://isirpublicws.cca.cz/IsirWsPublicPortType/getIsirWsPublicPodnetPosledniIdRe" +
            "quest", ReplyAction="http://isirpublicws.cca.cz/IsirWsPublicPortType/getIsirWsPublicPodnetPosledniIdRe" +
            "sponse")]
        System.Threading.Tasks.Task<InsolvencniRejstrik.IsirWs.getIsirWsPublicPodnetPosledniIdResponse> getIsirWsPublicPodnetPosledniIdAsync(InsolvencniRejstrik.IsirWs.getIsirWsPublicPodnetPosledniIdRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://isirpublicws.cca.cz/types/")]
    public partial class isirWsPublicData
    {
        
        private long idField;
        
        private System.DateTime datumZalozeniUdalostiField;
        
        private System.DateTime datumZverejneniUdalostiField;
        
        private string dokumentUrlField;
        
        private string spisovaZnackaField;
        
        private string typUdalostiField;
        
        private string popisUdalostiField;
        
        private string oddilField;
        
        private int cisloVOddiluField;
        
        private bool cisloVOddiluFieldSpecified;
        
        private string poznamkaField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public long id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public System.DateTime datumZalozeniUdalosti
        {
            get
            {
                return this.datumZalozeniUdalostiField;
            }
            set
            {
                this.datumZalozeniUdalostiField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public System.DateTime datumZverejneniUdalosti
        {
            get
            {
                return this.datumZverejneniUdalostiField;
            }
            set
            {
                this.datumZverejneniUdalostiField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=3)]
        public string dokumentUrl
        {
            get
            {
                return this.dokumentUrlField;
            }
            set
            {
                this.dokumentUrlField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=4)]
        public string spisovaZnacka
        {
            get
            {
                return this.spisovaZnackaField;
            }
            set
            {
                this.spisovaZnackaField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=5)]
        public string typUdalosti
        {
            get
            {
                return this.typUdalostiField;
            }
            set
            {
                this.typUdalostiField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=6)]
        public string popisUdalosti
        {
            get
            {
                return this.popisUdalostiField;
            }
            set
            {
                this.popisUdalostiField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=7)]
        public string oddil
        {
            get
            {
                return this.oddilField;
            }
            set
            {
                this.oddilField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=8)]
        public int cisloVOddilu
        {
            get
            {
                return this.cisloVOddiluField;
            }
            set
            {
                this.cisloVOddiluField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool cisloVOddiluSpecified
        {
            get
            {
                return this.cisloVOddiluFieldSpecified;
            }
            set
            {
                this.cisloVOddiluFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=9)]
        public string poznamka
        {
            get
            {
                return this.poznamkaField;
            }
            set
            {
                this.poznamkaField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://isirpublicws.cca.cz/types/")]
    public partial class isirWsPublicStatus
    {
        
        private stavType stavField;
        
        private kodChybyType kodChybyField;
        
        private bool kodChybyFieldSpecified;
        
        private string popisChybyField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=0)]
        public stavType stav
        {
            get
            {
                return this.stavField;
            }
            set
            {
                this.stavField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public kodChybyType kodChyby
        {
            get
            {
                return this.kodChybyField;
            }
            set
            {
                this.kodChybyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool kodChybySpecified
        {
            get
            {
                return this.kodChybyFieldSpecified;
            }
            set
            {
                this.kodChybyFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=2)]
        public string popisChyby
        {
            get
            {
                return this.popisChybyField;
            }
            set
            {
                this.popisChybyField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://isirpublicws.cca.cz/types/")]
    public enum stavType
    {
        
        /// <remarks/>
        OK,
        
        /// <remarks/>
        CHYBA,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://isirpublicws.cca.cz/types/")]
    public enum kodChybyType
    {
        
        /// <remarks/>
        WS1,
        
        /// <remarks/>
        WS2,
        
        /// <remarks/>
        SERVER1,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getIsirWsPublicIdDataRequest", WrapperNamespace="http://isirpublicws.cca.cz/types/", IsWrapped=true)]
    public partial class getIsirWsPublicPodnetIdRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://isirpublicws.cca.cz/types/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public long idPodnetu;
        
        public getIsirWsPublicPodnetIdRequest()
        {
        }
        
        public getIsirWsPublicPodnetIdRequest(long idPodnetu)
        {
            this.idPodnetu = idPodnetu;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getIsirWsPublicDataResponse", WrapperNamespace="http://isirpublicws.cca.cz/types/", IsWrapped=true)]
    public partial class getIsirWsPublicPodnetIdResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://isirpublicws.cca.cz/types/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("data", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public InsolvencniRejstrik.IsirWs.isirWsPublicData[] data;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://isirpublicws.cca.cz/types/", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public InsolvencniRejstrik.IsirWs.isirWsPublicStatus status;
        
        public getIsirWsPublicPodnetIdResponse()
        {
        }
        
        public getIsirWsPublicPodnetIdResponse(InsolvencniRejstrik.IsirWs.isirWsPublicData[] data, InsolvencniRejstrik.IsirWs.isirWsPublicStatus status)
        {
            this.data = data;
            this.status = status;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getIsirWsPublicPosledniIdDataRequest", WrapperNamespace="http://isirpublicws.cca.cz/types/", IsWrapped=true)]
    public partial class getIsirWsPublicPodnetPosledniIdRequest
    {
        
        public getIsirWsPublicPodnetPosledniIdRequest()
        {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getIsirWsPublicPosledniDataResponse", WrapperNamespace="http://isirpublicws.cca.cz/types/", IsWrapped=true)]
    public partial class getIsirWsPublicPodnetPosledniIdResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://isirpublicws.cca.cz/types/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute("cisloPosledniId", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=true)]
        public System.Nullable<long>[] cisloPosledniId;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://isirpublicws.cca.cz/types/", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public InsolvencniRejstrik.IsirWs.isirWsPublicStatus status;
        
        public getIsirWsPublicPodnetPosledniIdResponse()
        {
        }
        
        public getIsirWsPublicPodnetPosledniIdResponse(System.Nullable<long>[] cisloPosledniId, InsolvencniRejstrik.IsirWs.isirWsPublicStatus status)
        {
            this.cisloPosledniId = cisloPosledniId;
            this.status = status;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public interface IsirWsPublicPortTypeChannel : InsolvencniRejstrik.IsirWs.IsirWsPublicPortType, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public partial class IsirWsPublicPortTypeClient : System.ServiceModel.ClientBase<InsolvencniRejstrik.IsirWs.IsirWsPublicPortType>, InsolvencniRejstrik.IsirWs.IsirWsPublicPortType
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public IsirWsPublicPortTypeClient() : 
                base(IsirWsPublicPortTypeClient.GetDefaultBinding(), IsirWsPublicPortTypeClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.IsirWsPublicPortType.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public IsirWsPublicPortTypeClient(EndpointConfiguration endpointConfiguration) : 
                base(IsirWsPublicPortTypeClient.GetBindingForEndpoint(endpointConfiguration), IsirWsPublicPortTypeClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public IsirWsPublicPortTypeClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(IsirWsPublicPortTypeClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public IsirWsPublicPortTypeClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(IsirWsPublicPortTypeClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public IsirWsPublicPortTypeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        InsolvencniRejstrik.IsirWs.getIsirWsPublicPodnetIdResponse InsolvencniRejstrik.IsirWs.IsirWsPublicPortType.getIsirWsPublicPodnetId(InsolvencniRejstrik.IsirWs.getIsirWsPublicPodnetIdRequest request)
        {
            return base.Channel.getIsirWsPublicPodnetId(request);
        }
        
        public InsolvencniRejstrik.IsirWs.isirWsPublicData[] getIsirWsPublicPodnetId(long idPodnetu, out InsolvencniRejstrik.IsirWs.isirWsPublicStatus status)
        {
            InsolvencniRejstrik.IsirWs.getIsirWsPublicPodnetIdRequest inValue = new InsolvencniRejstrik.IsirWs.getIsirWsPublicPodnetIdRequest();
            inValue.idPodnetu = idPodnetu;
            InsolvencniRejstrik.IsirWs.getIsirWsPublicPodnetIdResponse retVal = ((InsolvencniRejstrik.IsirWs.IsirWsPublicPortType)(this)).getIsirWsPublicPodnetId(inValue);
            status = retVal.status;
            return retVal.data;
        }
        
        public System.Threading.Tasks.Task<InsolvencniRejstrik.IsirWs.getIsirWsPublicPodnetIdResponse> getIsirWsPublicPodnetIdAsync(InsolvencniRejstrik.IsirWs.getIsirWsPublicPodnetIdRequest request)
        {
            return base.Channel.getIsirWsPublicPodnetIdAsync(request);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        InsolvencniRejstrik.IsirWs.getIsirWsPublicPodnetPosledniIdResponse InsolvencniRejstrik.IsirWs.IsirWsPublicPortType.getIsirWsPublicPodnetPosledniId(InsolvencniRejstrik.IsirWs.getIsirWsPublicPodnetPosledniIdRequest request)
        {
            return base.Channel.getIsirWsPublicPodnetPosledniId(request);
        }
        
        public System.Nullable<long>[] getIsirWsPublicPodnetPosledniId(out InsolvencniRejstrik.IsirWs.isirWsPublicStatus status)
        {
            InsolvencniRejstrik.IsirWs.getIsirWsPublicPodnetPosledniIdRequest inValue = new InsolvencniRejstrik.IsirWs.getIsirWsPublicPodnetPosledniIdRequest();
            InsolvencniRejstrik.IsirWs.getIsirWsPublicPodnetPosledniIdResponse retVal = ((InsolvencniRejstrik.IsirWs.IsirWsPublicPortType)(this)).getIsirWsPublicPodnetPosledniId(inValue);
            status = retVal.status;
            return retVal.cisloPosledniId;
        }
        
        public System.Threading.Tasks.Task<InsolvencniRejstrik.IsirWs.getIsirWsPublicPodnetPosledniIdResponse> getIsirWsPublicPodnetPosledniIdAsync(InsolvencniRejstrik.IsirWs.getIsirWsPublicPodnetPosledniIdRequest request)
        {
            return base.Channel.getIsirWsPublicPodnetPosledniIdAsync(request);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.IsirWsPublicPortType))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.IsirWsPublicPortType))
            {
                return new System.ServiceModel.EndpointAddress("https://isir.justice.cz:8443/isir_public_ws/IsirWsPublicService");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return IsirWsPublicPortTypeClient.GetBindingForEndpoint(EndpointConfiguration.IsirWsPublicPortType);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return IsirWsPublicPortTypeClient.GetEndpointAddress(EndpointConfiguration.IsirWsPublicPortType);
        }
        
        public enum EndpointConfiguration
        {
            
            IsirWsPublicPortType,
        }
    }
}
