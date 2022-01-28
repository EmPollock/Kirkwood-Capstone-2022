using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

using DataAccessLayer;
using DataObjects;
using DataAccessInterfaces;
using LogicLayerInterfaces;

namespace LogicLayer
{
    public class UserManager : IUserManager
    {
        // Dependency is on IUserAccessor provider. This takes a tightly-coupled setup and makes it loosely coupled.
        private IUserAccessor _userAccessor;

        // A default constructor for most users
        public UserManager()
        {
            _userAccessor = new UserAccessor();
        }

        // Overloaded accessor allows calling code to
        // supply its own IUserAccessor provider.s
        public UserManager(IUserAccessor userAccessor)
        {
            this._userAccessor = userAccessor;
        }

        public bool AuthenticateUser(string email, string password)
        {
            bool result = false;
            try
            {
                result = 1 == _userAccessor.AuthenticateUserWithEmailAndPasswordHash(email, password);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public string HashSha256(string source)
        {
            string result = "";

            // create a byte array
            Byte[] data;

            //Create hash provider object
            using(SHA256 sha256hasher = SHA256.Create())
            {
                data = sha256hasher.ComputeHash(Encoding.UTF8.GetBytes(source));
            }
            // create an output stringbuilder object.
            var s = new StringBuilder();
            
            // loop through the hashed output making characters
            for(int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }
            result = s.ToString().ToUpper();

            return result;
        }

        public User GetUserByEmail(string email)
        {
            User requestedUser = null;

            try
            {
                requestedUser = this._userAccessor.SelectUserByEmail(email);
            }
            catch (Exception)
            {
                throw;
            }

            return requestedUser;
        }

        public List<string> GetRolesForUser(int employeeID)
        {
            List<String> roles = null;
            try
            {
                roles = this._userAccessor.SelectRolesByEmployeeID(employeeID);
            }
            catch (Exception)
            {

                throw;
            }
            return roles;
        }

        public User LoginUser(string email, string password)
        {
            User loggedInUser = null;
            try
            {
                if (email == "")
                {
                    throw new ArgumentException("Missing email.");
                }
                if(password == "") // or fails complexity rules.
                {
                    throw new ArgumentException("Bad or Missing password.");
                }
            
                password = this.HashSha256(password);
                if(this.AuthenticateUser(email, password))
                {
                    loggedInUser = this.GetUserByEmail(email);
                    loggedInUser.Roles = this.GetRolesForUser(loggedInUser.EmployeeID);
                } else
                {
                    throw new ApplicationException("Bad Email Address or Password.");
                }
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Login failed. Please check your credentials.", ex);
            }

            return loggedInUser;
        }

        public bool ResetPassword(string email, string oldPassword, string newPassword)
        {
            bool result = false;

            try
            {
                string oldPasswordHash = this.HashSha256(oldPassword);
                string newPasswordHash = this.HashSha256(newPassword);

                result = (1 == this._userAccessor.UpdatePasswordHash(email, oldPasswordHash, newPasswordHash));
                if (!result)
                {
                    throw new ApplicationException("Could not update password.");
                }
            }
            catch (Exception e)
            {
                throw new ApplicationException("Failed to update password", e);
            }


            return result;
        }
    }
}
