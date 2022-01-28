using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;
using DataAccessInterfaces;

namespace DataAccessFakes
{
    public class UserAccessorFake : IUserAccessor
    {
        List<User> fakeUsers = new List<User>();
        private List<String> fakePasswordHashes = new List<string>();

        public UserAccessorFake()
        {
            this.fakeUsers.Add(new User()
            {
                EmployeeID = 999999,
                EmailAddress = "tess@company.com",
                GivenName = "Tess",
                FamilyName = "Data",
                PhoneNumber = "1234567890",
                Roles = new List<String>(),
                Active = true
            });
            this.fakeUsers.Add(new User()
            {
                EmployeeID = 999998,
                EmailAddress = "duplicate@company.com",
                GivenName = "Tess",
                FamilyName = "Data",
                PhoneNumber = "1234567890",
                Roles = new List<String>(),
                Active = true
            });
            this.fakeUsers.Add(new User()
            {
                EmployeeID = 999997,
                EmailAddress = "duplicate@company.com",
                GivenName = "Tess",
                FamilyName = "Data",
                PhoneNumber = "1234567890",
                Roles = new List<String>(),
                Active = true
            });

            this.fakeUsers[0].Roles.Add("Prep");
            this.fakeUsers[0].Roles.Add("Rental");

            this.fakePasswordHashes.Add("9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E");
            this.fakePasswordHashes.Add("dup-9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E");
            this.fakePasswordHashes.Add("dup-9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E");
        }

        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int isAuthenticated = 0;

            for(int i = 0; i < fakeUsers.Count; i++)
            {
                if(fakeUsers[i].EmailAddress == email)
                {

                    if (fakePasswordHashes[i] == passwordHash && fakeUsers[i].Active)
                    {               // user is authenticated
                        isAuthenticated += 1;
                    }
                }
            }

            return isAuthenticated; // should only ever return 1 or 0
        }

        public List<string> SelectRolesByEmployeeID(int employeeID)
        {
            List<string> roles = new List<String>();
            Boolean foundEmployee = false;
            for(int i = 0; i < fakeUsers.Count; i++)
            {
                if(fakeUsers[i].EmployeeID == employeeID)
                {
                    roles = fakeUsers[i].Roles;
                    foundEmployee = true;
                    break;
                }
            }
            if (!foundEmployee)
            {
                throw new ApplicationException("Employee roles unavailable. Employee not found.");
            }

            return roles;
        }

        public User SelectUserByEmail(string email)
        {
            User user = null;
            foreach(var fakeUser in fakeUsers)
            {
                if(fakeUser.EmailAddress == email)
                {
                    user = fakeUser;
                }
            }

            if(user == null)
            {
                throw new ApplicationException("User not found.");
            }
            return user;
        }

        public int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash)
        {
            int rowsAffected = 0;

            for(int i =0; i < this.fakeUsers.Count; i++)
            {
                if(fakeUsers[i].EmailAddress == email)
                {
                    if(this.fakePasswordHashes[i] == oldPasswordHash)
                    {
                        this.fakePasswordHashes[i] = newPasswordHash;
                        rowsAffected = 1;
                        break;
                    }
                }
            }

            return rowsAffected;
        }
    }
}
