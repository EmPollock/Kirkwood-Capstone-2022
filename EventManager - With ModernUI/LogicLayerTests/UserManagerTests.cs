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

        [TestMethod]
        public void TestAuthenticateUserPassesWithCorrectUsernamePasswordHash()
        {
            // Arrange
            const string email = "tess@company.com";
            const string passwordHash = "9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E";
            const bool ExpectedResult = true;
            bool actualResult;

            // Act
            actualResult = userManager.AuthenticateUser(email, passwordHash);

            // Assert
            Assert.AreEqual(ExpectedResult, actualResult);
            
        }

        [TestMethod]
        public void TestAuthenticateUserFailsWithIncorrectUsername()
        {
            // Arrange
            const string email = "tess-x@company.com";
            const string passwordHash = "9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E";
            const bool ExpectedResult = false;
            bool actualResult;

            // Act
            actualResult = userManager.AuthenticateUser(email, passwordHash);

            // Assert
            Assert.AreEqual(ExpectedResult, actualResult);

        }

        [TestMethod]
        public void TestAuthenticateUserFailsWithIncorrectPasswordHash()
        {
            // Arrange
            const string email = "tess@company.com";
            const string passwordHash = "x9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E";
            const bool ExpectedResult = false;
            bool actualResult;

            // Act
            actualResult = userManager.AuthenticateUser(email, passwordHash);

            // Assert
            Assert.AreEqual(ExpectedResult, actualResult);

        }

        [TestMethod]
        public void TestAuthenticateUserFailsWithDuplicateUsers()
        {
            // Arrange
            const string email = "duplicate@company.com";
            const string passwordHash = "dup-9C9064C59F1FFA2E174EE754D2979BE80DD30DB552EC03E7E327E9B1A4BD594E";
            const bool ExpectedResult = false;
            bool actualResult;

            // Act
            actualResult = userManager.AuthenticateUser(email, passwordHash);

            // Assert
            Assert.AreEqual(ExpectedResult, actualResult);

        }

        [TestMethod]
        public void TestSelectUserByEmailReturnsCorrectUser()
        {
            // Arrange
            User user = null;
            const string expectedEmployeeEmail = "tess@company.com";
            int expectedEmployeeId = 999999;
            int actualEmployeeId = 0;
            // Act
            user = userManager.GetUserByEmail(expectedEmployeeEmail);
            actualEmployeeId = user.EmployeeID;
            // Assert
            Assert.AreEqual(expectedEmployeeId, actualEmployeeId);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestSelectUserByEmailReturnsApplicateExceptionForBadEmail()
        {
            // Arrange
            User user = null;
            const string badEmployeeEmail = "xtess@company.com";
            // Act
            user = userManager.GetUserByEmail(badEmployeeEmail);
            // Assert
            // Nothing to do, checking for exception.
        }

        [TestMethod]
        public void TestGetRolesForUserReturnsCorrectRoles()
        {
            // Arrange
            List<string> actualResult = null;
            List<string> expectedResult = new List<string>();
            expectedResult.Add("Rental");
            expectedResult.Add("Prep");
            const int employeeID = 999999;

            //Act
            actualResult = userManager.GetRolesForUser(employeeID);

            // Assert
            CollectionAssert.AreEquivalent(expectedResult, actualResult);
        }

        [TestMethod]
        public void TestGetRolesForUserFailsWithIncorrectList()
        {
            // Arrange
            List<string> actualResult = null;
            List<string> expectedResult = new List<string>();
            expectedResult.Add("xRental");
            expectedResult.Add("Prep");
            const int employeeID = 999999;

            //Act
            actualResult = userManager.GetRolesForUser(employeeID);

            // Assert
            CollectionAssert.AreNotEquivalent(expectedResult, actualResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestGetRolesForUserThrowsApplicationExceptionForBadEmployeeID()
        {
            // Arrange
            const int badEmployeeID = 999;

            //Act
            userManager.GetRolesForUser(badEmployeeID);

            // Assert
            // Nothing to do here.
        }

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
            actualResult = userManager.ResetPassword(email, oldPassword, newPassword);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);

        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestResetPasswordThrowsExceptionWithBadOldPassword()
        {
            // Arrange
            const string oldPassword = "xnewuser";
            const string newPassword = "P@ssw0rd";
            const string email = "tess@company.com";

            // Act
            userManager.ResetPassword(email, oldPassword, newPassword);

            // Assert
            // Nothing to do here

        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestResetPasswordThrowsExceptionWithBadEmail()
        {
            // Arrange
            const string oldPassword = "newuser";
            const string newPassword = "P@ssw0rd";
            const string email = "xtess@company.com";

            // Act
            userManager.ResetPassword(email, oldPassword, newPassword);

            // Assert
            // Nothing to do here

        }
    }
}
