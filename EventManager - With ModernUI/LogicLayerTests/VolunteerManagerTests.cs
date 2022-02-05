using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessFakes;
using LogicLayer;

namespace LogicLayerTests
{
    /// <summary>
    /// Austin Timmerman
    /// Created: 2022/01/26
    /// 
    /// Test class for VolunteerManager
    /// </summary>
    [TestClass]
    public class VolunteerManagerTests
    {
        private IVolunteerManager _volunteerManager = null;

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// The test initializer
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        [TestInitialize]
        public void TestInitialize()
        {
            _volunteerManager = new VolunteerManager(new VolunteerAccessorFake());
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Test that returns the amount of volunteers retrieved
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>

        [TestMethod]
        public void TestRetrieveAllVolunteerReturnsThree()
        {
            // arrange
            const int expectedAmount = 3;
            int actualAmount = 0;

            // act
            actualAmount = _volunteerManager.RetrieveAllVolunteers().Count;

            // assert

            Assert.AreEqual(expectedAmount, actualAmount);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Test that returns the amount of volunteer reviews retrieved
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        [TestMethod]
        public void TestRetrieveAllVolunteerReviewsReturnsThree()
        {
            // arrange
            const int expectedAmount = 3;
            int actualAmount = 0;

            // act
            actualAmount = _volunteerManager.RetrieveAllVolunteerReviews().Count;

            // assert

            Assert.AreEqual(expectedAmount, actualAmount);
        }
    }
}
