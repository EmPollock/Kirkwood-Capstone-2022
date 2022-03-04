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
    public class ServiceManagerTests
    {
        private IServiceManager _serviceManager = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _serviceManager = new ServiceManager(new ServiceAccessorFake());
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Test that makes sure RetrieveServicesBySupplierID 
        /// returns a list with the correct number of services
        /// </summary>
        [TestMethod]
        public void TestRetrieveServicesBySupplierIDReturnsAmountOfServices()
        {
            // arrange
            const int supplierID = 100000;
            const int expected = 2;
            int actual;

            // act
            actual = _serviceManager.RetrieveServicesBySupplierID(supplierID).Count;

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/03/02
        /// 
        /// Description:
        /// Test that makes sure RetrieveServicesBySupplierID with a bad supplierID
        /// returns a list with no services 
        /// </summary>
        [TestMethod]
        public void TestRetrieveServicesBySupplierIDByBadSupplierIDReturnsAnEmptyList()
        {
            // arrange
            const int supplierID = 999999;
            const int expected = 0;
            int actual;

            // act
            actual = _serviceManager.RetrieveServicesBySupplierID(supplierID).Count;

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}
