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
            const int locationID = 1000000;
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
            result = _sublocationManager.RetrieveSublocationBySublocationID(sublocationID);

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
            result = _sublocationManager.RetrieveSublocationBySublocationID(sublocationID);

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
            result = _sublocationManager.RetrieveSublocationsByLocationID(locationID);

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
            result = _sublocationManager.RetrieveSublocationsByLocationID(locationID);

            //assert
            Assert.IsTrue(result.Count == 0);

        }
    }
}
