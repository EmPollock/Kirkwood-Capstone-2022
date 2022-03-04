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
    public class ParkingLotManagerTests
    {

        private IParkingLotManager _parkingLotManager = null;

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Test initializer
        /// 
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            _parkingLotManager = new ParkingLotManager(new ParkingLotAccessorFake());
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Returns Parking Lot View model correctly
        /// 
        /// </summary>
        [TestMethod]
        public void TestReturnParkingLotVMForLocationReturnsCorrectList()
        {
            // arrange
            const int locationID = 100000;
            const int expectedCount = 3;
            List<ParkingLotVM> actualParkingLots = null;
            int acutalCount = 0;

            // act
            actualParkingLots = _parkingLotManager.RetrieveParkingLotByLocationID(locationID);
            acutalCount = actualParkingLots.Count;

            // assert
            Assert.AreEqual(expectedCount, acutalCount);

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Returns 0 Parking Lot View models
        /// 
        /// </summary>
        [TestMethod]
        public void TestReturnParkingLotVMForLocationReturnsEmptyListForLocationID()
        {
            // arrange
            const int locationID = 100099;
            const int expectedCount = 0;
            List<ParkingLotVM> actualParkingLots = null;
            int acutalCount = 0;

            // act
            actualParkingLots = _parkingLotManager.RetrieveParkingLotByLocationID(locationID);
            acutalCount = actualParkingLots.Count;

            // assert
            Assert.AreEqual(expectedCount, acutalCount);
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Tests to see if create parking lot returns an id
        /// 
        /// </summary>
        [TestMethod]
        public void TestCreateParkingLotReturnsLotID()
        {
            // arrange
            ParkingLot parking = new ParkingLot()
            {
                LocationID = 100000,
                Name = "Test",
                Description = "Test Description",
                ImageName = "Lot Image Path"
            };
            const int expectedLotID = 100005;
            int actualLotID;


            // act
            actualLotID = _parkingLotManager.CreateParkingLot(parking);

            // assert
            Assert.AreEqual(expectedLotID, actualLotID);

        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Tests to see if create parking lot throws an exception if there is not location id
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateParkingLotThrowsErrorIfNoLocationID()
        {
            // arrange
            ParkingLot parking = new ParkingLot()
            {
                // No LocationID
                Name = "Test",
                Description = "Test Description",
                ImageName = "Lot Image Path"
            };

            // act
            _parkingLotManager.CreateParkingLot(parking);

            // assert
            // Nothing to assert, exception testing
        }

        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/04
        /// 
        /// Description:
        /// Tests to see if create parking lot throws an exception if there is not a parking lot name
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateParkingLotThrowsErrorIfNoName()
        {
            // arrange
            ParkingLot parking = new ParkingLot()
            {
                LocationID = 100000,
                Name = "",
                Description = "Test Description",
                ImageName = "Lot Image Path"
            };

            // act
            _parkingLotManager.CreateParkingLot(parking);

            // assert
            // Nothing to assert, exception testing
        }


        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/03/04
        /// 
        /// Description:
        /// Tests to see if create parking lot throws an exception if the parking lot name is too long
        /// 
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestCreateParkingLotThrowsErrorIfNameTooLong()
        {
            // arrange
            ParkingLot parking = new ParkingLot()
            {
                LocationID = 100000,
                Name = "testtesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttesttest",
                Description = "Test Description",
                ImageName = "Lot Image Path"
            };

            // act
            _parkingLotManager.CreateParkingLot(parking);

            // assert
            // Nothing to assert, exception testing
        }
    }
}
