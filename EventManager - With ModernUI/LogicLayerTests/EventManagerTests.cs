using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessFakes;
using LogicLayer;

namespace LogicLayerTests
{
    [TestClass]
    public class EventManagerTests
    {

        private IEventManager _eventManager = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _eventManager = new EventManager(new EventAccessorFake());
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Test that returns number of rows added(should be 1)
        /// </summary>
        [TestMethod]
        public void TestCreateEventReturnsOneIfCreated()
        {
            // arrange
            const string eventName = "Test";
            const string eventDescription = "Test Description";
            const int expected = 1;
            int acutal = 0;

            // act
            acutal = _eventManager.CreateEvent(eventName, eventDescription);

            // assert

            Assert.AreEqual(expected, acutal);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Test that throws an application exception if there is no name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateEventThrowsApplicationExceptionIfNoName()
        {
            // arrange
            const string eventName = "";
            const string eventDescription = "Test Description";

            // act
            _eventManager.CreateEvent(eventName, eventDescription);

            // assert
            // nothing to assert, exception testing
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Test that throws an application exception if name is too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateEventThrowsApplicationExceptionIfNameOver50Char()
        {
            // arrange
            const string eventName = "jDWAAHKGh6r3JQwRW7IPVHDJunFb8b5tfgYfGz8vauaNJ2tM1z";
            const string eventDescription = "Test Description";

            // act
            _eventManager.CreateEvent(eventName, eventDescription);

            // assert
            // nothing to assert, exception testing

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Test that throws an application exception if the description is an empty string or null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateEventThrowsApplicationExceptionIfNoDescription()
        {
            // arrange
            const string eventName = "Test Event";
            const string eventDescription = "";

            // act
            _eventManager.CreateEvent(eventName, eventDescription);

            // assert
            // nothing to assert, exception testing

        }


        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Test that throws an application exception if the description is too long, over 1000 char
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateEventThrowsApplicationExceptionIfDescriptionTooLong()
        {
            // arrange
            const string eventName = "Test Event";
            const string eventDescription = "CG7RqtCb0qMq3CUwXSTQvuOKIVZgUdUS33qHKpKqXwHhdAJx1pVLwRWvWd2Y24v1RGQjMqwGMpbMhavquvuyitARU2Omrm4gIPSWaS0TCpbo4oI83WKvWSW2qOPk9SFUK77x48NVmp0QbCts1KrbDx01v4g1iSwumHHNmg33vo6GpUKhU3j2iYnaXyncIGiVrRoqDCVUhU7qwwioIMBuYASWnsWMMsKVNROFlwQkzdDWS5zRDpiEnKXMcXIcfbm9VIKbWYh2j1uozqNgRcxv6DbxDgC9CyVAToCuYuURoBrfK3k5ClHIGAmpeHM6S9aIwDJ3rtesuprRrjd4K2t5ZrtuRsLO8ZtnQz2SrZntqBRJqjf9d5GGjvM2tfq5Tq94AS075HGUXg7da1swsTgj8zRB31TcW4jZ98rXlyIiwsvJn06UJWybWveN2NM9LGqOyd6jL0IzkXYMBhm5wN8vqxvpUPYayChPgEDITBr0WVahkd8Ev0SVn89finbKKSCTcNLWMijRBZ5lo7pOiLz2j7RTwRDjIxPECGK5efJcicLU8E4hAwmaX5AJbtxXLEF5m1mkPbSinzsS4Nl5YvP7lDkhjdtGK9DwfGmXAsXMSPY42r2cTBpbSptU0w9XVgMNWUe9V4Use0aPu5ZahBLjwFy3gDCOW7L7vh75HawP1I4BctBgHc6csf8Kdhq70LKtTvkEPO7vdnmaQXTfSugSRgzQ2JZiTAmRrnT40nK4X5whoi63g7PKrXAuqVsgOrUkWlgtmWRjXfdTRWRPMewPoUCFwe6DuJ22okqUiQ0t1ZlvqlfzhZfyya4GPsZeZD7Fs5203eEZq3dvhJWZ6BpRgrJ168yZNBfHBDFKVdyBd0epClPSG4A0O38RVDkiuiFtuYwBtSs7o8VOfhGcxm01XzsTWIbcbXUc6HX4qfGCfXxBe4OfeC6pobrTpL2o8G6DGcOYExHjHHCSKyOs9Y4LT36lkiCOCet2Pp0ALA8r";

            // act
            _eventManager.CreateEvent(eventName, eventDescription);

            // assert
            // nothing to assert, exception testing
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Test that returns list of active events (currently 4 in fake data)
        /// </summary>

        [TestMethod]
        public void TestRetrieveActiveEventsReturnsCorrectList()
        {
            // arrange
            const int expectedCount = 4;
            int actualCount;
            // act
            actualCount = (_eventManager.RetreieveActiveEvents().Count);

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/01/22
        /// 
        /// Description:
        /// Test that throws an application exception if the event name for update is 
        /// an empty string or null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateEventThrowsApplicationExceptionIfNoName()
        {
            // arrange
            Event oldEvent = new Event()
            {
                EventID = 1000000,
                EventName = "Test Event 1",
                EventDescription = "A description of test event 1",
                Active = true
            };

            Event newEvent = new Event()
            {
                EventID = 1000000,
                EventName = "",
                EventDescription = "A description of test event 1",
                Active = true
            };

            // act
            _eventManager.UpdateEvent(oldEvent, newEvent);

            // assert
            // nothing to assert, exception testing
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Test that throws an application exception if event name for update is too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateEventThrowsApplicationExceptionIfNameOver50Char()
        {
            // arrange
            Event oldEvent = new Event()
            {
                EventID = 1000000,
                EventName = "Test Event 1",
                EventDescription = "A description of test event 1",
                Active = true
            };

            Event newEvent = new Event()
            {
                EventID = 1000000,
                EventName = "jDWAAHKGh6r3JQwRW7IPVHDJunFb8b5tfgYfGz8vauaNJ2tM1z",
                EventDescription = "A description of test event 1",
                Active = true
            };

            // act
            _eventManager.UpdateEvent(oldEvent, newEvent);

            // assert
            // nothing to assert, exception testing

        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Test that throws an application exception if the description for update 
        /// is an empty string or null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateEventThrowsApplicationExceptionIfNoDescription()
        {
            // arrange
            Event oldEvent = new Event()
            {
                EventID = 1000000,
                EventName = "Test Event 1",
                EventDescription = "A description of test event 1",
                Active = true
            };

            Event newEvent = new Event()
            {
                EventID = 1000000,
                EventName = "Test Event 1",
                EventDescription = "",
                Active = true
            };

            // act
            _eventManager.UpdateEvent(oldEvent, newEvent);

            // assert
            // nothing to assert, exception testing

        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Test that throws an application exception if the description for update 
        /// is too long, over 1000 char
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateEventThrowsApplicationExceptionIfDescriptionTooLong()
        {
            // arrange
            Event oldEvent = new Event()
            {
                EventID = 1000000,
                EventName = "Test Event 1",
                EventDescription = "A description of test event 1",
                Active = true
            };

            Event newEvent = new Event()
            {
                EventID = 1000000,
                EventName = "Test Event 1",
                EventDescription = "CG7RqtCb0qMq3CUwXSTQvuOKIVZgUdUS33qHKpKqXwHhdAJx1pVLwRWvWd2Y24v1RGQjMqwGMpbMhavquvuyitARU2Omrm4gIPSWaS0TCpbo4oI83WKvWSW2qOPk9SFUK77x48NVmp0QbCts1KrbDx01v4g1iSwumHHNmg33vo6GpUKhU3j2iYnaXyncIGiVrRoqDCVUhU7qwwioIMBuYASWnsWMMsKVNROFlwQkzdDWS5zRDpiEnKXMcXIcfbm9VIKbWYh2j1uozqNgRcxv6DbxDgC9CyVAToCuYuURoBrfK3k5ClHIGAmpeHM6S9aIwDJ3rtesuprRrjd4K2t5ZrtuRsLO8ZtnQz2SrZntqBRJqjf9d5GGjvM2tfq5Tq94AS075HGUXg7da1swsTgj8zRB31TcW4jZ98rXlyIiwsvJn06UJWybWveN2NM9LGqOyd6jL0IzkXYMBhm5wN8vqxvpUPYayChPgEDITBr0WVahkd8Ev0SVn89finbKKSCTcNLWMijRBZ5lo7pOiLz2j7RTwRDjIxPECGK5efJcicLU8E4hAwmaX5AJbtxXLEF5m1mkPbSinzsS4Nl5YvP7lDkhjdtGK9DwfGmXAsXMSPY42r2cTBpbSptU0w9XVgMNWUe9V4Use0aPu5ZahBLjwFy3gDCOW7L7vh75HawP1I4BctBgHc6csf8Kdhq70LKtTvkEPO7vdnmaQXTfSugSRgzQ2JZiTAmRrnT40nK4X5whoi63g7PKrXAuqVsgOrUkWlgtmWRjXfdTRWRPMewPoUCFwe6DuJ22okqUiQ0t1ZlvqlfzhZfyya4GPsZeZD7Fs5203eEZq3dvhJWZ6BpRgrJ168yZNBfHBDFKVdyBd0epClPSG4A0O38RVDkiuiFtuYwBtSs7o8VOfhGcxm01XzsTWIbcbXUc6HX4qfGCfXxBe4OfeC6pobrTpL2o8G6DGcOYExHjHHCSKyOs9Y4LT36lkiCOCet2Pp0ALA8r",
            Active = true
            };

            // act
            _eventManager.UpdateEvent(oldEvent, newEvent);

            // assert
            // nothing to assert, exception testing

        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Test that returns true if update successful
        /// </summary>
        [TestMethod]
        public void TestUpdateEventReturnsTrueIfUpdateSuccessful()
        {
            // arrange
            Event oldEvent = new Event()
            {
                EventID = 1000000,
                EventName = "Test Event 1",
                EventDescription = "A description of test event 1",
                Active = true
            };

            Event newEvent = new Event()
            {
                EventID = 1000000,
                EventName = "Test Name",
                EventDescription = "Description",
                Active = false
            };

            bool expected = true;
            bool actual;

            // act
            actual = _eventManager.UpdateEvent(oldEvent, newEvent);

            // assert

            Assert.AreEqual(expected, actual);
        }

        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Test that returns an event object and tests to see if it is the correct ID
        /// </summary>
        [TestMethod]
        public void TestRetrieveEventByEventNameAndDescriptionReturnsCorrectEventID()
        {
            // arrange
            const string eventName = "Test Event 1";
            const string eventDescription = "A description of test event 1";
            const int expectedEventID = 1000000;
            Event actualEvent = null;
            int actualEventID;

            // act
            actualEvent = _eventManager.RetrieveEventByEventNameAndDescription(eventName, eventDescription);
            actualEventID = actualEvent.EventID;
            // assert

            Assert.AreEqual(expectedEventID, actualEventID);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/01/30
        /// 
        /// Description:
        /// Test that returns and event object and tests to see if it is the correct ID
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestRetrieveEventByEventNameAndDescriptionThrowsExceptionWhenNotFound()
        {
            // arrange
            const string eventName = "Test Event xxxxxxxxxx";
            const string eventDescription = "A description of test event 1";
            
            // act
            _eventManager.RetrieveEventByEventNameAndDescription(eventName, eventDescription);
            
            // assert
            // notthing to assert, exception testing
            
        }
    }
}
