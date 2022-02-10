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
        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Click event for the login button. Logs the user in using input.
        /// 
        /// </summary>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var email = this.txtEmailAddress.Text;
            var password = this.pwdPassword.Password;
            if (!email.IsValidEmailAddress())
            {
                MessageBox.Show("Bad email address.");
                return;
            }
            else
            {
                try
                {
                    this._user = this._userManager.LoginUser(email, password);
                    if (this._user != null)
                    {

                        string instructions = "On first login, all new users must choose a password to continue.";
                        if(password == "newuser")
                        {
                            // force change password
                            var updateWindow = new UpdatePasswordWindow(this._userManager, this._user, instructions, true);

                            bool? result = updateWindow.ShowDialog();
                            if(result == true)
                            {

                                this.updateUIForUser();
                            } else
                            {
                                _user = null;
                                this.updateUIForLogout();
                                MessageBox.Show("You did not update your password. You will be logged out.");
                            }
                        } else
                        {
                            this.updateUIForUser();
                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message, "Alert!", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.pwdPassword.Password = "";
                    this.txtEmailAddress.Select(0, Int32.MaxValue);
                    this.txtEmailAddress.Focus();
                }
            }
            
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

            this.txtGreeting.Content = "You are not logged in.";
            this.staMessage.Content = "Welcome, please log in to continue";

            this.grdMainMenu.Visibility = Visibility.Collapsed;
            this.grdLogin.Visibility = Visibility.Visible;
            txtEmailAddress.Focus();
            this.btnLogin.IsDefault = true;

        }

        /// <summary>
        /// Christopher Repko 
        /// Created: 2022/01/27
        /// 
        /// Description:
        /// Method to handle UI transition from logged out to logged in states
        /// 
        /// </summary>
        private void updateUIForUser()
        {

            this.staMessage.Content = "Logged in at " + DateTime.Now.ToLongDateString() + ", " + DateTime.Now.ToShortTimeString() + ". Please remember to log out before you leave.";

            this.txtEmailAddress.Text = "";
            this.pwdPassword.Password = "";
            this.mnuUser.Header = this._user.GivenName + " " + this._user.FamilyName + " ▼";
            this.grdLogin.Visibility = Visibility.Collapsed;
            this.grdMainMenu.Visibility = Visibility.Visible;
        }


        /// <summary>
        /// Christopher Repko 
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Loaded event for window. Focuses the email text input.
        /// 
        /// </summary>
        private void frmMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtEmailAddress.Focus();
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
            Uri pageURI = new Uri("Event/pgCreateEvent.xaml", UriKind.Relative);
            this.MainFrame.NavigationService.Navigate(pageURI);
        }


        /// <summary>
        /// Christopher Repko 
        /// Created: 2022/1/27
        /// 
        /// Description:
        /// Click event for "View My Events" button. Currently brings up the Event List screen
        /// 
        /// Update:
        /// Derrick Nagy
        /// Updated: 2022/02/08
        /// 
        /// Description:
        /// Added option to send user information to the view events page
        /// 
        /// </summary>
        private void btnViewEvents_Click(object sender, RoutedEventArgs e)
        {
            Uri pageURI = new Uri("Event/pgViewEvents.xaml", UriKind.Relative);

            if (_user != null)
            {
                pgViewEvents pgViewEvents = new pgViewEvents(_user);
                this.MainFrame.NavigationService.Navigate(pgViewEvents);
            }
            else
            {
                this.MainFrame.NavigationService.Navigate(pageURI);
            }

        }

        private void btnViewVolunteers_Click(object sender, RoutedEventArgs e)
        {
            Uri pageURI = new Uri("Volunteer/pgViewAllVolunteers.xaml", UriKind.Relative);
            this.MainFrame.NavigationService.Navigate(pageURI);
        }

        private void btnViewLocations_Click(object sender, RoutedEventArgs e)
        {
            Uri pageURI = new Uri("Location/pgViewLocations.xaml", UriKind.Relative);
            this.MainFrame.NavigationService.Navigate(pageURI);
        }
    }
}
