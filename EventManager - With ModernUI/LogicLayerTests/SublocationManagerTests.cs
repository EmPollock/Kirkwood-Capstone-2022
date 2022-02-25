using DataObjects;
using LogicLayerInterfaces;
using DataAccessFakes;
using LogicLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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

        [TestMethod]
        public void 
    }
}
