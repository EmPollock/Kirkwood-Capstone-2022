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
        /// 
        /// Vinayak Deshpande
        /// Updated: 2022/03/05
        /// 
        /// Description: modified to accept an extra
        /// parameter during task creation.
        ///
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
            _taskAccessor = taskAccessor;
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
        public bool AddTask(Tasks newtask, int numTotalVolunteers)
        {
            bool result = false;

            
            if(newtask.Name.Length >= 50)
            {
                throw new ApplicationException("Task name cannot exceed 50 characters.");
            }
            if(newtask.Description.Length >= 255)
            {
                throw new ApplicationException("Task description cannot exceed 255 characters.");
            }
            if(newtask.DueDate == null)
            {
                throw new ApplicationException("Please set a due date for this task.");
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
                result = (1 == _taskAccessor.InsertTasks(newtask, numTotalVolunteers));
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/02/07
        /// 
        /// Description:
        /// An update method that updates a task object in the task list
        /// </summary>
        /// <param name="oldTask"></param>
        /// <param name="newTask"></param>
        /// <returns></returns>
        public bool EditTask(Tasks oldTask, Tasks newTask)
        {
            bool result = false;

            if (newTask.Name == "" || newTask.Name == null)
            {
                throw new ApplicationException("Task name cannot be empty.");
            }
            if (newTask.Name.Length >= 50)
            {
                throw new ApplicationException("Task name cannot exceed 50 characters.");
            }
            if (newTask.Description == "" || newTask.Description == null)
            {
                throw new ApplicationException("Task description cannot be empty.");
            }
            if (newTask.Description.Length >= 255)
            {
                throw new ApplicationException("Task description cannot exceed 255 characters.");
            }
            if (newTask.DueDate == null)
            {
                throw new ApplicationException("Please set a due date for this task.");
            }
            if (newTask.DueDate == DateTime.Today.AddDays(-1))
            {
                throw new ApplicationException("Task due date cannot be set to the previous day.");
            }
            if (newTask.DueDate == DateTime.Today.AddHours(-1))
            {
                throw new ApplicationException("Task due date cannot be set to the previous hour.");
            }
            if (newTask.DueDate == DateTime.Today.AddMinutes(-1))
            {
                throw new ApplicationException("Task due date cannot be set to the previous minute.");
            }

            try
            {
                result = 1 == _taskAccessor.UpdateTasks(oldTask, newTask);
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

        /// <summary>
        /// Mike Cahow
        /// 2022/01/31
        /// 
        /// Description:
        /// Method that retrieves all tasks for an event
        /// </summary>
        /// <returns>list Tasks</returns>
        public List<TasksVM> RetrieveAllActiveTasksByEventID(int eventID = 100000)
        {
            List<TasksVM> tasks = new List<TasksVM>();

            try
            {
                tasks = _taskAccessor.SelectAllActiveTasksByEventID(eventID);
            }
            catch (Exception)
            {

                throw;
            }

            return tasks;
        }

    }
}
