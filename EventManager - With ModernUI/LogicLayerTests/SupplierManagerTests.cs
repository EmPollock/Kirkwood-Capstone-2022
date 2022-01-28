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
    public class SupplierManagerTests
    {
        private ISupplierManager _supplierManager = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _supplierManager = new SupplierManager(new SupplierAccessorFake());
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/01/27
        /// 
        /// Description:
        /// Test that makes sure retrieve active suppliers returns a list with the correct number of suppliers
        /// </summary>

        [TestMethod]
        public void TestRetrieveActiveSuppliersReturnsCorrectList()
        {
            // arrange
            const int expected = 4;
            int actual;

            // act
            actual = _supplierManager.RetrieveActiveSuppliers().Count;

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}
