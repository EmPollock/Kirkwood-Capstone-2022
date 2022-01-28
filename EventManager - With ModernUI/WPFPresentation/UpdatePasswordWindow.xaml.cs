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
using System.Windows.Shapes;

using DataObjects;
using LogicLayer;
using DataAccessFakes;
using DataAccessInterfaces;
using LogicLayerInterfaces;

namespace WPFPresentation
{
    /// <summary>
    /// Interaction logic for UpdatePasswordWindow.xaml
    /// </summary>
    public partial class UpdatePasswordWindow : Window
    {
        IUserManager _userManager;
        User _user;
        bool _newUser;

        public UpdatePasswordWindow(IUserManager userManager, User user, string instructions, bool newUser = false)
        {
            this._userManager = userManager;
            this._user = user;
            this._newUser = newUser;
            InitializeComponent();
            this.pwdOldPassword.Focus();
            this.txtInstructions.Text = instructions;
            if (this._newUser)
            {
                this.newUserUpdate();
            }
        }

        private void newUserUpdate()
        {
            this.pwdOldPassword.Password = "newuser";
            this.pwdOldPassword.IsEnabled = false;
            this.pwdNewPassword.Focus();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if(pwdNewPassword.Password != pwdConfirmPassword.Password )
            {
                MessageBox.Show("New password and Retype password must match");
                pwdNewPassword.Password = "";
                this.pwdConfirmPassword.Password = "";
                this.pwdNewPassword.Focus();
                return;
            }
            try
            {
                string oldPassword = this.pwdOldPassword.Password;
                string newPassword = this.pwdNewPassword.Password;

                if (this._userManager.ResetPassword(this._user.EmailAddress, oldPassword, newPassword))
                {
                    MessageBox.Show("Password successfully updated");
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Update failed.\n\n" + ex.Message);
            }
        }
    }
}
