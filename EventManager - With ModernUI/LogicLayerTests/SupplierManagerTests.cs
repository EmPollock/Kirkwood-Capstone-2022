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

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/11
        /// 
        /// Description:
        /// Test to make sure that RetrieveSupplierReviewsBySupplierID correctly returns a list of reviews.
        /// </summary>
        [TestMethod]
        public void TestRetrieveSupplierReviewsBySupplierIDReturnsCorrectReviewList()
        {
            // arrange
            const int supplierID = 100000;
            List<Reviews> reviewList = new List<Reviews>();
            List<Reviews> expectedList = new List<Reviews>();

            expectedList.Add(new Reviews()
            {
                ForeignID = 100000,
                ReviewID = 100000,
                FullName = "Whodunnit",
                ReviewType = "Supplier Review",
                Rating = 3,
                Review = "Could be better.",
                DateCreated = DateTime.Now,
                Active = true
            });

            expectedList.Add(new Reviews()
            {
                ForeignID = 100000,
                ReviewID = 100020,
                FullName = "Whodunnit2",
                ReviewType = "Supplier Review2",
                Rating = 5,
                Review = "Amazing!",
                DateCreated = DateTime.Now,
                Active = true
            });

            // act
            reviewList = _supplierManager.RetrieveSupplierReviewsBySupplierID(supplierID);

            // assert
            Assert.AreEqual(expectedList.Count, reviewList.Count);
            for (int i = 0; i < expectedList.Count; i++)
            {
                Reviews expected = expectedList[i];
                Reviews actual = reviewList[i];
                Assert.AreEqual(expected.ForeignID, actual.ForeignID);
                Assert.AreEqual(expected.ReviewID, actual.ReviewID);
                Assert.AreEqual(expected.FullName, actual.FullName);
                Assert.AreEqual(expected.Rating, actual.Rating);
                Assert.AreEqual(expected.ReviewType, actual.ReviewType);
                Assert.AreEqual(expected.Review, actual.Review);
            }
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/11
        /// 
        /// Description:
        /// Test to make sure that RetrieveSupplierReviewsBySupplierID correctly returns an empty list of reviews
        /// if the supplierID provided has no reviews.
        /// </summary>
        [TestMethod]
        public void TestRetrieveSupplierReviewsBySupplierIDReturnsEmptyReviewListWithBadID()
        {
            // arrange
            const int supplierID = 10006;
            List<Reviews> reviewList = new List<Reviews>();

            // act
            reviewList = _supplierManager.RetrieveSupplierReviewsBySupplierID(supplierID);

            // assert
            Assert.IsFalse(reviewList.Any());
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/11
        /// 
        /// Description:
        /// Test to make sure that RetrieveSupplierTagsBySupplierID correctly returns a list of tags.
        /// </summary>
        [TestMethod]
        public void TestRetrieveSupplierTagsBySupplierIDReturnsCorrectTagList()
        {
            // arrange
            const int supplierID = 100000;
            List<string> reviewList = new List<string>();
            List<string> expectedList = new List<string>()
            {
                "Test Tag 1",
                "Test Tag 2"
            };

            // act
            reviewList = _supplierManager.RetrieveSupplierTagsBySupplierID(supplierID);

            // assert
            CollectionAssert.AreEqual(reviewList, expectedList);
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/11
        /// 
        /// Description:
        /// Test to make sure that RetrieveSupplierTagsBySupplierID returns an empty tag list 
        /// if there are no tags associated with the supplier ID
        /// </summary>
        [TestMethod]
        public void TestRetrieveSupplierTagsBySupplierIDReturnsEmptyTagListWithBadID()
        {
            // arrange
            const int supplierID = 10006;
            List<string> reviewList = new List<string>();

            // act
            reviewList = _supplierManager.RetrieveSupplierTagsBySupplierID(supplierID);

            // assert
            Assert.IsFalse(reviewList.Any());
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/11
        /// 
        /// Description:
        /// Test to make sure that RetrieveSupplierImagesBySupplierID correctly returns a list of images.
        /// </summary>
        [TestMethod]
        public void TestRetrieveSupplierImagesBySupplierIDReturnsCorrectImageList()
        {
            // arrange
            const int supplierID = 100000;
            List<string> reviewList = new List<string>();
            List<string> expectedList = new List<string>()
            {
                "Fakepath.png",
                "Fakepath2.png",
                "Fakepath3.png"
            };

            // act
            reviewList = _supplierManager.RetrieveSupplierImagesBySupplierID(supplierID);

            // assert
            CollectionAssert.AreEqual(reviewList, expectedList);
        }

        /// <summary>
        /// Christopher Repko
        /// Created: 2022/02/11
        /// 
        /// Description:
        /// Test to make sure that RetrieveSupplierImagesBySupplierID correctly returns an empty list 
        /// of images if there are no images associated with the supplier ID
        /// </summary>
        [TestMethod]
        public void TestRetrieveSupplierImagesBySupplierIDReturnsEmptyImageListWithBadID()
        {
            // arrange
            const int supplierID = 10006;
            List<string> reviewList = new List<string>();

            // act
            reviewList = _supplierManager.RetrieveSupplierImagesBySupplierID(supplierID);

            // assert
            Assert.IsFalse(reviewList.Any());
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Test to make sure that RetrieveSupplierAvailabilityBySupplierIDAndDate correctly prioritizes an exception
        /// availability over a regular availability on the same date
        /// </summary>
        [TestMethod]
        public void TestRetrieveSupplierAvailabilityBySupplierIDAndDatePrioritizesExceptions()
        {
            // arrange
            const int supplierID = 100001;
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
            actualList = _supplierManager.RetrieveSupplierAvailabilityBySupplierIDAndDate(supplierID, date);

            // assert
            // collection assert does not seem to be able to compare reference variables properly
            // this properly proves that the correct list is being chosen
            Assert.AreEqual(expectedList.First<Availability>().TimeStart, actualList.First().TimeStart);
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Test to make sure that RetrieveSupplierAvailabilityBySupplierIDAndDate correctly returns the right
        /// regular availabilities when there are no exception availabilities on that date for that supplier.
        /// </summary>
        [TestMethod]
        public void TestRetrieveSupplierAvailabilityBySupplierIDAndDateCorrectList()
        {
            // arrange
            const int supplierID = 100000;
            DateTime date = new DateTime(2022, 01, 01);
            int actualCount;
            int expectedCount = 2;

            // act
            actualCount = _supplierManager.RetrieveSupplierAvailabilityBySupplierIDAndDate(supplierID, date).Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Test to make sure that RetrieveSupplierAvailabilityBySupplierIDAndDate correctly returns an empty
        /// list when no availability is found
        /// </summary>
        [TestMethod]
        public void TestRetrieveSupplierAvailabilityBySupplierIDAndDateReturnsNullWithNoneFound()
        {
            // arrange
            const int supplierID = 100000;
            DateTime badDate = new DateTime(1999, 01, 01);
            List<Availability> actualList;

            // act
            actualList = _supplierManager.RetrieveSupplierAvailabilityBySupplierIDAndDate(supplierID, badDate);

            // assert
            Assert.IsFalse(actualList.Any());
        }

        /// <summary>
        /// Kris Howell
        /// Created: 2022/03/03
        /// 
        /// Description:
        /// Test to make sure that RetrieveSupplierAvailabilityBySupplierIDAndDate correctly returns an Availability
        /// object with null TimeStart/TimeEnd properties when there is an exception for a day with no availability
        /// </summary>
        [TestMethod]
        public void TestRetrieveSupplierAvailabilityBySupplierIDAndDateReturnsEmptyAvailabilityForEmptyException()
        {
            // arrange
            const int supplierID = 100000;
            DateTime date = new DateTime(2022, 01, 03);
            DateTime? expected = null;
            DateTime? actual;

            // act
            actual = _supplierManager.RetrieveSupplierAvailabilityBySupplierIDAndDate(supplierID, date).First().TimeStart;

            // assert
            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        /// Derrick Nagy
        /// Created: 2022/04/05
        /// 
        /// Description:
        /// That that makes usr ethat the supplier availablity is returned
        /// 
        /// </summary>
        [TestMethod]
        public void TestSupplierAvailabilityForNextThreeMonthsReturnsCorrectList()
        {
            // arrange
            const int supplierID = 100000;
            const int expected = 3;
            int actual;


            // act
            List<DateTime> results = _supplierManager.SupplierAvailabilityForNextThreeMonths(supplierID);
            actual = results.Count;

            // assert
            Assert.AreEqual(expected, actual);




        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/09
        /// 
        /// Description:
        /// Test to make sure that RetrieveSupplierAvailabilityBySupplierID correctly returns an AvailabilityVM list
        /// </summary>
        [TestMethod]
        public void TestRetrieveSupplierAvailabilityBySupplierIDReturnsCorrectAmount()
        {
            // arrange
            const int supplierID = 100000;
            int expectedCount = 3;
            int actualCount;

            // act
            actualCount = _supplierManager.RetrieveSupplierAvailabilityBySupplierID(supplierID).Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Austin Timmerman
        /// Created: 2022/04/09
        /// 
        /// Description:
        /// Test to make sure that RetrieveSupplierAvailabilityBySupplierID correctly returns an AvailabilityVM list
        /// </summary>
        [TestMethod]
        public void TestRetrieveSupplierAvailabilityExceptionBySupplierIDReturnsCorrectAmount()
        {
            // arrange
            const int supplierID = 100000;

            int expectedCount = 1;
            int actualCount;

            // act
            actualCount = _supplierManager.RetrieveSupplierAvailabilityExceptionBySupplierID(supplierID).Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/04
        /// 
        /// Description:
        /// Test to make sure that RetrieveSupplierBySupplierID returns the correct supplier
        /// </summary>
        [TestMethod]
        public void TestRetrieveSupplierBySupplierIDReturnsCorrectSupplier()
        {
            // arrange
            const int id = 100000;
            const string expected = "Test Supplier 1";
            Supplier supplier = new Supplier();
            string actual;

            // act
            supplier = _supplierManager.RetrieveSupplierBySupplierID(id);
            actual = supplier.Name;

            // assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/04
        /// 
        /// Description:
        /// Test to make sure that RetrieveSupplierBySupplierID returns an 
        /// application exception given an invalid id
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestRetrieveSupplierBySupplierIDReturnsApplicationExceptionIfInvalidID()
        {
            // arrange
            const int id = 200000;
            Supplier supplier = new Supplier();

            // act
            supplier = _supplierManager.RetrieveSupplierBySupplierID(id);

            // assert
            // nothing to assert
        }

        /// <summary>
        /// Logan Baccam
        /// Created: 2022/04/22
        /// 
        /// Description:
        /// Test to make sure CreateSupplier returns correct
        /// row affected
        /// </summary>
        [TestMethod]
        public void TestCreateSupplierRequestReturns1IfCreated()
        {
            // arrange
            Supplier actual;
            Supplier expected = new Supplier()
            {
                SupplierID = 100009,
                Name = "Test Supplier 10",
                Description = "Description of Test Supplier 10 goes here.",
                Phone = "100-111-1111",
                Email = "testSupplier10@suppliers.com",
                Address1 = "Test Supplier 100th Street",
                City = "Cedar Rapids",
                State = "Iowa",
                ZipCode = "52404",
                Active = true

            };
            // act
            _supplierManager.CreateSupplier(expected);
            actual = _supplierManager.RetrieveActiveSuppliers().Single(x => x.SupplierID == expected.SupplierID);
            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}
