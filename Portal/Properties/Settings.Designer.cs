//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace avii.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.1.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.WebServiceUrl)]
        [global::System.Configuration.DefaultSettingValueAttribute("http://mvneplatform.com/ShoppingBag.asmx")]
        public string avii_com_telspace_ShoppingBag {
            get {
                return ((string)(this["avii_com_telspace_ShoppingBag"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://sanvitti.com/purchaseorder.asmx")]
        public string avii_com_sanvitti_www_PurchaseOrder {
            get {
                return ((string)(this["avii_com_sanvitti_www_PurchaseOrder"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://jbhargava-001-site1.htempurl.com/purchaseorder.asmx")]
        public string avii_com_aerovoice_www_PurchaseOrder {
            get {
                return ((string)(this["avii_com_aerovoice_www_PurchaseOrder"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://jbhargava-001-site1.htempurl.com/purchaseorder.asmx")]
        public string avii_com_aerovoice2_www_PurchaseOrder {
            get {
                return ((string)(this["avii_com_aerovoice2_www_PurchaseOrder"]));
            }
        }
    }
}
