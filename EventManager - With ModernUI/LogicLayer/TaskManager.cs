using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccessLayer;
using DataObjects;
using DataAccessInterfaces;
using LogicLayerInterfaces;

namespace LogicLayer
{
    /// <summary>
    /// Mike Cahow
    /// Created: 2022/01/23
    /// 
    /// Description:
    /// Class for handling Task Manager methods
    /// </summary>
    public class TaskManager : ITaskManager
    {
        private ITaskAccessor _taskAccessor = null;

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Constructor for task manager using the task accessor
        /// </summary>
        public TaskManager()
        {
            _taskAccessor = new TaskAccessor();
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Constructor that takes and sets it to the _taskAccessor field in order to pass test data to the task manager
        /// </summary>
        /// <param name="taskAccessor"></param>
        public TaskManager(ITaskAccessor taskAccessor)
        {
            _taskAccessor = new TaskAccessor();
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// An insert method that adds a new task to the task list
        /// </summary>
        /// <param name="newtask"></param>
        /// <returns>bool result that returns true if successful</returns>
        public bool AddTask(Tasks newtask)
        {
            bool result = false;

            if(newtask.Name == "" || newtask.Name == null)
            {
                throw new ApplicationException("Task name cannot be blank.");
            }
            if(newtask.Name.Length >= 50)
            {
                throw new ApplicationException("Task name cannot exceed 50 characters.");
            }
            if(newtask.Description == "" || newtask.Description == null)
            {
                throw new ApplicationException("Task description cannot be blank.");
            }
            if(newtask.Description.Length >= 255)
            {
                throw new ApplicationException("Task description cannot exceed 255 characters.");
            }
            if(newtask.DueDate == DateTime.Today.AddDays(-1))
            {
                throw new ApplicationException("Task due date cannot be set to the previous day.");
            }
            if(newtask.DueDate == DateTime.Today.AddHours(-1))
            {
                throw new ApplicationException("Task due date cannot be set to the previous hour.");
            }
            if (newtask.DueDate == DateTime.Today.AddMinutes(-1))
            {
                throw new ApplicationException("Task due date cannot be set to the previous minute.");
            }

            try
            {
                result = (1 == _taskAccessor.InsertTasks(newtask));
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// Method that retrieves the list of priorities from the data access layer
        /// </summary>
        /// <returns></returns>
        public List<Priority> RetrieveAllPriorities()
        {
            List<Priority> priorities = new List<Priority>();

            try
            {
                priorities = _taskAccessor.SelectAllPriorities();
            }
            catch (Exception)
            {

                throw;
            }

            return priorities;
        }
    }
}
