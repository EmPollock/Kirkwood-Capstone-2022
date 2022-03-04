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
