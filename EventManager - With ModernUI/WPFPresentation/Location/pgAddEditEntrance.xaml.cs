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
using DataAccessInterfaces;
using DataObjects;

namespace WPFPresentation.Location
{
    /// <summary>
    /// Interaction logic for pgAddEditEntrance.xaml
    /// </summary>
    public partial class pgAddEditEntrance : Page
    {
        IEntranceManager _entranceManager;
        ManagerProvider _managerProvider;
        int _locationID;
        User _user;

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Initializes component and sets up entrance manager with fake and default accessors
        /// </summary>
        /// <param name="locationID"></param>
        /// <param name="managerProvider"></param>
        /// <param name="user"></param>
        internal pgAddEditEntrance(int locationID, ManagerProvider managerProvider, User user)
        {
            //for fakes
            //_entranceManager = new EntranceManager(new EntranceAccessorFake());

            _entranceManager = new EntranceManager();
            _locationID = locationID;
            _managerProvider = managerProvider;
            _user = user;
            InitializeComponent();
        }

        /// <summary>
        /// Alaina Gilson
        /// Created 2022/03/03
        /// 
        /// Description:
        /// Adds a new entrance to database
        /// </summary>
        private void btnEntranceAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = txtBoxEntranceName.Text;
            string description = txtBoxEntranceDescription.Text;

            try
            {
                if (name == "" || description == "")
                {
                    MessageBox.Show("Please enter all fields for the entrance.");
                    txtBoxEntranceName.Focus();
                }
                else
                {
                    _entranceManager.CreateEntrance(_locationID, name, description);
                    //MessageBox.Show("Entrance has been added successfully.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem creating a new entrance.\n\n" + ex.Message);
            }
            finally
            {
                pgViewLocationDetails page = new pgViewLocationDetails(_locationID, _managerProvider, _user, 1);
                this.NavigationService.Navigate(page);            }
        }


        /// <summary>
        /// Alaina Gilson
        /// Created 2022/03/03
        /// 
        /// Description:
        /// Button cancel to take user back to view entrances page
        /// </summary>
        private void btnEntranceCancel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure?\nUnsaved changes will be discarded.",
                               "Cancel",
                               MessageBoxButton.YesNo,
                               MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                pgViewLocationDetails page = new pgViewLocationDetails(_locationID, _managerProvider, _user, 1);
                this.NavigationService.Navigate(page);
            }
        }
    }
}
