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
    }
}
