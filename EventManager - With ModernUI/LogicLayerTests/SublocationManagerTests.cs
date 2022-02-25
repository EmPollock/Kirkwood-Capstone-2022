using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataObjects;
using LogicLayerInterfaces;
using DataAccessFakes;
using LogicLayer;

namespace LogicLayerTests
{
    [TestClass]
    public class SublocationManagerTests
    {
        private ISublocationManager _sublocationManager = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _sublocationManager = new SublocationManager(new SublocationAccessorFake());
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/22
        /// 
        /// Description:
        /// Test that checks the amount of sublocations returned with the given locationID
        /// </summary>
        [TestMethod]
        public void TestRetrieveSublocationByLocationIDReturnsCorrectAmount()
        {
            // arrange
            const int locationID = 100000;
            const int expectedCount = 3;
            int actualCount;

            // act
            actualCount = _sublocationManager.RetrieveSublocationsByLocationID(locationID).Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/22
        /// 
        /// Description:
        /// Test that checks the amount of sublocations returned with a bad locationID
        /// </summary>
        [TestMethod]
        public void TestRetrieveSublocationByLocationIDWithBadLocationIDReturnsNoSublocations()
        {
            // arrange
            const int locationID = 9999999;
            const int expectedCount = 0;
            int actualCount;

            // act
            actualCount = _sublocationManager.RetrieveSublocationsByLocationID(locationID).Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
