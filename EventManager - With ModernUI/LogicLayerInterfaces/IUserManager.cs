using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;

namespace LogicLayerInterfaces
{
    public interface IUserManager
    {
        // Should return a user object with roles
        // or throw an exception.
        User LoginUser(string email, string password);
        bool AuthenticateUser(string email, string password);

        string HashSha256(string source);

        User GetUserByEmail(string email);

        List<String> GetRolesForUser(int employeeID);

        bool ResetPassword(string email, string oldPassword, string newPassword);
    }
}
