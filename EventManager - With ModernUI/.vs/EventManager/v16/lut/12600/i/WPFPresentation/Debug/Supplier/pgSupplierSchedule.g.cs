﻿#pragma checksum "Supplier\pgSupplierSchedule.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0DC4FB0E651B9F19B5988E615C343F7F6CFC3576D09C00B741EA9F4AB5C5CC92"
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
using WPFPresentation.Supplier;


namespace WPFPresentation.Supplier {
    
    
    /// <summary>
    /// pgSupplierSchedule
    /// </summary>
    public partial class pgSupplierSchedule : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "Supplier\pgSupplierSchedule.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grdSupplierSchedule;
        
        #line default
        #line hidden
        
        
        #line 18 "Supplier\pgSupplierSchedule.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtSupplierSchedule;
        
        #line default
        #line hidden
        
        
        #line 19 "Supplier\pgSupplierSchedule.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Calendar calSupplierCalendar;
        
        #line default
        #line hidden
        
        
        #line 30 "Supplier\pgSupplierSchedule.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock lblSupplierDate;
        
        #line default
        #line hidden
        
        
        #line 34 "Supplier\pgSupplierSchedule.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid datSupplierActivities;
        
        #line default
        #line hidden
        
        
        #line 46 "Supplier\pgSupplierSchedule.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid datSupplierAvailabilities;
        
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
            System.Uri resourceLocater = new System.Uri("/WPFPresentation;component/supplier/pgsupplierschedule.xaml", System.UriKind.Relative);
            
            #line 1 "Supplier\pgSupplierSchedule.xaml"
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
            
            #line 9 "Supplier\pgSupplierSchedule.xaml"
            ((WPFPresentation.Supplier.pgSupplierSchedule)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.grdSupplierSchedule = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.txtSupplierSchedule = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.calSupplierCalendar = ((System.Windows.Controls.Calendar)(target));
            
            #line 19 "Supplier\pgSupplierSchedule.xaml"
            this.calSupplierCalendar.SelectedDatesChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.calSupplierCalendar_SelectedDatesChanged);
            
            #line default
            #line hidden
            
            #line 19 "Supplier\pgSupplierSchedule.xaml"
            this.calSupplierCalendar.DisplayDateChanged += new System.EventHandler<System.Windows.Controls.CalendarDateChangedEventArgs>(this.calSupplierCalendar_DisplayDateChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.lblSupplierDate = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.datSupplierActivities = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 7:
            this.datSupplierAvailabilities = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
