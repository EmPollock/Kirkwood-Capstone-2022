﻿#pragma checksum "..\..\pgCreateEvent.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D3831BD297478CC3EF45737509D9DBBE8302CB1EAEFAFDF4EE734263518F8295"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using WPFPresentation;


namespace WPFPresentation {
    
    
    /// <summary>
    /// pgCreateEvent
    /// </summary>
    public partial class pgCreateEvent : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\pgCreateEvent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl tabsetCreateEvent;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\pgCreateEvent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabCreateEvent;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\pgCreateEvent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtBlkCreateNewEvent;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\pgCreateEvent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblEventTitle;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\pgCreateEvent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBoxEventName;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\pgCreateEvent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblEventDescription;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\pgCreateEvent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBoxEventDescription;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\pgCreateEvent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEventNext;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\pgCreateEvent.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEventCancel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WPFPresentation;component/pgcreateevent.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\pgCreateEvent.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.tabsetCreateEvent = ((System.Windows.Controls.TabControl)(target));
            return;
            case 2:
            this.tabCreateEvent = ((System.Windows.Controls.TabItem)(target));
            return;
            case 3:
            this.txtBlkCreateNewEvent = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.lblEventTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.txtBoxEventName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.lblEventDescription = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.txtBoxEventDescription = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.btnEventNext = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\pgCreateEvent.xaml"
            this.btnEventNext.Click += new System.Windows.RoutedEventHandler(this.btnEventNext_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnEventCancel = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

