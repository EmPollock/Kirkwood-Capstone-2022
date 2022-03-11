using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessFakes;
using LogicLayer;

namespace LogicLayerTests
{

    [TestClass]
    public class EntranceManagerTests
    {
        private IEntranceManager _entranceManager = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _entranceManager = new EntranceManager(new EntranceAccessorFake());
        }

        /// <summary>
        /// Alaina Gilson
        /// Created 2022/02/27
        /// 
        /// Description:
        /// Test that returns number of rows added, should be one
        /// </summary>
        [TestMethod]
        public void TestCreateEntranceReturnsOneIfCreated()
        {
            // arrange
            const int locationID = 100000;
            const string entranceName = "Test";
            const string description = "Test Description";
            const int expected = 1;
            int acutal = 0;

            // act
            acutal = _entranceManager.CreateEntrance(locationID, entranceName, description);

            // assert

            Assert.AreEqual(expected, acutal);
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/02/27
        /// 
        /// Description:
        /// Test that throws an application exception if there is no name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateEntranceThrowsApplicationExceptionIfNoName()
        {
            // arrange
            const int locationID = 100000;
            const string entranceName = "";
            const string description = "Test Description";

            // act
            _entranceManager.CreateEntrance(locationID, entranceName, description);

            // assert
            // nothing to assert, exception testing
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/02/27
        /// 
        /// Description:
        /// Test that throws an application exception if there is no description
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateEntranceThrowsApplicationExceptionIfNoDescription()
        {
            // arrange
            const int locationID = 100000;
            const string entranceName = "Test";
            const string description = "";

            // act
            _entranceManager.CreateEntrance(locationID, entranceName, description);

            // assert
            // nothing to assert, exception testing
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/02/27
        /// 
        /// Description:
        /// Test that throws an application exception if name is over 100 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateEntranceThrowsApplicationExceptionIfNameOver100Characters()
        {
            // arrange
            const int locationID = 100000;
            const string entranceName = "fhjdkslahfjksdlahfueiwajfdksabfieuawhfdsjkalbfjdsalhfueilajsdklafdjkslahfjlehauijdskaflhjiehiaufhdsajklhdfjkslahfeilwa";
            const string description = "Test Description";

            // act
            _entranceManager.CreateEntrance(locationID, entranceName, description);

            // assert
            // nothing to assert, exception testing
        }

        /// <summary>
        /// Alaina Gilson
        /// Created: 2022/02/27
        /// 
        /// Description:
        /// Test that throws an application exception if description is over 255 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateEntranceThrowsApplicationExceptionIfDescriptionOver255Characters()
        {
            // arrange
            const int locationID = 100000;
            const string entranceName = "Test";
            const string description = "fhjdkslahfjksdlahfueiwajfdksabfieuawhfdsjkalbfjdsalhfueilajsdklafdjkslahfjlehauijdskaflhjiehiaufhdsajklhdfjkslahfeilwafhjdkslahfjksdlahfueiwajfdksabfieuawhfdsjkalbfjdsalhfueilajsdklafdjkslahfjlehauijdskaflhjiehiaufhdsajklhdfjkslahfeilwafhjdkslahfjksdlahfueiwajfdksabfieuawhfdsjkalbfjdsalhfueilajsdklafdjkslahfjlehauijdskaflhjiehiaufhdsajklhdfjkslahfeilwa";

            // act
            _entranceManager.CreateEntrance(locationID, entranceName, description);

            // assert
            // nothing to assert, exception testing
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/03/04
        /// 
        /// Description:
        /// Test method that returns the correct list of entrance(s) for a location
        /// </summary>
        [TestMethod]
        public void TestRetrieveEntranceByLocationIDReturnsCorrectList()
        {
            // arrange
            int locationID = 100000;
            const int expectedResult = 2;
            int actualResult;

            // act
            actualResult = _entranceManager.RetrieveEntranceByLocationID(locationID).Count;

            // assert
            Assert.AreEqual(expectedResult, actualResult);

        }
    }
}
