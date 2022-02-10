using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;
using DataAccessInterfaces;

namespace DataAccessFakes
{
    /// <summary>
    /// Mike Cahow
    /// Created: 2022/01/24
    /// 
    /// Description:
    /// Class of fakes used to test the methods related to Tasks
    /// </summary>
    public class TaskAccessorFakes : ITaskAccessor
    {
        private List<TasksVM> _fakeTasks = new List<TasksVM>();
        private List<Priority> _priorities = new List<Priority>();

        public TaskAccessorFakes()
        {
            _fakeTasks.Add(new TasksVM()
            {
                EventID = 1000000,
                TaskEventName = "Test Event 1",
                Name = "Mop",
                Description = "Mop up spilled drink",
                DueDate = DateTime.Now,
                Priority = 3,
                TaskPriority = "High",
                Active = true
            });

            _fakeTasks.Add(new TasksVM()
            {
                EventID = 1000000,
                TaskEventName = "Test Event 1",
                Name = "Wash Towels",
                Description = "Wash Towels after Events",
                DueDate = DateTime.Now,
                Priority = 1,
                TaskPriority = "Low",
                Active = true
            });

            _fakeTasks.Add(new TasksVM()
            {
                EventID = 1000000,
                TaskEventName = "Test Event 1",
                Name = "Sweep",
                Description = "Sweep broken glass",
                DueDate = DateTime.Now,
                Priority = 3,
                TaskPriority = "High",
                Active = true
            });
            _fakeTasks.Add(new TasksVM()
            {
                EventID = 1000000,
                TaskEventName = "Test Event 1",
                Name = "Clean Trash",
                Description = "Make sure trash is cleared from spectator area",
                DueDate = DateTime.Now,
                Priority = 2,
                TaskPriority = "Medium",
                Active = false
            });

            /// <summary>
            /// Mike Cahow
            /// Created: 2022/01/24
            /// 
            /// Description:
            /// Creating a "fake" list of priorities to use for testing purposes
            /// </summary>
            _priorities.Add(new Priority()
            {
                PriorityID = 1,
                Description = "Low"
            });
            _priorities.Add(new Priority()
            {
                PriorityID = 2,
                Description = "Medium"
            });
            _priorities.Add(new Priority()
            {
                PriorityID = 3,
                Description = "High"
            });
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// Fake insert tasks method used for testing
        /// </summary>
        /// <param name="newTask"></param>
        /// <returns>int rowsAffected</returns>
        public int InsertTasks(Tasks newTask)
        {
            int rowsAffected = 0;

            try
            {
                _fakeTasks.Add(new TasksVM()
                {
                    EventID = 999999,
                    Name = "Clean",
                    Description = "Clean this room.",
                    DueDate = DateTime.Now,
                    Priority = 3
                });
                rowsAffected = 1;
            }
            catch (Exception)
            {

                throw;
            }

            return rowsAffected;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// A method that returns a fake list of priorites
        /// </summary>
        /// <returns>List Priority</returns>
        public List<Priority> SelectAllPriorities()
        {
            return null;
        }

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/31
        /// 
        /// Description:
        /// Select method that grabs a fake list of all tasks for an event
        /// </summary>
        /// <returns>List Tasks</returns>
        public List<TasksVM> SelectAllActiveTasksByEventID(int eventID)
        {
            List<TasksVM> tasks = new List<TasksVM>();

            foreach(var task in _fakeTasks)
            {
                if(task.EventID == eventID && task.Active == true)
                {
                    tasks.Add(task);
                }
            }

            return tasks;
        }
    }
}
