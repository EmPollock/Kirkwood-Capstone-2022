using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessFakes;
using LogicLayerInterfaces;
using DataObjects;
using LogicLayer;

namespace LogicLayerTests
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/03/10
    /// 
    /// Description:
    /// The VolunteerReviewManagerTests test class for all Volunteer Review tests
    /// </summary>
    [TestClass]
    public class VolunteerReviewManagerTests
    {
        IVolunteerReviewManager _volunteerReviewManager = null;

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// The test initializer 
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _volunteerReviewManager = new VolunteerReviewManager(new VolunteerReviewAccessorFake());
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Test method which checks to see if the passed in volunteerID returns the correct amount of
        /// reviews
        /// </summary>
        [TestMethod]
        public void TestRetrieveVolunteerReviewsByVolunteerIDReturnsCorrectAmount()
        {
            const int volunteerID = 999999;
            const int expectedCount = 2;
            int actualCount;

            actualCount = _volunteerReviewManager.RetrieveVolunteerReviewsByVolunteerID(volunteerID).Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Test method which checks to see if the bad volunteerID passed in returns the correct amount of
        /// reviews
        /// </summary>
        [TestMethod]
        public void TestRetrieveVolunteerReviewsByVolunteerIDWithBadVolunteerIDReturnsCorrectAmount()
        {
            const int volunteerID = 1001010;
            const int expectedCount = 0;
            int actualCount;

            actualCount = _volunteerReviewManager.RetrieveVolunteerReviewsByVolunteerID(volunteerID).Count;

            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
