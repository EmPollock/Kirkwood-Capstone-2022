using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessFakes;
using LogicLayer;
using System.Collections.Generic;

namespace LogicLayerTests
{
    [TestClass]
    public class EventDateManagerTests
    {
        private IEventDateManager _eventDateManager = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _eventDateManager = new EventDateManager(new EventDateAccessorFake());
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/29
        /// 
        /// Description:
        /// Test that returns true if a date was created for the event
        /// </summary>
        [TestMethod]
        public void TestCreateEvenDateReturnsTrueIfCreated()
        {
            // arrange
            EventDate eventDate = new EventDate()
            {
                EventDateID = DateTime.Now,
                EventID = 99,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(8),
                Active = true
            };
            const bool expected = true;
            bool actual;

            // act
            actual = _eventDateManager.CreateEventDate(eventDate);

            // assert
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/29
        /// 
        /// Description:
        /// Test that throws exception if duplicate date for event
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateEvenDateThrowsExceptionIfDuplicateDateForEvent()
        {
            // arrange
            EventDate eventDate = new EventDate()
            {
                EventDateID = new DateTime(2022, 01, 01),
                EventID = 1,
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            };
                        
            // act
            _eventDateManager.CreateEventDate(eventDate);

            // assert
            // exception testing, nothing to assert

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/29
        /// 
        /// Description:
        /// Test that that show manager throws an application exception if the EventDateID is null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateEvenDateThrowsExceptionForNullEventDateID()
        {
            // arrange            
            EventDate eventDate = new EventDate()
            {
                //EventDateID not set and is therefor null
                EventID = 1,
                StartTime = new DateTime(2022, 01, 01, 8, 0, 0),
                EndTime = new DateTime(2022, 01, 01, 20, 0, 0),
                Active = true
            };

            // act
            _eventDateManager.CreateEventDate(eventDate);

            // assert
            // exception testing, nothing to assert

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Test that shows that retrieve event dates by event id returns the correct amount of event dates
        /// </summary>
        [TestMethod]
        public void TestRetrieveEventDatesByEventIDReturnsCorrectListOfDatesForEvent()
        {
            // arrange
            const int eventID = 1;
            const int expectedCount = 3;
            List<EventDate> eventDateList = null;
            int actualCount;

            // act
            eventDateList = _eventDateManager.RetrieveEventDatesByEventID(eventID);
            actualCount = eventDateList.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/29
        /// 
        /// Description:
        /// Test that shows that retrieve event dates by event id throws an exception if the event dates list is empty
        /// </summary>
        /// Update:
        /// Derrick Nagy
        /// Created: 2022/02/06/2022
        /// 
        /// Description:
        /// Throwing an exception is not the desired behavior
        //[TestMethod]
        //[ExpectedException(typeof(ApplicationException))]
        //public void TestRetrieveEventDatesByEventIDThrowsExceptionForEmptyList()
        //{
        //    // arrange
        //    const int eventID = -1;


        //    // act
        //    _eventDateManager.RetrieveEventDatesByEventID(eventID);

        //    // assert
        //    // exception testing, nothing to assert

        //}


        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/29
        /// 
        /// Description:
        /// Test that shows that retrieve event dates by event id throws an exception if the event dates list is empty
        /// </summary>
        [TestMethod]
        public void TestRetrieveEventDatesByEventIDReturnsEmptyListForEmptyList()
        {
            // arrange
            const int eventID = -1;
            const int expectedCount = 0;
            List<EventDate> eventDateList = null;
            int actualCount;

            // act
            eventDateList = _eventDateManager.RetrieveEventDatesByEventID(eventID);
            actualCount = eventDateList.Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);

        }


    }
}
