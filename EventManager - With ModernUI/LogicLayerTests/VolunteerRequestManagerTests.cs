using DataAccessFakes;
using LogicLayer;
using LogicLayerInterfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayerTests
{
    [TestClass]
    public class VolunteerRequestManagerTests
    {
        private IVolunteerRequestManager _volunteerRequestManager = null;

        [TestInitialize]
        public void TestInitialize()
        {
            _volunteerRequestManager = new VolunteerRequestManager(new VolunteerRequestAccessorFake());
        }

        /// <summary>
        /// Vinayak Deshpande
        /// Created 2022/01/28
        /// Description
        /// Recreation of Tests that check the request accessor
        /// </summary>

        [TestMethod]
        public void TestRetrieveAllRequests()
        {
            const int expectedAmount = 5;
            int actualamount = 0;

            actualamount = _volunteerRequestManager.RetrieveVolunteerRequests(999999).Count;

            Assert.AreEqual(expectedAmount, actualamount);
        }
    }
}
