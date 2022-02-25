using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using LogicLayer;
using LogicLayerInterfaces;
using DataAccessFakes;
using DataObjects;

namespace LogicLayerTests
{
    [TestClass]
    public class SublocationManagerTests
    {
        private ISublocationManager _supplierManager = null;

        /// <summary>
        /// Christopher Repko
        /// Created 2022/02/24
        /// 
        /// Description:
        /// Initialization method. Creates a SublocationManager for testing.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _supplierManager = new SublocationManager(new SublocationAccessorFake());
        }

        /// <summary>
        /// Christopher Repko
        /// Created 2022/02/24
        /// 
        /// Description:
        /// Tests that RetrieveSublocationBySublocationID returns the correct sublocation for a sublocationID
        /// </summary>
        [TestMethod]
        public void TestRetrieveSublocationBySublocationIDRetrievesCorrectSublocation()
        {
            //arrange
            const int sublocationID = 1000001;
            const int expectedLocationID = 1000000;
            string expectedName = "Fake Sublocation 1";
            string expectedDescription = "The first fake sublocation";
            Sublocation result;

            //act
            result = _supplierManager.RetrieveSublocationBySublocationID(sublocationID);

            //assert
            Assert.AreEqual(expectedLocationID, result.LocationID);
            Assert.AreEqual(expectedName, result.SublocationName);
            Assert.AreEqual(expectedDescription, result.SublocationDescription);
        }

        /// <summary>
        /// Christopher Repko
        /// Created 2022/02/24
        /// 
        /// Description:
        /// Tests that RetrieveSublocationBySublocationID returns null if there is no sublocation with the passed sublocation ID
        /// </summary>
        [TestMethod]
        public void TestRetrieveSublocationBySublocationIDRetrievesNullOnBadSublocationID()
        {
            //arrange
            const int sublocationID = 10000099;
            Sublocation result;

            //act
            result = _supplierManager.RetrieveSublocationBySublocationID(sublocationID);

            //assert
            Assert.IsNull(result);
        }

        /// <summary>
        /// Christopher Repko
        /// Created 2022/02/24
        /// 
        /// Description:
        /// Tests that RetrieveSublocationsByLocationID retrieves the correct sublocation list for a location ID
        /// </summary>
        [TestMethod]
        public void TestRetrieveSublocationsByLocationIDRetrievesCorrectSublocations()
        {
            //arrange
            const int locationID = 1000000;
            List<Sublocation> expected = new List<Sublocation>();
            expected.Add(new Sublocation()
            {
                SublocationID = 1000001,
                LocationID = 1000000,
                SublocationName = "Fake Sublocation 1",
                SublocationDescription = "The first fake sublocation"
            });

            expected.Add(new Sublocation()
            {
                SublocationID = 1000002,
                LocationID = 1000000,
                SublocationName = "Fake Sublocation 3",
                SublocationDescription = "The third fake sublocation"
            });

            expected.Add(new Sublocation()
            {
                SublocationID = 1000004,
                LocationID = 1000000,
                SublocationName = "Fake Sublocation 5",
                SublocationDescription = "The fith fake sublocation"
            });
            List<Sublocation> result;

            //act
            result = _supplierManager.RetrieveSublocationsByLocationID(locationID);

            //assert
            for(int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(expected[i].LocationID, result[i].LocationID);
                Assert.AreEqual(expected[i].SublocationName, result[i].SublocationName);
                Assert.AreEqual(expected[i].SublocationDescription, result[i].SublocationDescription);
            }
        }

        /// <summary>
        /// Christopher Repko
        /// Created 2022/02/24
        /// 
        /// Description:
        /// Tests that RetrieveSublocationsByLocationID returns an empty list when passed a location ID with no sublocationIDs
        /// </summary>
        [TestMethod]
        public void TestRetrieveSublocationsByLocationIDRetrievesEmptyListOnBadSublocationID()
        {
            //arrange
            const int locationID = 10000099;
            List<Sublocation> result;

            //act
            result = _supplierManager.RetrieveSublocationsByLocationID(locationID);

            //assert
            Assert.IsTrue(result.Count == 0);
        }
    }
}
