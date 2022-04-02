﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        /// Austin Timmerman
        /// Created: 2022/03/30
        /// 
        /// Description:
        /// Test to make sure that RetrieveAvailabilityByVolunteerIDAndDate correctly returns an empty
        /// list when no availability is found
        /// </summary>
        /// (Original Author: Kris Howell LocationManagerTests.cs)
        [TestMethod]
        public void TestRetrieveAvailabilityByVolunteerIDAndDateReturnsNullWithNoneFound()
        {
            // arrange
            const int volunteerID = 100000;
            DateTime badDate = new DateTime(1999, 01, 01);
            List<Availability> actualList;

            // act
            actualList = _volunteerManager.RetrieveAvailabilityByVolunteerIDAndDate(volunteerID, badDate);

            // assert
            Assert.IsFalse(actualList.Any());
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/30
        /// 
        /// Description:
        /// Test to make sure that RetrieveAvailabilityByVolunteerIDAndDate correctly returns an Availability
        /// object with null TimeStart/TimeEnd properties when there is an exception for a day with no availability
        /// </summary>
        /// (Original Author: Kris Howell LocationManagerTests.cs)
        [TestMethod]
        public void TestRetrieveAvailabilityByVolunteerIDAndDateReturnsEmptyAvailabilityForEmptyException()
        {
            // arrange
            const int volunteerID = 100000;
            DateTime date = new DateTime(2022, 01, 03);
            DateTime? expected = null;
            DateTime? actual;

            // act
            actual = _volunteerManager.RetrieveAvailabilityByVolunteerIDAndDate(volunteerID, date).First().TimeStart;

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}
