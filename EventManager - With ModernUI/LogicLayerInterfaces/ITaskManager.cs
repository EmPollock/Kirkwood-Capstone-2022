using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataObjects;

namespace LogicLayerInterfaces
{
    /// <summary>
    /// Mike Cahow
    /// Created: 2022/01/23
    /// 
    /// Description:
    /// Interface for handling Task manager class methods
    /// </summary>
    public interface ITaskManager
    {
        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/23
        /// 
        /// Description:
        /// Method that inserts a new task into the database
        /// </summary>
        /// <param name="newTask"></param>
        /// <returns>bool result</returns>
        bool AddTask(Tasks newTask);

        /// <summary>
        /// Mike Cahow
        /// Created: 2022/01/24
        /// 
        /// Description:
        /// Method that retrieves all priorities to populate cboPriorities
        /// </summary>
        /// <returns>List Priority</returns>
        List<Priority> RetrieveAllPriorities();

        /// <summary>
        /// Mike Cahow
        /// 2022/01/31
        /// 
        /// Description:
        /// Method that retrieves all tasks for an event
        /// </summary>
        /// <returns>list Tasks</returns>
        List<TasksVM> RetrieveAllActiveTasksByEventID(int eventID = 100000);
    }
}
