﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenWaitService.WebApplicationService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WebApplicationService.IWebAplicationService")]
    public interface IWebAplicationService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebAplicationService/ApproveFineshedProcessBywwfId", ReplyAction="http://tempuri.org/IWebAplicationService/ApproveFineshedProcessBywwfIdResponse")]
        void ApproveFineshedProcessBywwfId(System.Guid wwfId, string bookmark);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebAplicationService/ApproveFineshedProcessBywwfId", ReplyAction="http://tempuri.org/IWebAplicationService/ApproveFineshedProcessBywwfIdResponse")]
        System.Threading.Tasks.Task ApproveFineshedProcessBywwfIdAsync(System.Guid wwfId, string bookmark);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWebAplicationServiceChannel : OpenWaitService.WebApplicationService.IWebAplicationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WebAplicationServiceClient : System.ServiceModel.ClientBase<OpenWaitService.WebApplicationService.IWebAplicationService>, OpenWaitService.WebApplicationService.IWebAplicationService {
        
        public WebAplicationServiceClient() {
        }
        
        public WebAplicationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WebAplicationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebAplicationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebAplicationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void ApproveFineshedProcessBywwfId(System.Guid wwfId, string bookmark) {
            base.Channel.ApproveFineshedProcessBywwfId(wwfId, bookmark);
        }
        
        public System.Threading.Tasks.Task ApproveFineshedProcessBywwfIdAsync(System.Guid wwfId, string bookmark) {
            return base.Channel.ApproveFineshedProcessBywwfIdAsync(wwfId, bookmark);
        }
    }
}
