using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataAccessInterfaces;
using DataObjects;
using LogicLayer;
using LogicLayerInterfaces;
using System.Collections.Generic;
using DataAccessFakes;

namespace LogicLayerTests
{
    [TestClass]
    public class ActivityManagerTests
    {
        private IActivityManager _activityManager = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _activityManager = new ActivityManager(new ActivityAccessorFake(), new EventDateAccessorFake(),
                new SublocationAccessorFake(), new ActivityResultAccessorFake());
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/02
        /// 
        /// Description:
        /// Test that passes if RetrieveActivitesByEventID returns a list of the
        ///     expected size.
        /// </summary>
        [TestMethod]
        public void TestRetrieveActivitiesByEventIDRetrievesListWithCorrectEventID()
        {
            //arrange   
            const int eventID = 1;
            const int expectedCount = 2;
            List<ActivityVM> activities;
            int actualCount;

            //act
            activities = _activityManager.RetrieveActivitiesByEventID(eventID);
            actualCount = activities.Count;

            //assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Emma Pollock
        /// Created: 2022/02/02
        /// 
        /// Description:
        /// Test throws exception because event has no activities.
        /// </summary>

        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TestRetrieveActivitiesByEventIDFailsWithIncorrectEventID()
        {
            //arrange   
            const int eventID = 10;
            List<ActivityVM> activities;

            //act
            activities = _activityManager.RetrieveActivitiesByEventID(eventID);

            //assert
            //error testing, nothing to do here
        }
    }
}
