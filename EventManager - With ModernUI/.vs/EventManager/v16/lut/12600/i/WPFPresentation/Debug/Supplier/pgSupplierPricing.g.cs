﻿#pragma checksum "Supplier\pgSupplierPricing.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E1745D08DDF514A1DE4E0CC3F955147E9197BF2215486335437705F2A4DA76DE"
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
    /// pgSupplierPricing
    /// </summary>
    public partial class pgSupplierPricing : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "Supplier\pgSupplierPricing.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtSupplierServices;
        
        #line default
        #line hidden
        
        
        #line 31 "Supplier\pgSupplierPricing.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid imageDataGrid;
        
        #line default
        #line hidden
        
        
        #line 44 "Supplier\pgSupplierPricing.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn colServiceName;
        
        #line default
        #line hidden
        
        
        #line 45 "Supplier\pgSupplierPricing.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn colServicePrice;
        
        #line default
        #line hidden
        
        
        #line 46 "Supplier\pgSupplierPricing.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTextColumn colServiceDescription;
        
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
            System.Uri resourceLocater = new System.Uri("/WPFPresentation;component/supplier/pgsupplierpricing.xaml", System.UriKind.Relative);
            
            #line 1 "Supplier\pgSupplierPricing.xaml"
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
            
            #line 10 "Supplier\pgSupplierPricing.xaml"
            ((WPFPresentation.Supplier.pgSupplierPricing)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtSupplierServices = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.imageDataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            this.colServiceName = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 5:
            this.colServicePrice = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            case 6:
            this.colServiceDescription = ((System.Windows.Controls.DataGridTextColumn)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
