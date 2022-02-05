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
using DataAccessFakes;

namespace WPFPresentation.Location
{
    /// <summary>
    /// Interaction logic for pgViewLocations.xaml
    /// </summary>
    public partial class pgViewLocations : Page
    {
        ILocationManager _locationManager = null;

        /// <summary>
        /// Kris Howell
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Initialize location manager and page
        /// </summary>
        public pgViewLocations()
        {
            // fake accessor
            _locationManager = new LocationManager(new LocationAccessorFake());

            // live data accessor
            // _locationManager = new LocationManager();

            InitializeComponent();
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Populate list of locations table with all active locations
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                datLocationsList.ItemsSource = _locationManager.RetrieveActiveLocations();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
