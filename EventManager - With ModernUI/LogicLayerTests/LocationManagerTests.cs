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
    public class LocationManagerTests
    {
        private ILocationManager _locationManager = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _locationManager = new LocationManager(new LocationAccessorFake());
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Test that makes sure retrieve active locations returns a list with the correct number of locations
        /// </summary>
        [TestMethod]
        public void TestRetrieveActiveSuppliersReturnsCorrectList()
        {
            // arrange
            const int expected = 5;
            int actual;

            // act
            actual = _locationManager.RetrieveActiveLocations().Count;

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/03
        /// 
        /// Description:
        /// Test that returns the location that matches the provided LocationID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        [TestMethod]
        public void TestRetrieveLocationByLocationIDReturnsCorrectLocation()
        {
            int locationID = 100000;
            Location expectedResult = new Location()
            {
                LocationID = 100000,
                UserID = 100000,
                Name = "Fake Location One",
                Description = "The number one fake location around",
                PricingInfo = "One day rent: $20",
                Phone = "3193193193",
                Email = "fakeLocation@gmail.com",
                Address1 = "Fake Address Number 1",
                City = "Fake City",
                State = "IA",
                ZipCode = "52206",
                Active = true
            };
            Location actualResult;

            actualResult = _locationManager.RetrieveLocationByLocationID(locationID);

            Assert.AreEqual(expectedResult.LocationID, actualResult.LocationID);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/04
        /// 
        /// Description:
        /// Test that returns the count of location reviews that match the provided LocationID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        [TestMethod]
        public void TestRetrieveLocationReviewsReturnsCorrectAmount()
        {
            int locationID = 100000;
            int expectedCount = 2;
            int actualCount;

            actualCount = _locationManager.RetrieveLocationReviews(locationID).Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/02/05
        /// 
        /// Description:
        /// Test that returns the count of location images that match the provided locationID
        /// </summary>
        ///
        /// <remarks>
        /// Updater Name
        /// Updated: yyyy/mm/dd
        /// </remarks>
        [TestMethod]
        public void TestRetrieveLocationImagesReturnsCorrectAmount()
        {
            int locationID = 100000;
            int expectedCount = 2;
            int actualCount;

            actualCount = _locationManager.RetrieveLocationImages(locationID).Count;

            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
