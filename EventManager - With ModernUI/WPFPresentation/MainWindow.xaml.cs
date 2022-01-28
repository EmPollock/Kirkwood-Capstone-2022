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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(btnLogin.Content.ToString() == "Login")
            {
                var email = this.txtEmailAddress.Text;
                var password = this.pwdPassword.Password;
                if (!password.IsValidPassword())
                {
                    password = "";
                }
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
            } else
            {
                this.updateUIForLogout();
            }
            
        }

        private void updateUIForLogout()
        {
            this._user = null;

            this.txtGreeting.Content = "You are not logged in.";
            this.staMessage.Content = "Welcome, please log in to continue";
            this.txtEmailAddress.Visibility = Visibility.Visible;
            this.pwdPassword.Visibility = Visibility.Visible;
            this.lblEmail.Visibility = Visibility.Visible;
            this.lblPassword.Visibility = Visibility.Visible;

            this.btnLogin.Content = "Login";
            txtEmailAddress.Focus();
            this.btnLogin.IsDefault = true;

        }

        private void updateUIForUser()
        {
            string roles = "";
            for(int i = 0; i < this._user.Roles.Count; i++)
            {
                roles += " " + this._user.Roles[i];
                
                if(i == this._user.Roles.Count - 2)
                {
                    if(this._user.Roles.Count > 2)
                    {
                        roles += ",";
                    }
                    roles += " and";
                } else if (i < this._user.Roles.Count - 1)
                {
                    roles += ",";
                }
            }
            this.txtGreeting.Content = "Welcome, " + this._user.GivenName + "! You are logged in as " + roles;

            this.staMessage.Content = "Logged in at " + DateTime.Now.ToLongDateString() + ", " + DateTime.Now.ToShortTimeString() + ". Please remember to log out before you leave.";

            this.txtEmailAddress.Text = "";
            this.pwdPassword.Password = "";
            this.txtEmailAddress.Visibility = Visibility.Hidden;
            this.pwdPassword.Visibility = Visibility.Hidden;
            this.lblEmail.Visibility = Visibility.Hidden;
            this.lblPassword.Visibility = Visibility.Hidden;

            this.btnLogin.Content = "Log Out";
            this.btnLogin.IsDefault = false;
        }

        private void frmMainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtEmailAddress.Focus();
        }
    }
}
