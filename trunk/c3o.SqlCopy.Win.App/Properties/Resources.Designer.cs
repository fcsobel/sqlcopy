﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace c3o.SqlCopy.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("c3o.SqlCopy.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static System.Drawing.Bitmap control_play_blue {
            get {
                object obj = ResourceManager.GetObject("control_play_blue", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        internal static System.Drawing.Bitmap pencil {
            get {
                object obj = ResourceManager.GetObject("pencil", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot;?&gt;
        ///&lt;ArrayOfCopyObject xmlns:xsi=&quot;http://www.w3.org/2001/XMLSchema-instance&quot; xmlns:xsd=&quot;http://www.w3.org/2001/XMLSchema&quot;&gt;
        ///  &lt;CopyObject&gt;
        ///    &lt;Name&gt;SqlServer&lt;/Name&gt;
        ///	  &lt;SourceType&gt;SqlServer&lt;/SourceType&gt;
        ///    &lt;Source&gt;&lt;/Source&gt;
        ///    &lt;Destination&gt;&lt;/Destination&gt;
        ///    &lt;BatchSize&gt;0&lt;/BatchSize&gt;
        ///    &lt;BulkCopyOptions&gt;0&lt;/BulkCopyOptions&gt;
        ///    &lt;BulkCopyTimeout&gt;30&lt;/BulkCopyTimeout&gt;
        ///    &lt;NotifyAfter&gt;0&lt;/NotifyAfter&gt;
        ///    &lt;Default&gt;false&lt;/Default&gt;
        ///    &lt;UseInternalTransaction&gt;false&lt;/UseInternalTrans [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string template {
            get {
                return ResourceManager.GetString("template", resourceCulture);
            }
        }
    }
}
