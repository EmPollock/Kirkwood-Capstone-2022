using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;

namespace DataAccessInterfaces
{
    /// <summary>
    /// Mike Cahow
    /// Created: 2022/01/23
    /// 
    /// Description:
    /// Interface for methods used to access data from the database
    /// </summary>
    public interface ITaskAccessor
    {
        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// An insert method for a new task into database
        /// </summary>
        /// <param name="newTask"></param>
        /// <returns>int</returns>
        int InsertTasks(Tasks newTask);

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Select method that retrieves a list of all priorities from Priority table
        /// </summary>
        /// <returns></returns>
        List<Priority> SelectAllPriorities();

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/31
        /// 
        /// Description:
        /// Select method that grabs a list of all tasks for an event
        /// </summary>
        /// <returns>List Tasks</returns>
        List<TasksVM> SelectAllActiveTasksByEventID(int eventID);
    }
}
