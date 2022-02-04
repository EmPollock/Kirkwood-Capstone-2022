using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LogicLayer;
using DataAccessFakes;
using DataObjects;
using LogicLayerInterfaces;
using System.Collections.Generic;

namespace LogicLayerTests
{
    [TestClass]
    public class UserManagerTests
    {
        private IUserManager userManager;

        [TestInitialize]
        public void TestSetup()
        {

            userManager = new UserManager(new UserAccessorFake());
        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Tests that SHA-256 hashing works correctly
        /// 
        /// </summary>
        [TestMethod]
        public void TestHashSha256ReturnsCorrectHashValue()
        {
            // Arrange
            const string source = "newuser";
            const string ExpectedResult = "9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E";
            string actualResult = "";

            //Act
            actualResult = userManager.HashSha256(source);

            //Assert
            Assert.AreEqual(ExpectedResult, actualResult);
        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Tests that authentication logic works
        /// 
        /// </summary>
        [TestMethod]
        public void TestAuthenticateUserPassesWithCorrectUsernamePasswordHash()
        {
            // Arrange
            const string email = "tess@company.com";
            const string passwordHash = "9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E";
            const bool ExpectedResult = true;
            bool actualResult;

            // Act
            actualResult = userManager.AuthenticateUserByEmailAndPassword(email, passwordHash);

            // Assert
            Assert.AreEqual(ExpectedResult, actualResult);

        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Tests authentication logic rejects incorrect username
        /// 
        /// </summary>
        [TestMethod]
        public void TestAuthenticateUserFailsWithIncorrectUsername()
        {
            // Arrange
            const string email = "tess-x@company.com";
            const string passwordHash = "9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E";
            const bool ExpectedResult = false;
            bool actualResult;

            // Act
            actualResult = userManager.AuthenticateUserByEmailAndPassword(email, passwordHash);

            // Assert
            Assert.AreEqual(ExpectedResult, actualResult);

        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Tests authentication logic rejects incorrect password
        /// 
        /// </summary>
        [TestMethod]
        public void TestAuthenticateUserFailsWithIncorrectPasswordHash()
        {
            // Arrange
            const string email = "tess@company.com";
            const string passwordHash = "x9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E";
            const bool ExpectedResult = false;
            bool actualResult;

            // Act
            actualResult = userManager.AuthenticateUserByEmailAndPassword(email, passwordHash);

            // Assert
            Assert.AreEqual(ExpectedResult, actualResult);

        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Tests authentication logic rejects cases where multiple users are returned from the same credentials
        /// 
        /// </summary>
        [TestMethod]
        public void TestAuthenticateUserFailsWithDuplicateUsers()
        {
            // Arrange
            const string email = "duplicate@company.com";
            const string passwordHash = "dup-9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E";
            const bool ExpectedResult = false;
            bool actualResult;

            // Act
            actualResult = userManager.AuthenticateUserByEmailAndPassword(email, passwordHash);

            // Assert
            Assert.AreEqual(ExpectedResult, actualResult);

        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Tests that user selection logic grabs correct user
        /// 
        /// </summary>
        [TestMethod]
        public void TestSelectUserByEmailReturnsCorrectUser()
        {
            // Arrange
            User user = null;
            const string expectedUserEmail = "tess@company.com";
            int expectedUserID = 999999;
            int actualUserID = 0;
            // Act
            user = userManager.RetrieveUserByEmail(expectedUserEmail);
            actualUserID = user.UserID;
            // Assert
            Assert.AreEqual(expectedUserID, actualUserID);
        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Tests user selection logic throws an exception if there is no valid user.
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSelectUserByEmailReturnsApplicateExceptionForBadEmail()
        {
            // Arrange
            User user = null;
            const string badUserEmail = "xtess@company.com";
            // Act
            user = userManager.RetrieveUserByEmail(badUserEmail);
            // Assert
            // Nothing to do, checking for exception.
        }

        // Christopher Repko
        // User role tests. We aren't using them right now, but this can be used for later templates.
        //[TestMethod]
        //public void TestGetRolesForUserReturnsCorrectRoles()
        //{
        //    // Arrange
        //    List<string> actualResult = null;
        //    List<string> expectedResult = new List<string>();
        //    expectedResult.Add("Rental");
        //    expectedResult.Add("Prep");
        //    const int employeeID = 999999;

        //    //Act
        //    actualResult = userManager.GetRolesForUser(employeeID);

        //    // Assert
        //    CollectionAssert.AreEquivalent(expectedResult, actualResult);
        //}

        //[TestMethod]
        //public void TestGetRolesForUserFailsWithIncorrectList()
        //{
        //    // Arrange
        //    List<string> actualResult = null;
        //    List<string> expectedResult = new List<string>();
        //    expectedResult.Add("xRental");
        //    expectedResult.Add("Prep");
        //    const int employeeID = 999999;

        //    //Act
        //    actualResult = userManager.GetRolesForUser(employeeID);

        //    // Assert
        //    CollectionAssert.AreNotEquivalent(expectedResult, actualResult);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(ApplicationException))]
        //public void TestGetRolesForUserThrowsApplicationExceptionForBadUserID()
        //{
        //    // Arrange
        //    const int badUserID = 999;

        //    //Act
        //    userManager.GetRolesForUser(badUserID);

        //    // Assert
        //    // Nothing to do here.
        //}

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Tests password reset logic works correctly
        /// 
        /// </summary>
        [TestMethod]
        public void TestResetPasswordWorksWithValidPasswords()
        {
            // Arrange
            const string oldPassword = "newuser";
            const string newPassword = "P@ssw0rd";
            const string email = "tess@company.com";
            const bool expectedResult = true;
            bool actualResult;

            // Act
            actualResult = userManager.UpdatePasswordHash(email, oldPassword, newPassword);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Tests password reset logic rejects passwords with incorrect old password.
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestResetPasswordThrowsExceptionWithBadOldPassword()
        {
            // Arrange
            const string oldPassword = "xnewuser";
            const string newPassword = "P@ssw0rd";
            const string email = "tess@company.com";

            // Act
            userManager.UpdatePasswordHash(email, oldPassword, newPassword);

            // Assert
            // Nothing to do here

        }

        /// <summary>
        /// Christopher Repko (Based on Jim Glasgow's in-class examples)
        /// Created: 2022/1/21
        /// 
        /// Description:
        /// Tests password reset logic rejects bas email addresses
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestResetPasswordThrowsExceptionWithBadEmail()
        {
            // Arrange
            const string oldPassword = "newuser";
            const string newPassword = "P@ssw0rd";
            const string email = "xtess@company.com";

            // Act
            userManager.UpdatePasswordHash(email, oldPassword, newPassword);

            // Assert
            // Nothing to do here

        }
    }
}
