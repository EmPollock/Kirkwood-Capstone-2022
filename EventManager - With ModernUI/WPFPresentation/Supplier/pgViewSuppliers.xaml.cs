using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LogicLayer;
using LogicLayerInterfaces;
using DataObjects;
using DataAccessFakes;

namespace WPFPresentation
{
    public partial class pgViewSuppliers : Page
    {
        ISupplierManager _supplierManager = null;
        ManagerProvider _managerProvider = null;

        /// <summary>
        /// Kris Howell
        /// Created: 2022/01/27
        /// 
        /// Description:
        /// Initialize supplier manager and page
        /// 
        /// Update:
        /// Austin Timmerman
        /// Updated: 2022/02/27
        /// 
        /// Description:
        /// Added the ManagerProvider instance variable and modified page parameters
        /// </summary>
        internal pgViewSuppliers(ManagerProvider managerProvider)
        {
            _managerProvider = managerProvider;
            _supplierManager = managerProvider.SupplierManager;

            InitializeComponent();
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/01/27
        /// 
        /// Description:
        /// Populate list of suppliers table with all active suppliers
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                datSuppliersList.ItemsSource = _supplierManager.RetrieveActiveSuppliers();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void datSuppliersList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(this.datSuppliersList.SelectedItem != null)
            {
                DataObjects.Supplier supplier = (DataObjects.Supplier)this.datSuppliersList.SelectedItem;

                Page page = new Supplier.pgViewSupplierListing(supplier, _managerProvider);
                this.NavigationService.Navigate(page);
            }
        }
    }
}
