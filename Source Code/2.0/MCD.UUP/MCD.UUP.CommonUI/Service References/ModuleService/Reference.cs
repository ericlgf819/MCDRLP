﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4952
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MCD.UUP.CommonUI.ModuleService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BaseEntity", Namespace="http://schemas.datacontract.org/2004/07/MCD.UUP.Entity")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(MCD.UUP.CommonUI.ModuleService.ModuleEntity))]
    public partial class BaseEntity : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TableNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TableName {
            get {
                return this.TableNameField;
            }
            set {
                if ((object.ReferenceEquals(this.TableNameField, value) != true)) {
                    this.TableNameField = value;
                    this.RaisePropertyChanged("TableName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ModuleEntity", Namespace="http://schemas.datacontract.org/2004/07/MCD.UUP.Entity")]
    [System.SerializableAttribute()]
    public partial class ModuleEntity : MCD.UUP.CommonUI.ModuleService.BaseEntity {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ModuleCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ModuleNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int SortIndexField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Guid SystemIDField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ModuleCode {
            get {
                return this.ModuleCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.ModuleCodeField, value) != true)) {
                    this.ModuleCodeField = value;
                    this.RaisePropertyChanged("ModuleCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ModuleName {
            get {
                return this.ModuleNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ModuleNameField, value) != true)) {
                    this.ModuleNameField = value;
                    this.RaisePropertyChanged("ModuleName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int SortIndex {
            get {
                return this.SortIndexField;
            }
            set {
                if ((this.SortIndexField.Equals(value) != true)) {
                    this.SortIndexField = value;
                    this.RaisePropertyChanged("SortIndex");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Guid SystemID {
            get {
                return this.SystemIDField;
            }
            set {
                if ((this.SystemIDField.Equals(value) != true)) {
                    this.SystemIDField = value;
                    this.RaisePropertyChanged("SystemID");
                }
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ModuleService.IModuleService")]
    public interface IModuleService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IModuleService/InsertModule", ReplyAction="http://tempuri.org/IModuleService/InsertModuleResponse")]
        bool InsertModule(MCD.UUP.CommonUI.ModuleService.ModuleEntity entity, System.Data.DataTable dtFunction);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IModuleService/UpdateModule", ReplyAction="http://tempuri.org/IModuleService/UpdateModuleResponse")]
        bool UpdateModule(MCD.UUP.CommonUI.ModuleService.ModuleEntity entity, System.Data.DataTable dtFunction);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IModuleService/DeleteModule", ReplyAction="http://tempuri.org/IModuleService/DeleteModuleResponse")]
        bool DeleteModule(MCD.UUP.CommonUI.ModuleService.ModuleEntity entity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IModuleService/GetSingleModule", ReplyAction="http://tempuri.org/IModuleService/GetSingleModuleResponse")]
        MCD.UUP.CommonUI.ModuleService.ModuleEntity GetSingleModule(MCD.UUP.CommonUI.ModuleService.ModuleEntity entity);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IModuleService/SelectModules", ReplyAction="http://tempuri.org/IModuleService/SelectModulesResponse")]
        System.Data.DataTable SelectModules(System.Guid systemID, string moduleName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IModuleService/SelectModulesBySystemCode", ReplyAction="http://tempuri.org/IModuleService/SelectModulesBySystemCodeResponse")]
        System.Data.DataTable SelectModulesBySystemCode(string systemCode, string moduleName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IModuleService/SelectModulesWithGroupBySystemCode", ReplyAction="http://tempuri.org/IModuleService/SelectModulesWithGroupBySystemCodeResponse")]
        System.Data.DataTable SelectModulesWithGroupBySystemCode(string systemCode, string moduleName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IModuleService/GetFunctionByModuleID", ReplyAction="http://tempuri.org/IModuleService/GetFunctionByModuleIDResponse")]
        System.Data.DataTable GetFunctionByModuleID(System.Guid moduleID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IModuleService/GetFunctionBySystemCode", ReplyAction="http://tempuri.org/IModuleService/GetFunctionBySystemCodeResponse")]
        System.Data.DataTable GetFunctionBySystemCode(string systemCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IModuleService/IsExistsModuleCode", ReplyAction="http://tempuri.org/IModuleService/IsExistsModuleCodeResponse")]
        bool IsExistsModuleCode(System.Guid systemID, System.Guid moduleID, string moduleCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IModuleService/GetFunctionByUserOrGroupID", ReplyAction="http://tempuri.org/IModuleService/GetFunctionByUserOrGroupIDResponse")]
        System.Data.DataTable GetFunctionByUserOrGroupID(System.Guid userOrGroupID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IModuleService/UpdateUserOrGroupFunction", ReplyAction="http://tempuri.org/IModuleService/UpdateUserOrGroupFunctionResponse")]
        bool UpdateUserOrGroupFunction(System.Guid userOrGroupID, System.Data.DataTable dtUserFunction);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IModuleService/UpdateUserOrGroupFunctionWithGroup", ReplyAction="http://tempuri.org/IModuleService/UpdateUserOrGroupFunctionWithGroupResponse")]
        bool UpdateUserOrGroupFunctionWithGroup(System.Guid userOrGroupID, System.Data.DataTable dtModuleFunction);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IModuleServiceChannel : MCD.UUP.CommonUI.ModuleService.IModuleService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class ModuleServiceClient : System.ServiceModel.ClientBase<MCD.UUP.CommonUI.ModuleService.IModuleService>, MCD.UUP.CommonUI.ModuleService.IModuleService {
        
        public ModuleServiceClient() {
        }
        
        public ModuleServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ModuleServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ModuleServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ModuleServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool InsertModule(MCD.UUP.CommonUI.ModuleService.ModuleEntity entity, System.Data.DataTable dtFunction) {
            return base.Channel.InsertModule(entity, dtFunction);
        }
        
        public bool UpdateModule(MCD.UUP.CommonUI.ModuleService.ModuleEntity entity, System.Data.DataTable dtFunction) {
            return base.Channel.UpdateModule(entity, dtFunction);
        }
        
        public bool DeleteModule(MCD.UUP.CommonUI.ModuleService.ModuleEntity entity) {
            return base.Channel.DeleteModule(entity);
        }
        
        public MCD.UUP.CommonUI.ModuleService.ModuleEntity GetSingleModule(MCD.UUP.CommonUI.ModuleService.ModuleEntity entity) {
            return base.Channel.GetSingleModule(entity);
        }
        
        public System.Data.DataTable SelectModules(System.Guid systemID, string moduleName) {
            return base.Channel.SelectModules(systemID, moduleName);
        }
        
        public System.Data.DataTable SelectModulesBySystemCode(string systemCode, string moduleName) {
            return base.Channel.SelectModulesBySystemCode(systemCode, moduleName);
        }
        
        public System.Data.DataTable SelectModulesWithGroupBySystemCode(string systemCode, string moduleName) {
            return base.Channel.SelectModulesWithGroupBySystemCode(systemCode, moduleName);
        }
        
        public System.Data.DataTable GetFunctionByModuleID(System.Guid moduleID) {
            return base.Channel.GetFunctionByModuleID(moduleID);
        }
        
        public System.Data.DataTable GetFunctionBySystemCode(string systemCode) {
            return base.Channel.GetFunctionBySystemCode(systemCode);
        }
        
        public bool IsExistsModuleCode(System.Guid systemID, System.Guid moduleID, string moduleCode) {
            return base.Channel.IsExistsModuleCode(systemID, moduleID, moduleCode);
        }
        
        public System.Data.DataTable GetFunctionByUserOrGroupID(System.Guid userOrGroupID) {
            return base.Channel.GetFunctionByUserOrGroupID(userOrGroupID);
        }
        
        public bool UpdateUserOrGroupFunction(System.Guid userOrGroupID, System.Data.DataTable dtUserFunction) {
            return base.Channel.UpdateUserOrGroupFunction(userOrGroupID, dtUserFunction);
        }
        
        public bool UpdateUserOrGroupFunctionWithGroup(System.Guid userOrGroupID, System.Data.DataTable dtModuleFunction) {
            return base.Channel.UpdateUserOrGroupFunctionWithGroup(userOrGroupID, dtModuleFunction);
        }
    }
}