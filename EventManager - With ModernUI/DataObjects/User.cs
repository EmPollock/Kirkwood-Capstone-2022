using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObjects
{
    public class User
    {
        public int EmployeeID { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public List<string> Roles { get; set; }

        public bool Active { get; set; }

        public User()
        {

        }

        public User(int employeeID, string givenName, string familyName, string phoneNumber, string emailAddress, List<string> roles)
        {
            EmployeeID = employeeID;
            GivenName = givenName;
            FamilyName = familyName;
            PhoneNumber = phoneNumber;
            EmailAddress = emailAddress;
            Roles = roles;
        }


    }
}
