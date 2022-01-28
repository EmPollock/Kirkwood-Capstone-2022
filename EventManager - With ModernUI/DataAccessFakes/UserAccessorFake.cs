﻿using System;
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
        /// <summary>
        /// Initializer for UserAccessorFake. Populates the lists with fake data.
        /// </summary>
        public UserAccessorFake()
        {
            this.fakeUsers.Add(new User()
            {
                UserID = 999999,
                EmailAddress = "tess@company.com",
                GivenName = "Tess",
                FamilyName = "Data",
                State = "CA",
                City = "Los Angeles",
                Zip = 12345,
                //Roles = new List<String>(),
                Active = true
            });
            this.fakeUsers.Add(new User()
            {
                UserID = 999998,
                EmailAddress = "duplicate@company.com",
                GivenName = "Tess",
                FamilyName = "Data",
                State = "NV",
                City = "Reno",
                Zip = null,
                //Roles = new List<String>(),
                Active = true
            });
            this.fakeUsers.Add(new User()
            {
                UserID = 999997,
                EmailAddress = "duplicate@company.com",
                GivenName = "Tess",
                FamilyName = "Data",
                State = "IA",
                City = "Cedar Rapids",
                Zip = 12345,
                //Roles = new List<String>(),
                Active = true
            });

            //this.fakeUsers[0].Roles.Add("Prep");
            //this.fakeUsers[0].Roles.Add("Rental");

            this.fakePasswordHashes.Add("9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E");
            this.fakePasswordHashes.Add("dup-9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E");
            this.fakePasswordHashes.Add("dup-9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E");
        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: Unknown
        /// 
        /// Description:
        /// Method to test user auth fake data 
        /// 
        /// </summary>
        /// <param name="email">Fake email to be used as credentials</param>
        /// <param name="passwordHash">Fake password to be used as credentials</param>
        /// <returns>Number of matching users</returns>
        public int AuthenticateUserWithEmailAndPasswordHash(string email, string passwordHash)
        {
            int isAuthenticated = 0;

            for (int i = 0; i < fakeUsers.Count; i++)
            {
                if (fakeUsers[i].EmailAddress == email)
                {

                    if (fakePasswordHashes[i] == passwordHash && fakeUsers[i].Active)
                    {               // user is authenticated
                        isAuthenticated += 1;
                    }
                }
            }

            return isAuthenticated; // should only ever return 1 or 0
        }

        /// <summary>
        /// Ramiro Pena
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// create fake user with fake data
        /// 
        /// </summary>
        /// <param name="newUser">User to be used to insert</param>
        /// <returns>Number of insert operations carried out</returns>
        public int InsertUser(User newUser)
        {
            int rowsAffected = 0;
            int userID = fakeUsers.Last().UserID + 1;


            fakeUsers.Add(new User()
            {
                GivenName = newUser.GivenName,
                FamilyName = newUser.FamilyName,
                EmailAddress = newUser.EmailAddress,
                State = newUser.State,
                City = newUser.City,
                Zip = newUser.Zip
            });

            rowsAffected++;

            return rowsAffected;
        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: Unknown
        /// 
        /// Description:
        /// Method to test user auth fake data.
        /// 
        /// TODO: Needs to be updated to get roles eventually. This isn't required as part of the login process.
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<string> SelectRolesByUserID(int userID)
        {
            List<string> roles = new List<String>();
            Boolean foundEmployee = false;
            for (int i = 0; i < fakeUsers.Count; i++)
            {
                if (fakeUsers[i].UserID == userID)
                {
                    //roles = fakeUsers[i].Roles;
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

        /// <summary>
        /// 
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: Unknown
        /// 
        /// Description:
        /// Get a user with a matching email address
        /// </summary>
        /// <param name="email">email to be used to seelct user</param>
        /// <returns>User with matching email address</returns>
        public User SelectUserByEmail(string email)
        {
            User user = null;
            foreach (var fakeUser in fakeUsers)
            {
                if (fakeUser.EmailAddress == email)
                {
                    user = fakeUser;
                }
            }

            if (user == null)
            {
                throw new ApplicationException("User not found.");
            }
            return user;
        }

        /// <summary>
        /// 
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: Unknown
        /// 
        /// Description:
        /// Update a fake password hash for testing purposes
        /// </summary>
        /// <param name="email">Email to have hash reset for</param>
        /// <param name="oldPasswordHash">Old password hash to be replaced</param>
        /// <param name="newPasswordHash">New password hash to replace</param>
        /// <returns>number of update operations that go through.</returns>
        public int UpdatePasswordHash(string email, string oldPasswordHash, string newPasswordHash)
        {
            int rowsAffected = 0;

            for (int i = 0; i < this.fakeUsers.Count; i++)
            {
                if (fakeUsers[i].EmailAddress == email)
                {
                    if (this.fakePasswordHashes[i] == oldPasswordHash)
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
