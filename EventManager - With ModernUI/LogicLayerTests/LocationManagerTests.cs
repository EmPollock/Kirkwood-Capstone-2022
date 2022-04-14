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
        public void TestRetrieveActiveLocationsReturnsCorrectList()
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
        [TestMethod]
        public void TestRetrieveLocationImagesReturnsCorrectAmount()
        {
            int locationID = 100000;
            int expectedCount = 2;
            int actualCount;

            actualCount = _locationManager.RetrieveLocationImagesByLocationID(locationID).Count;

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

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/02/24
        /// 
        /// Description:
        /// Test that returns the count of rows affected after deactivating a location
        /// </summary>
        [TestMethod]
        public void TestDeactivateLocationReturnsCorrectAmount()
        {
            // arrange
            int locationID = 100000;
            int expectedCount = 1;
            int actualCount;

            // act
            actualCount = _locationManager.DeactivateLocationByLocationID(locationID);

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// Kris Howell
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Test to make sure that RetrieveLocationAvailabilityByLocationIDAndDate correctly prioritizes an exception
        /// availability over a regular availability on the same date
        /// </summary>
        [TestMethod]
        public void TestRetrieveLocationAvailabilityByLocationIDAndDatePrioritizesExceptions()
        {
            // arrange
            const int locationID = 100001;
            DateTime date = new DateTime(2022, 01, 01);
            List<Availability> actualList;
            List<Availability> expectedList = new List<Availability>()
            {
                new Availability()
                {
                    ForeignID = 100001,
                    AvailabilityID = 100004,
                    TimeStart = new DateTime(2022, 01, 01, 2, 45, 00),
                    TimeEnd = new DateTime(2022, 01, 01, 4, 45, 00)
                }
            };

            // act
            actualList = _locationManager.RetrieveLocationAvailabilityByLocationIDAndDate(locationID, date);

            // assert
            // collection assert does not seem to be able to compare reference variables properly
            // this properly proves that the correct list is being chosen
            Assert.AreEqual(expectedList.First<Availability>().TimeStart, actualList.First().TimeStart);
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Test to make sure that RetrieveLocationAvailabilityByLocationIDAndDate correctly returns the right
        /// regular availabilities when there are no exception availabilities on that date for that location.
        /// </summary>
        [TestMethod]
        public void TestRetrieveLocationAvailabilityByLocationIDAndDateCorrectList()
        {
            // arrange
            const int locationID = 100000;
            DateTime date = new DateTime(2022, 01, 01);
            int actualCount;
            int expectedCount = 2;

            // act
            actualCount = _locationManager.RetrieveLocationAvailabilityByLocationIDAndDate(locationID, date).Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Test that returns the count of rows affected after updating a location description
        /// </summary>
        [TestMethod]
        public void TestUpdateLocationDescriptionReturnsCorrectAmount()
        {
            // arrange
            Location oldLocation = new Location() 
            {
                LocationID = 100000,
                Description = "Description of Test Location 1 goes here.",
                Phone = "111-111-1111",
                Email = "testLocation1@locations.com",
                Address1 = "Test Location 1 Street",
                PricingInfo = "Pricing information for renting Test Location 1 goes here."
            };

            Location newLocation = new Location()
            {
                LocationID = 100000,
                Description = "Description of Test Location",
                Phone = "111-111-1111",
                Email = "testLocation1@locations.com",
                Address1 = "Test Location 1 Street",
                PricingInfo = "Pricing information for renting Test Location 1 goes here."
            };
            
            int expectedCount = 1;
            int actualCount;

            // act
            actualCount = _locationManager.UpdateLocationBioByLocationID(oldLocation, newLocation);

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/03/18
        /// 
        /// Description:
        /// Test that returns the count of rows affected after updating a location phone number
        /// </summary>
        [TestMethod]
        public void TestUpdateLocationPhoneReturnsCorrectAmount()
        {
            // arrange
            Location oldLocation = new Location()
            {
                LocationID = 100000,
                Description = "Description of Test Location 1 goes here.",
                Phone = "111-111-1111",
                Email = "testLocation1@locations.com",
                Address1 = "Test Location 1 Street",
                PricingInfo = "Pricing information for renting Test Location 1 goes here."
            };

            Location newLocation = new Location()
            {
                LocationID = 100000,
                Description = "Description of Test Location 1 goes here.",
                Phone = "111-111-1112",
                Email = "testLocation1@locations.com",
                Address1 = "Test Location 1 Street",
                PricingInfo = "Pricing information for renting Test Location 1 goes here."
            };

            int expectedCount = 1;
            int actualCount;

            // act
            actualCount = _locationManager.UpdateLocationBioByLocationID(oldLocation, newLocation);

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/03/18
        /// 
        /// Description:
        /// Test that returns the count of rows affected after updating a location email
        /// </summary>
        [TestMethod]
        public void TestUpdateLocationEmailReturnsCorrectAmount()
        {
            // arrange
            Location oldLocation = new Location()
            {
                LocationID = 100000,
                Description = "Description of Test Location 1 goes here.",
                Phone = "111-111-1111",
                Email = "testLocation1@locations.com",
                Address1 = "Test Location 1 Street",
                PricingInfo = "Pricing information for renting Test Location 1 goes here."
            };

            Location newLocation = new Location()
            {
                LocationID = 100000,
                Description = "Description of Test Location 1 goes here.",
                Phone = "111-111-1111",
                Email = "xtestLocation1@locations.com",
                Address1 = "Test Location 1 Street",
                PricingInfo = "Pricing information for renting Test Location 1 goes here."
            };

            int expectedCount = 1;
            int actualCount;

            // act
            actualCount = _locationManager.UpdateLocationBioByLocationID(oldLocation, newLocation);

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/03/18
        /// 
        /// Description:
        /// Test that returns the count of rows affected after updating a location address 1
        /// </summary>
        [TestMethod]
        public void TestUpdateLocationAddress1ReturnsCorrectAmount()
        {
            // arrange
            Location oldLocation = new Location()
            {
                LocationID = 100000,
                Description = "Description of Test Location 1 goes here.",
                Phone = "111-111-1111",
                Email = "testLocation1@locations.com",
                Address1 = "Test Location 1 Street",
                PricingInfo = "Pricing information for renting Test Location 1 goes here."
            };

            Location newLocation = new Location()
            {
                LocationID = 100000,
                Description = "Description of Test Location 1 goes here.",
                Phone = "111-111-1111",
                Email = "testLocation1@locations.com",
                Address1 = "xTest Location 1 Street",
                PricingInfo = "Pricing information for renting Test Location 1 goes here."
            };

            int expectedCount = 1;
            int actualCount;

            // act
            actualCount = _locationManager.UpdateLocationBioByLocationID(oldLocation, newLocation);

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/03/18
        /// 
        /// Description:
        /// Test that returns the count of rows affected after updating a location address 2
        /// </summary>
        [TestMethod]
        public void TestUpdateLocationAddress2ReturnsCorrectAmount()
        {
            // arrange
            Location oldLocation = new Location()
            {
                LocationID = 100000,
                Description = "Description of Test Location 1 goes here.",
                Phone = "111-111-1111",
                Email = "testLocation1@locations.com",
                Address1 = "Test Location 1 Street",
                PricingInfo = "Pricing information for renting Test Location 1 goes here."
            };

            Location newLocation = new Location()
            {
                LocationID = 100000,
                Description = "Description of Test Location 1 goes here.",
                Phone = "111-111-1111",
                Email = "testLocation1@locations.com",
                Address1 = "Test Location 1 Street",
                Address2 = "apt 1",
                PricingInfo = "Pricing information for renting Test Location 1 goes here."
            };

            int expectedCount = 1;
            int actualCount;

            // act
            actualCount = _locationManager.UpdateLocationBioByLocationID(oldLocation, newLocation);

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/03/18
        /// 
        /// Description:
        /// Test that returns the count of rows affected after updating a location pricing
        /// </summary>
        [TestMethod]
        public void TestUpdateLocationPricingReturnsCorrectAmount()
        {
            // arrange
            Location oldLocation = new Location()
            {
                LocationID = 100000,
                Description = "Description of Test Location 1 goes here.",
                Phone = "111-111-1111",
                Email = "testLocation1@locations.com",
                Address1 = "Test Location 1 Street",
                PricingInfo = "Pricing information for renting Test Location 1 goes here."
            };

            Location newLocation = new Location()
            {
                LocationID = 100000,
                Description = "Description of Test Location 1 goes here.",
                Phone = "111-111-1111",
                Email = "testLocation1@locations.com",
                Address1 = "Test Location 1 Street",
                PricingInfo = "Pricing information for renting Test Location 1."
            };

            int expectedCount = 1;
            int actualCount;

            // act
            actualCount = _locationManager.UpdateLocationBioByLocationID(oldLocation, newLocation);

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/03/18
        /// 
        /// Description:
        /// Test that throws an exception if the desceiption is too long
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateDescriptionThrowsExceptionTooLong()
        {
            // arrange
            Location oldLocation = new Location()
            {
                LocationID = 100000,
                Description = "Description of Test Location 1 goes here.",
                Phone = "111-111-1111",
                Email = "testLocation1@locations.com",
                Address1 = "Test Location 1 Street",
                PricingInfo = "Pricing information for renting Test Location 1 goes here."
            };

            Location newLocation = new Location()
            {
                LocationID = 100000,
                Description = "Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.Description of Test Location 1 goes here.",
                Phone = "111-111-1111",
                Email = "testLocation1@locations.com",
                Address1 = "Test Location 1 Street",
                PricingInfo = "Pricing information for renting Test Location 1 goes here."
            };

            int actualCount;

            // act
            actualCount = _locationManager.UpdateLocationBioByLocationID(oldLocation, newLocation);

            // assert
            // nothing to do, exception expected
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/03/18
        /// 
        /// Description:
        /// Test that throws an exception for an invalid phone number format
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateLocationPhoneThrowsExceptionInvalidFormat()
        {
            // arrange
            Location oldLocation = new Location()
            {
                LocationID = 100000,
                Description = "Description of Test Location 1 goes here.",
                Phone = "111-111-1111",
                Email = "testLocation1@locations.com",
                Address1 = "Test Location 1 Street",
                PricingInfo = "Pricing information for renting Test Location 1 goes here."
            };

            Location newLocation = new Location()
            {
                LocationID = 100000,
                Description = "Description of Test Location 1 goes here.",
                Phone = "dogs",
                Email = "testLocation1@locations.com",
                Address1 = "Test Location 1 Street",
                PricingInfo = "Pricing information for renting Test Location 1."
            };

            int actualCount;

            // act
            actualCount = _locationManager.UpdateLocationBioByLocationID(oldLocation, newLocation);

            // assert
            // nothing to do
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/03/18
        /// 
        /// Description:
        /// Test that throws an exception for an invalid email format
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateLocationEmailThrowsExceptionInvalidFormat()
        {
            // arrange
            Location oldLocation = new Location()
            {
                LocationID = 100000,
                Description = "Description of Test Location 1 goes here.",
                Phone = "111-111-1111",
                Email = "testLocation1@locations.com",
                Address1 = "Test Location 1 Street",
                PricingInfo = "Pricing information for renting Test Location 1 goes here."
            };

            Location newLocation = new Location()
            {
                LocationID = 100000,
                Description = "Description of Test Location 1 goes here.",
                Phone = "111-111-1111",
                Email = "dogs",
                Address1 = "Test Location 1 Street",
                PricingInfo = "Pricing information for renting Test Location 1."
            };
            int actualCount;

            // act
            actualCount = _locationManager.UpdateLocationBioByLocationID(oldLocation, newLocation);

            // assert
            // nothing to do
        }

        /// <summary>
        /// Jace Pettinger
        /// Created: 2022/03/18
        /// 
        /// Description:
        /// Test that throws an exception for too long pricing info
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestUpdateLocationPricingThrowsExceptionTooLong()
        {
            // arrange
            Location oldLocation = new Location()
            {
                LocationID = 100000,
                Description = "Description of Test Location 1 goes here.",
                Phone = "111-111-1111",
                Email = "testLocation1@locations.com",
                Address1 = "Test Location 1 Street",
                PricingInfo = "Pricing information for renting Test Location 1 goes here."
            };

            Location newLocation = new Location()
            {
                LocationID = 100000,
                Description = "Description of Test Location 1 goes here.",
                Phone = "111-111-1111",
                Email = "testLocation1@locations.com",
                Address1 = "Test Location 1 Street",
                PricingInfo = "Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1.Pricing information for renting Test Location 1."
            };

            int actualCount;

            // act
            actualCount = _locationManager.UpdateLocationBioByLocationID(oldLocation, newLocation);

            // assert
            // nothing to do
        }

        /// Kris Howell
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Test to make sure that RetrieveLocationAvailabilityByLocationIDAndDate correctly returns an empty
        /// list when no availability is found
        /// </summary>
        [TestMethod]
        public void TestRetrieveLocationAvailabilityByLocationIDAndDateReturnsNullWithNoneFound()
        {
            // arrange
            const int locationID = 100000;
            DateTime badDate = new DateTime(1999, 01, 01);
            List<Availability> actualList;

            // act
            actualList = _locationManager.RetrieveLocationAvailabilityByLocationIDAndDate(locationID, badDate);

            // assert
            Assert.IsFalse(actualList.Any());
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/10
        /// 
        /// Description:
        /// Test to make sure that RetrieveLocationAvailabilityByLocationIDAndDate correctly returns an Availability
        /// object with null TimeStart/TimeEnd properties when there is an exception for a day with no availability
        /// </summary>
        [TestMethod]
        public void TestRetrieveLocationAvailabilityByLocationIDAndDateReturnsEmptyAvailabilityForEmptyException()
        {
            // arrange
            const int locationID = 100000;
            DateTime date = new DateTime(2022, 01, 03);
            DateTime? expected = null;
            DateTime? actual;

            // act
            actual = _locationManager.RetrieveLocationAvailabilityByLocationIDAndDate(locationID, date).First().TimeStart;

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// Test to make sure that RetrieveLocationAvailabilityByLocationID correctly returns an AvailabilityVM list
        /// </summary>
        [TestMethod]
        public void TestRetrieveLocationAvailabilityByLocationIDReturnsCorrectAmount()
        {
            // arrange
            const int locationID = 100000;
            int expectedCount = 3;
            int actualCount;

            // act
            actualCount = _locationManager.RetrieveLocationAvailabilityByLocationID(locationID).Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/11
        /// 
        /// Description:
        /// Test to make sure that RetrieveSupplierAvailabilityBySupplierID correctly returns an AvailabilityVM list
        /// </summary>
        [TestMethod]
        public void TestRetrieveLocationAvailabilityExceptionByLocationIDReturnsCorrectAmount()
        {
            // arrange
            const int locationID = 100000;

            int expectedCount = 1;
            int actualCount;

            // act
            actualCount = _locationManager.RetrieveLocationAvailabilityExceptionByLocationID(locationID).Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
