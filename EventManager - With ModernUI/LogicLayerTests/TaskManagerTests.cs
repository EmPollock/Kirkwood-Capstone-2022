using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using LogicLayer;
using DataAccessFakes;
using DataObjects;
using LogicLayerInterfaces;
using System.Collections.Generic;

namespace LogicLayerTests
{
    /// <summary>
    /// Mike Cahow
    /// Created: 2022/01/23
    /// 
    /// Description:
    /// Test Class to test the Tasks Logic Layer methods
    /// </summary>
    [TestClass]
    public class TaskManagerTests
    {
        private ITaskManager taskManager = null;

        [TestInitialize]
        public void TestSetup()
        {
            taskManager = new TaskManager(new TaskAccessorFakes());
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Test that returns true if successful
        /// </summary>
        [TestMethod]
        public void TestInsertTaskReturnsTrueIfCreated()
        {
            //arrange
            Tasks task = new Tasks()
            {
                EventID = 100001,
                Name = "Clean Something",
                Description = "Something needs cleaned and it ain't me",
                DueDate = DateTime.Today,
                Priority = 2
            };
            bool expectedResult = true;
            bool actualResult;

            //act
            actualResult = taskManager.AddTask(task);

            //assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// Test that returns an application exception if Name is empty
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestInsertTaskThrowsExceptionIfNameIsBlank()
        {
            //arrange
            Tasks task = new Tasks()
            {
                EventID = 100001,
                Name = "",
                Description = "Something needs cleaned and it ain't me",
                DueDate = DateTime.Today,
                Priority = 2
            };

            //act
            taskManager.AddTask(task);

            //assert
            //checking for exception

        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// Test that returns an application exception if Name is over 50 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestInsertTaskThrowsExceptionIfNameIsLongerThan50Characters()
        {
            //arrange
            Tasks task = new Tasks()
            {
                EventID = 100001,
                Name = "Cleannnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnn Something",
                Description = "Something needs cleaned and it ain't me",
                DueDate = DateTime.Today,
                Priority = 2
            };

            //act
            taskManager.AddTask(task);

            //assert
            //checking for exception

        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// Test that returns an application exception if Description is empty
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestInsertTaskThrowsExceptionIfDescriptionIsBlank()
        {
            //arrange
            Tasks task = new Tasks()
            {
                EventID = 100001,
                Name = "Clean",
                Description = "",
                DueDate = DateTime.Today,
                Priority = 2
            };

            //act
            taskManager.AddTask(task);

            //assert
            //checking for exception

        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// Test that returns an application exception if Description is over 255 characters
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestInsertTaskThrowsExceptionIfDescriptionIsLongerThan255Characters()
        {
            //arrange
            Tasks task = new Tasks()
            {
                EventID = 100001,
                Name = "Clean",
                Description = "Something needs cleaned and it ain't me but lonnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnnger",
                DueDate = DateTime.Today,
                Priority = 2
            };

            //act
            taskManager.AddTask(task);

            //assert
            //checking for exception

        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// Test that returns an application exception if DueDate is set to previous day
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestInsertTaskThrowsExceptionIfDueDateIsSetToPreviousDay()
        {
            //arrange
            Tasks task = new Tasks()
            {
                EventID = 100001,
                Name = "Clean",
                Description = "Something needs cleaned and it ain't me",
                DueDate = DateTime.Today.AddDays(-1),
                Priority = 2
            };

            //act
            taskManager.AddTask(task);

            //assert
            //checking for exception

        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// Test that returns an application exception if DueDate is set to previous hour
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestInsertTaskThrowsExceptionIfDueDateIsSetToPreviousHour()
        {
            //arrange
            Tasks task = new Tasks()
            {
                EventID = 100001,
                Name = "Clean",
                Description = "Something needs cleaned and it ain't me",
                DueDate = DateTime.Today.AddHours(-1),
                Priority = 2
            };

            //act
            taskManager.AddTask(task);

            //assert
            //checking for exception

        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// Test that returns an application exception if DueDate is set to previous minutes
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void TestInsertTaskThrowsExceptionIfDueDateIsSetToPreviousMinute()
        {
            //arrange
            Tasks task = new Tasks()
            {
                EventID = 100001,
                Name = "Clean",
                Description = "Something needs cleaned and it ain't me",
                DueDate = DateTime.Today.AddMinutes(-1),
                Priority = 2
            };

            //act
            taskManager.AddTask(task);

            //assert
            //checking for exception

        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/31
        /// 
        /// Description:
        /// Test that checks if the correct number of items in a list of tasks will be returned
        /// Should be four according to TaskAccessorFakes
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllActiveTasksByEventIDReturnsCorrectNumberOfListItems()
        {
            // arrange
            const int eventID = 100000;
            const int expectedCount = 3;
            int actualCount;

            // act
            actualCount = taskManager.RetrieveAllActiveTasksByEventID(eventID).Count;

            // assert
            Assert.AreEqual(expectedCount, actualCount);
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/31
        /// 
        /// Description:
        /// Test that checks if the method returned the correct number of list items
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllActiveTasksByEventIDFailsWithIncorrectNumberOfListItems()
        {
            // arrange
            const int eventID = 100000;
            const int expectedCount = 2;
            int actualCount;


            // act
            actualCount = taskManager.RetrieveAllActiveTasksByEventID(eventID).Count;

            // assert
            Assert.AreNotEqual(expectedCount, actualCount);

        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/31
        /// 
        /// Description: 
        /// Test that fails because an incorrect list was passed
        /// </summary>
        [TestMethod]
        public void TestRetrieveAllActiveTasksByEventIDFailsWithIncorrectListBeingReturned()
        {
            // arrange
            const int badEventID = 100001;
            const int expectedCount = 3;
            int actualCount;

            // act
            actualCount = taskManager.RetrieveAllActiveTasksByEventID(badEventID).Count;

            // assert
            Assert.AreNotEqual(expectedCount, actualCount);

        }

    }
}
