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
using DataAccessFakes;
using DataObjects;
using LogicLayerInterfaces;
using WPFPresentation.Location;

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IUserManager _userManager = null;
        User _user = null;
        
        public MainWindow()
        {

            this._userManager = new UserManager();
            //this._userManager = new UserManager(new UserAccessorFake());
            // Keep this always safe! 
            InitializeComponent();
            this.btnBack.IsEnabled = false;
        }

        public MainWindow(User user, IUserManager userManager)
        {
            InitializeComponent();
            this._user = user;
            this._userManager = userManager;
            this.btnBack.IsEnabled = false;
            this.mnuUser.Header = user.GivenName + " ▼";
        }

        /// <summary>
        /// Christopher Repko 
        /// Created: 2022/01/27
        /// 
        /// Description:
        /// Method to handle UI transition from logged in to logged out states
        /// 
        /// </summary>
        private void updateUIForLogout()
        {
            this._user = null;

            SplashScreen splash = new SplashScreen();
            splash.Show();
            this.Close();
        }

        /// <summary>
        /// Christopher Repko 
        /// Created: 2022/1/27
        /// 
        /// Description:
        /// Click event for Logout option. Logs the user out.
        /// 
        /// </summary>
        private void mnuLogOut_Click(object sender, RoutedEventArgs e)
        {
            this.updateUIForLogout();
        }

        /// <summary>
        /// Christopher Repko 
        /// Created: 2022/1/27
        /// 
        /// Description:
        /// Click event for "Create Event" button. Brings up the Create Event screen.
        /// 
        /// </summary>

        private void btnCreateEvents_Click(object sender, RoutedEventArgs e)
        {
            Page page = new pgCreateEvent();
            this.MainFrame.NavigationService.Navigate(page);
        }


        /// <summary>
        /// Christopher Repko 
        /// Created: 2022/1/27
        /// 
        /// Description:
        /// Click event for "View My Events" button. Currently brings up the Event List screen
        /// 
        /// </summary>
        private void btnViewEvents_Click(object sender, RoutedEventArgs e)
        {
            Page page = new pgViewEvents();
            this.MainFrame.NavigationService.Navigate(page);
        }

        private void btnViewVolunteers_Click(object sender, RoutedEventArgs e)
        {
            Page page = new pgViewAllVolunteers();
            this.MainFrame.NavigationService.Navigate(page);
        }

        /// <summary>
        /// Christopher Repko 
        /// Created: 2022/2/4
        /// 
        /// Description:
        /// Click event for the back button. Navigates to the previous screen and handles button enabling.
        /// 
        /// </summary>
        /// <param name="sender">The back button</param>
        /// <param name="e">Arguments passed as part of the event</param>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if(this.MainFrame.NavigationService.CanGoBack)
            {
                this.MainFrame.GoBack();
                if(!this.MainFrame.NavigationService.CanGoBack)
                {
                    this.btnBack.IsEnabled = false;
                }
            } else
            {
                this.btnBack.IsEnabled = false;
            }
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if(this.MainFrame.NavigationService.CanGoBack)
            {
                this.btnBack.IsEnabled = true;
            }
        }

        private void btnViewLocations_Click(object sender, RoutedEventArgs e)
        {
            var page = new pgViewLocations();
            this.MainFrame.NavigationService.Navigate(page);
        }

        private void btnViewSuppliers_Click(object sender, RoutedEventArgs e)
        {
            var page = new pgViewSuppliers();
            this.MainFrame.NavigationService.Navigate(page);
        }
    }
}
