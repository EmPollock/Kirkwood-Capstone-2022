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
                LocationName = "Fake Location One",
                LocationDescription = "The number one fake location around",
                LocationPricingText = "One day rent: $20",
                LocationPhone = "3193193193",
                LocationEmail = "fakeLocation@gmail.com",
                LocationAddress1 = "Fake Address Number 1",
                LocationCity = "Fake City",
                LocationState = "IA",
                LocationZipCode = "52206",
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
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Test that returns number of rows added(should be 1)
        /// </summary>
        [TestMethod]
        public void TestCreateLocationReturnsOneRowAffected()
        {
            //arrange
            const string locationName = "Cedar Park";
            const string locationAddress1 = "777 Cool St";
            const string locationCity = "Marshalltown";
            const string locationState = "Iowa";
            const string locationZip = "50158";
            const int expected = 1;
            int actual = 0;
            //act
            actual = _locationManager.CreateLocation(locationName, locationAddress1, locationCity, locationState, locationZip);
            //assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Test that throws an application exception if there is no location name
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateLocationThrowsApplicationExceptionIfNoLocationName()
        {
            //arrange
            const string locationName = "";
            const string locationAddress1 = "777 Cool St";
            const string locationCity = "Marshalltown";
            const string locationState = "Iowa";
            const string locationZip = "50158";
            //act
            _locationManager.CreateLocation(locationName, locationAddress1, locationCity, locationState, locationZip);
            //assert

        }


        /// <summary>
        /// Logan Baccam
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Test that throws an application exception if the location name exceed 160 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateLocationThrowsApplicationExceptionIfNameOver160Char()
        {
            //arrange
            const string locationName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaz";
            const string locationAddress1 = "777 Cool St";
            const string locationCity = "Marshalltown";
            const string locationState = "Iowa";
            const string locationZip = "50158";
            //act
            _locationManager.CreateLocation(locationName, locationAddress1, locationCity, locationState, locationZip);
            //assert
        }


        /// <summary>
        /// Logan Baccam
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Test that throws an application exception if the location address is missing
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateLocationThrowsApplicationExceptionIfNoAddress()
        {
            //arrange
            const string locationName = "Cedar Park";
            const string locationAddress1 = "";
            const string locationCity = "Marshalltown";
            const string locationState = "Iowa";
            const string locationZip = "50158";
            //act
            _locationManager.CreateLocation(locationName, locationAddress1, locationCity, locationState, locationZip);
            //assert
        }
        /// <summary>
        /// Logan Baccam
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Test that throws an application exception if the location address exceeds 100 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateLocationThrowsApplicationExceptionIfAddressOver100Char()
        {
            //arrange
            const string locationName = "Cedar Park";
            const string locationCity = "Marshalltown";
            const string locationState = "Iowa";
            const string locationZip = "50158";
            const string locationAddress1 = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                                            "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            //act
            _locationManager.CreateLocation(locationName, locationAddress1, locationCity, locationState, locationZip);
            //assert
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/01/26
        /// 
        /// Description:
        /// Test that returns a matching location
        /// </summary>
        [TestMethod]
        public void TestRetrieveLocationByNameAndAddressReturnsCorrectLocation()
        {
            //arrange
            Location _matchingLocation = new Location();
            const int expected = 100002;
            const string locationName = "Test Location 3";
            const string address = "Test Location 3 Street";
            int actual;

            //act
            _matchingLocation = _locationManager.RetrieveLocationByNameAndAddress(locationName, address);
            actual = _matchingLocation.LocationID;
            //assert
            Assert.AreEqual(expected, actual);

        }
    }
}
